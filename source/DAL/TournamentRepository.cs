using Modules.Entities;
using Modules.Enums;
using Modules.Interfaces.DAL;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class TournamentRepository : ITournamentRepository
    {
        private readonly string _conString;
        private readonly IUserRepository _userRepository;
        private readonly IScheduleRepository _scheduleRepository;
        public TournamentRepository(string conString, IUserRepository userRepository, IScheduleRepository scheduleRepository)
        {
            _conString = conString;
            _userRepository = userRepository;
            _scheduleRepository = scheduleRepository;
        }

        public bool AddTournament(Tournament tournament)
        {
            try
            {
                MySqlHelper.ExecuteNonQuery(_conString,
                    "INSERT INTO tournaments(id, sportType, tournamentSystem, location, description, startDate, endDate, maxCapacity, minCapacity)" +
                    "VALUES (@id, @sportType, @tournamentSystem, @location, @description, @startDate, @endDate, @maxCapacity, @minCapacity);",
                    new MySqlParameter("@id", tournament.ID.ToByteArray()),
                    new MySqlParameter("@sportType", tournament.SportType),
                    new MySqlParameter("@tournamentSystem", tournament.TournamentSystem.Name),
                    new MySqlParameter("@location", tournament.Location),
                    new MySqlParameter("@description", tournament.Description),
                    new MySqlParameter("@startDate", tournament.DateRange.Start),
                    new MySqlParameter("@endDate", tournament.DateRange.End),
                    new MySqlParameter("@maxCapacity", tournament.CapacityRange.Max),
                    new MySqlParameter("@minCapacity", tournament.CapacityRange.Min));
                return true;

            }
            catch (MySqlException ex) when (ex.Number == 1062)
            {
                return false;
            }

        }


        public IEnumerable<Tournament> GetAllTournaments()
        {
            var tournaments = new Dictionary<Guid, Tournament>();
            using var reader = MySqlHelper.ExecuteReader(_conString,
                "SELECT id, sportType, tournamentSystem, location, description, startDate, endDate, maxCapacity, minCapacity " +
                "FROM tournaments");

            if (!reader.HasRows) return tournaments.Values.ToList();
            var registeredPlayers = _userRepository.GetAllRegisteredPlayers();
            while (reader.Read())
            {
                var id = reader.GetGuid("id");
                var tournament = new Tournament
                {
                    ID = id,
                    TournamentSystem = Tournament
                            .GetTournamentTypes()
                            .FirstOrDefault(type =>
                                type.Name == reader.GetString("tournamentSystem")),
                    SportType = Enum.Parse<SportType>(reader.GetString("sportType"), true),
                    Location = reader.GetString("location"),
                    Description = reader.GetString("description"),
                    DateRange = (reader.GetDateTime("startDate"),
                                 reader.GetDateTime("endDate")),
                    CapacityRange = (reader.GetInt32("minCapacity"),
                                     reader.GetInt32("maxCapacity")),
                    Players = registeredPlayers.TryGetValue(id, out List<User>? users) ? users
                        : new List<User>()
                };
                _scheduleRepository.GetAndSetTournamentSchedule(tournament);
                tournaments.Add(id, tournament);
            }

            return tournaments.Values.ToList();
        }
        public Tournament? GetTournamentBy(Guid id)
        {
            using var reader = MySqlHelper.ExecuteReader(_conString,
                "SELECT sportType, tournamentSystem, location, description, startDate, endDate, maxCapacity, minCapacity" +
                "FROM tournaments WHERE id = @id",
                new MySqlParameter("@id", id));
            if (!reader.Read()) return null;
            var tournament = new Tournament
            {
                ID = id,
                TournamentSystem = Tournament
                        .GetTournamentTypes()
                        .FirstOrDefault(type =>
                            type.Name == reader.GetString("tournamentSystem")),
                SportType = (SportType)reader.GetInt32("sportType"),
                Location = reader.GetString("location"),
                Description = reader.GetString("description"),
                DateRange = (reader.GetDateTime("startDate"),
                                 reader.GetDateTime("endDate")),
                CapacityRange = (reader.GetInt32("minCapacity"),
                                 reader.GetInt32("maxCapacity")),
                Players = _userRepository.GetPlayersByTournament(id).ToList()
            };
            _scheduleRepository.GetAndSetTournamentSchedule(tournament);
            return tournament;
        }
        public bool RegisterPlayer(Tournament tournament, User user)
        {
            try
            {
                MySqlHelper.ExecuteNonQuery(_conString,
                    "INSERT INTO users_tournaments (tournament_id, user_id)" +
                    "VALUES (@tid, @uid)",
                    new MySqlParameter("@tid", tournament.ID),
                    new MySqlParameter("@uid", user.ID));
                return true;
            }
            catch (MySqlException ex) when (ex.Number == 1062)
            {
                return false;
            }
        }
        public bool WithdrawPlayer(Tournament tournament, User user)
        {
            return MySqlHelper.ExecuteNonQuery(_conString,
                "DELETE FROM users_tournaments " +
                "WHERE tournament_id = @tid AND user_id = @uid",
                new MySqlParameter("@tid", tournament.ID),
                new MySqlParameter("@uid", user.ID))
                > 0;
        }
        public bool UpdateTournament(Tournament tournament)
        {
            try
            {
                MySqlHelper.ExecuteNonQuery(_conString,
                    "UPDATE tournaments SET sportType = @sportType, tournamentSystem = @tournamentSystem, location = @location, description = @description, startDate = @startDate, endDate = @endDate, maxCapacity = @maxCapacity, minCapacity = @minCapacity WHERE id = @id",
                    new MySqlParameter("@id", tournament.ID.ToByteArray()),
                    new MySqlParameter("@sportType", tournament.SportType),
                    new MySqlParameter("@tournamentSystem", tournament.TournamentSystem.Name),
                    new MySqlParameter("@location", tournament.Location),
                    new MySqlParameter("@description", tournament.Description),
                    new MySqlParameter("@startDate", tournament.DateRange.Start),
                    new MySqlParameter("@endDate", tournament.DateRange.End),
                    new MySqlParameter("@maxCapacity", tournament.CapacityRange.Max),
                    new MySqlParameter("@minCapacity", tournament.CapacityRange.Min));
                return true;
            }
            catch (MySqlException ex) when (ex.Number == 1062)
            {
                return false;
            }
        }
        public bool DeleteTournament(Tournament tournament)
        {
            return MySqlHelper.ExecuteNonQuery(_conString,
                "DELETE FROM tournaments WHERE id = @id",
                new MySqlParameter("@id", tournament.ID))
                > 0;
        }
    }
}
