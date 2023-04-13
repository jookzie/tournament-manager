namespace Modules.Entities.ScheduleTypes
{
    public record DoubleRoundRobin : ISchedule
    {
        public List<Round> Rounds { get; }
        public DoubleRoundRobin(IEnumerable<Round> rounds)
        {
            Rounds = rounds.ToList();
        }
        public DoubleRoundRobin(IEnumerable<User> players, int nrOfGames = 3)
        {
            // Reference: https://en.wikipedia.org/wiki/Round-robin_tournament#Circle_method
            var playersRound = new List<User>(players);

            // If odd, add null to make it even
            if (playersRound.Count % 2 == 1)
                playersRound.Add(null);

            // Fill the list of rounds
            var rounds = new List<Round>(2 * (playersRound.Count - 1));
            for (int i = 0; i < rounds.Capacity; i++)
                rounds.Add(new Round());

            // Used for shuffling the players and the game order in a round
            var rand = new Random();
            playersRound.OrderBy(player => rand.Next());

            for (int r = 0; r < rounds.Count; r++)
            {
                // Insert last player to the second position (i.e., first position if counting from 0)
                playersRound.Insert(1, playersRound[playersRound.Count - 1]);
                playersRound.RemoveAt(playersRound.Count - 1);

                // Match players on the opposite sides of the list TWICE
                // If a player is matched with a null, assign them as a skipper
                var matches = new List<Match>();
                for (int m = 0; m < playersRound.Count / 2; m++)
                {
                    int player1 = m;
                    int player2 = playersRound.Count - m - 1;
                    if (playersRound[player1] is null)
                        rounds[r].Skipper = playersRound[player2];
                    else if (playersRound[player2] is null)
                        rounds[r].Skipper = playersRound[player1];
                    else
                    {
                        if(r > (rounds.Count - 1) / 2)
                            matches.Add(new Match(playersRound[player1], playersRound[player2], nrOfGames));
                        else
                            matches.Add(new Match(playersRound[player2], playersRound[player1], nrOfGames));
                    }
                }
                rounds[r].Matches.AddRange(matches);
            }
            Rounds = rounds;
        }
    }
}
