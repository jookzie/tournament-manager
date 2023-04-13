using Modules.Entities.ScheduleTypes;
using Modules.Enums;
using Modules.Utilities;
using System.Reflection;

namespace Modules.Entities
{
    public record Tournament
    {
        public Guid ID { get; set; } = Guid.NewGuid();
        public Type TournamentSystem
        {
            get => _tournamentSystem;
            set
            {
                ClearSchedule();
                _tournamentSystem = value;
            }
        }
        public SportType SportType { get; set; }
        public string Location
        {
            get => _location;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("Location");
                if (value.Length > 200)
                    throw new ArgumentOutOfRangeException("Location", "Location cannot be longer than 200 characters");
                _location = value;
            }
        }
        public string Description
        {
            get => _description;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("Description");
                if (value.Length > 200)
                    throw new ArgumentOutOfRangeException("Description", "Description cannot be longer than 200 characters");
                _description = value;
            }
        }
        public (DateTime Start, DateTime End) DateRange
        {
            get => _dateRange;
            set
            {
                if (value.Start > value.End)
                    throw new InvalidOperationException("Start date cannot be after end date.");
                _dateRange = value;
            }
        }
        public (int Min, int Max) CapacityRange
        {
            get => _capacityRange;
            set
            {
                if (value.Max < 3)
                    throw new ArgumentOutOfRangeException("Maximum capacity", "Max capacity cannot be below 3.");
                if (value.Min < 3)
                    throw new ArgumentOutOfRangeException("Minimum capacity", "Minimum capacity cannot be less than 3.");
                if (value.Min > 100)
                    throw new ArgumentOutOfRangeException("Minimum capacity", "Minimum capacity cannot be greater than 100.");
                if (value.Max > 100)
                    throw new ArgumentOutOfRangeException("Maximum capacity", "Maximum capacity cannot be greater than 100.");
                if (value.Max < Players.Count)
                    throw new InvalidOperationException("Maximum capacity cannot be less than the number of players.");
                if (value.Min > value.Max)
                    throw new InvalidOperationException("Minimum capacity cannot be greater than maximum capacity.");
                _capacityRange = value;
            }
        }
        public List<User> Players
        {
            get => _players;
            set
            {
                if (value.Count > CapacityRange.Max)
                    throw new InvalidOperationException("Tournament cannot have more than maximum capacity of players.");
                _players = value;
            }
        }
        public ISchedule Schedule { get; private set; }
        public void SetScheduleBy(List<Round> rounds)
        {
            Schedule = (ISchedule)Activator.CreateInstance(TournamentSystem, rounds);
        }
        public void ClearSchedule()
        {
            Schedule = null;
        }
        public void GenerateSchedule(int numberOfGames = 3)
        {
            if(numberOfGames < 1)
                throw new ArgumentOutOfRangeException("Number of games", "Number of games cannot be negative.");
            if (Players.Count < CapacityRange.Min)
                throw new InvalidOperationException(
                    $"Minimum required amount of users not met - " +
                    $"{Players.Count} out of {CapacityRange.Min}.");
            if (DateTime.Now > DateRange.End)
                throw new InvalidOperationException(
                    $"Could not generate schedule due to the tournament being expired in {DateRange.End:dd-MMMM}.");
            Schedule = (ISchedule)Activator.CreateInstance(TournamentSystem, Players, numberOfGames);
        }

        // Scoreboard is a key-value pair list of users and their number of wins
        public List<KeyValuePair<User, int>> GetScoreboard()
        {
            var scoreboard = new List<KeyValuePair<User, int>>();
            if (!IsScheduled) return scoreboard;
            scoreboard = Schedule.Rounds
                .SelectMany(round => round.Matches)
                .Where(m => m.Winner is not null)
                .GroupBy(m => m.Winner)
                .OrderByDescending(winner => winner.Count())
                .Select(winner => new KeyValuePair<User, int>(winner.Key, winner.Count()))
                .ToList();
            
            // Check for ties
            if (scoreboard.Select(p => p.Value).FindDuplicates().Count == 0)
                return scoreboard;
            
            // Iterate through the scoreboard.
            // If current winner has the same score as the next winner,
            // get the matchup from the schedule,
            // and swap the winners if the second player is the winner of the match.
            for (int i = 0; i < scoreboard.Count - 1; i++)
            {
                var player1 = scoreboard[i];
                var player2 = scoreboard[i+1];
                // If the two winners have different scores, skip
                if (player1.Value != player2.Value)
                    continue;
                var match = Schedule.Rounds
                    .SelectMany(r => r.Matches)
                    .FirstOrDefault(m =>
                        m.Players.First == player1.Key &&
                        m.Players.Second == player2.Key ||
                        m.Players.First == player2.Key &&
                        m.Players.Second == player1.Key);
                if (match.Winner == player2.Key)
                    scoreboard.Swap(i, i + 1);
            }
            
            return scoreboard;
        }
        public bool IsConcluded
            => IsScheduled &&
              !Schedule.Rounds.Any(round =>
                    round.Matches.Any(match =>
                    match.Winner is null))
        ;
        public bool IsFull => Players.Count == CapacityRange.Max;
        public bool IsScheduled => Schedule is not null;
        public bool IsAvailable => !IsScheduled && !IsFull && DateTime.Now.AddDays(7) < DateRange.Start;
        public string Status
          => IsConcluded  ? "Concluded"
            : IsScheduled ? "Scheduled"
            : IsFull      ? "Full"
            : IsAvailable ? "Available"
                          : "Unavailable"
        ;
        public bool Overlaps(Tournament tournament)
        {
            if (tournament is null)
                return false;
            if (tournament.ID == ID)
                return true;
            if (tournament.DateRange.Start >= DateRange.Start &&
                tournament.DateRange.Start <= DateRange.End)
                return true;
            if (tournament.DateRange.End >= DateRange.Start &&
                tournament.DateRange.End <= DateRange.End)
                return true;
            if (tournament.DateRange.Start <= DateRange.Start &&
                tournament.DateRange.End >= DateRange.End)
                return true;
            return false;
        }
        public override string ToString()
            => $"[{Players.Count}/{CapacityRange.Max}]: {Location} [{Status}]"
        ;
        public string Information
            => $"{ToString()} {SportType.GetDescription()} - {TournamentSystem.Name}"
        ;
        public static List<Type> GetTournamentTypes()
            => Assembly
                .GetAssembly(typeof(ISchedule))
                .GetTypes()
                .Where(t => t.GetInterface(nameof(ISchedule)) == typeof(ISchedule))
                .ToList()
        ;

        private Type _tournamentSystem;
        private string _location;
        private string _description;
        private (DateTime, DateTime) _dateRange = new();
        private (int, int) _capacityRange = new();
        private List<User> _players = new();
    }
}
