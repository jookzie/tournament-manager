using Modules.Entities;
using Modules.Interfaces.BLL;
using Modules.Interfaces.DAL;

namespace BLL
{
    public class TournamentManager
    {
        public TournamentManager(IUserManager userManager, ITournamentRepository tournamentRepository)
        {
            _userManager = userManager;
            _tournamentRepository = tournamentRepository;
            //MockData.LoadTournamentMocks(this);
        }
        public IEnumerable<Tournament> GetAllTournaments()
        {
            _tournaments = _tournamentRepository
                .GetAllTournaments()
                .ToDictionary(t => t.ID, t => t);
            return _tournaments.Values;
        }
        public Tournament? GetTournamentBy(Guid id)
        {
            if (_tournaments.TryGetValue(id, out var tournament)) 
                return tournament;
            tournament = _tournamentRepository.GetTournamentBy(id);
            if (tournament is not null) 
                _tournaments.Add(id, tournament);
            return tournament;
        }
        public void AddTournament(Tournament tournament)
        {
            _tournaments.Add(tournament.ID, tournament);
            _tournamentRepository.AddTournament(tournament);
        }
        public void UpdateTournament(Tournament tournament)
        {
            _tournaments[tournament.ID] = tournament;
            _tournamentRepository.UpdateTournament(tournament);
        }
        public bool DeleteTournament(Tournament tournament)
        {
            _tournamentRepository.DeleteTournament(tournament);
            return _tournaments.Remove(tournament.ID);
        }

        public bool RegisterPlayer(Guid tournamentID, Guid userID)
        {
            var user = _userManager.GetUserBy(userID);
            var tournament = _tournaments[tournamentID];

            if (user is null || tournament is null || tournament.Players.Contains(user)) 
                return false;

            if (_tournaments.Values.Any(t =>
                t.Players.Contains(user) &&
                t.Overlaps(tournament) &&
                !t.IsConcluded))
                throw new InvalidOperationException(
                    $"Player '{user}' is already registered in another tournament with overlapping period.");

            tournament.Players.Add(user);
            _tournamentRepository.RegisterPlayer(tournament, user);
            return true;
        }

        public bool WithdrawPlayer(Guid tournamentID, Guid userID)
        {
            var user = _userManager.GetUserBy(userID);
            var tournament = _tournaments[tournamentID];
            if (user is not null &&
                tournament.Players.Contains(user) &&
                !tournament.IsScheduled)
            {
                tournament.Players.Remove(user);
                _tournamentRepository.WithdrawPlayer(tournament, user);
                return true;
            }
            else return false;
        }

        private Dictionary<Guid, Tournament> _tournaments = new();
        private readonly IUserManager _userManager;
        private readonly ITournamentRepository _tournamentRepository;
    }
}
