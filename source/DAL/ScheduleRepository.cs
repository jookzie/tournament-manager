using Modules.Entities;
using Modules.Interfaces.DAL;
using MySql.Data.MySqlClient;

namespace DAL
{
    // not one of my proudest lines of code
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly string _conString;
        private readonly IUserRepository _userRepository;
        public ScheduleRepository(string conString, IUserRepository userRepository)
        {
            _conString = conString;
            _userRepository = userRepository;
        }

        public void OverrideSchedule(Tournament tournament)
        {
            DeleteSchedule(tournament);
            
            string query =
                "INSERT INTO schedule(tournament_id, round_num, match_num, game_num, player1_id, player2_id, score1, score2, skipper_id) VALUES ";

            int gameIndex = 0;
            int matchIndex = 0;
            var schedule = tournament.Schedule;
            for (int r = 0; r < schedule.Rounds.Count; r++)
            {
                for (int m = 0; m < schedule.Rounds[r].Matches.Count; m++)
                {
                    for (int g = 0; g < schedule.Rounds[r].Matches[m].Games.Count; g++)
                    {
                        query += $"(@id, @round_num{r}, @match_num{matchIndex}, @game_num{gameIndex}, " +
                                 $"@player1_id{matchIndex}, @player2_id{matchIndex}, " +
                                 $"@score1{gameIndex}, @score2{gameIndex}, @skipper_id{r}),";
                        gameIndex++;
                    }
                    matchIndex++;
                }
            }
            query = query.Remove(query.Length - 1, 1) + ";";

            var cmd = new MySqlCommand(query);
            cmd.Parameters.AddWithValue("@id", tournament.ID);
            gameIndex = 0;
            matchIndex = 0;
            for (int r = 0; r < schedule.Rounds.Count; r++)
            {
                var round = schedule.Rounds[r];
                cmd.Parameters.AddWithValue($"@round_num{r}", r);
                if (round.Skipper is null)
                    cmd.Parameters.AddWithValue($"@skipper_id{r}", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue($"@skipper_id{r}", round.Skipper.ID);
                for (int m = 0; m < round.Matches.Count; m++)
                {
                    var match = round.Matches[m];
                    cmd.Parameters.AddWithValue($"@match_num{matchIndex}", matchIndex);
                    cmd.Parameters.AddWithValue($"@player1_id{matchIndex}", match.Players.First.ID);
                    cmd.Parameters.AddWithValue($"@player2_id{matchIndex}", match.Players.Second.ID);
                    for (int g = 0; g < match.Games.Count; g++)
                    {
                        cmd.Parameters.AddWithValue($"@game_num{gameIndex}", gameIndex);
                        cmd.Parameters.AddWithValue($"@score1{gameIndex}", match.Games[g].Scores.First);
                        cmd.Parameters.AddWithValue($"@score2{gameIndex}", match.Games[g].Scores.Second);
                        gameIndex++;
                    }
                    matchIndex++;
                }
            }
            using var con = new MySqlConnection(_conString);
            con.Open();
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
        }

        public bool GetAndSetTournamentSchedule(Tournament tournament)
        {
            var players = _userRepository
                .GetPlayersByTournament(tournament.ID)
                .ToDictionary(p => p.ID, p => p);

            using var reader = MySqlHelper.ExecuteReader(_conString,
                "SELECT round_num, match_num, game_num, player1_id, player2_id, score1, score2,skipper_id "+
                "FROM schedule " +
                "WHERE tournament_id = @id " +
                "ORDER BY round_num, match_num, game_num",
                new MySqlParameter("@id", tournament.ID));
            if (!reader.HasRows) return false;

            var rounds = new List<Round>();
            int currentRoundIndex = -1;
            int currentMatchIndex = -1;
            while (reader.Read())
            {
                if (currentRoundIndex != reader.GetInt32("round_num"))
                {
                    rounds.Add(new Round());
                    currentRoundIndex = reader.GetInt32("round_num");
                }
                if (currentMatchIndex != reader.GetInt32("match_num"))
                {
                    rounds.Last().Matches.Add(new Match(
                    players[reader.GetGuid("player1_id")],
                    players[reader.GetGuid("player2_id")]));
                    currentMatchIndex = reader.GetInt32("match_num");
                }
                // don't ask why, this is rather readable than having it in a few lines
                rounds.Last()
                    .Matches.Last()
                        .Games.Add
                        (
                            new Game
                            (
                                (
                                    players[reader.GetGuid("player1_id")],
                                    players[reader.GetGuid("player2_id")]
                                ),
                                (
                                    reader.GetInt32("score1"), 
                                    reader.GetInt32("score2")
                                )
                            )
                        );
                
            }
            tournament.SetScheduleBy(rounds);
            return true;
        }
        public bool DeleteSchedule(Tournament tournament)
        {
            return MySqlHelper.ExecuteNonQuery(_conString,
                "DELETE FROM schedule WHERE tournament_id = @id",
                new MySqlParameter("@id", tournament.ID))
                > 0;
        }
    }
}
