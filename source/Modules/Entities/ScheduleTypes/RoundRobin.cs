namespace Modules.Entities.ScheduleTypes
{
    public record RoundRobin : ISchedule
    {
        public List<Round> Rounds { get; }


        public RoundRobin(IEnumerable<Round> rounds)
        {
            Rounds = rounds.ToList();
        }
        public RoundRobin(IEnumerable<User> players, int nrOfGames = 3)
        {
            // Reference: https://en.wikipedia.org/wiki/Round-robin_tournament#Circle_method
            var playersRound = new List<User>(players);

            // If odd, add null to make it even
            if (playersRound.Count % 2 == 1)
                playersRound.Add(default);

            // Fill the list of rounds
            var rounds = new List<Round>(playersRound.Count - 1);
            for (int i = 0; i < rounds.Capacity; i++)
                rounds.Add(new Round());

            // Used for shuffling the game order in a round
            playersRound.OrderBy(player => Random.Shared.Next());

            foreach (var round in rounds)
            {
                // Insert last player to the second position (i.e., first position if counting from 0)
                playersRound.Insert(1, playersRound[playersRound.Count - 1]);
                playersRound.RemoveAt(playersRound.Count - 1);

                // Match players on the opposite sides of the list
                // If a player is matched with a null, assign them as a skipper
                var matches = new List<Match>();
                for (int m = 0; m < playersRound.Count / 2; m++)
                {
                    int player1 = m;
                    int player2 = playersRound.Count - m - 1;
                    if (playersRound[player1] is null)
                        round.Skipper = playersRound[player2];
                    else if (playersRound[player2] is null)
                        round.Skipper = playersRound[player1];
                    else
                        matches.Add(new Match(playersRound[player1], playersRound[player2], nrOfGames));
                }
                // Shuffle the game order
                round.Matches.AddRange(matches.OrderBy(x => Random.Shared.Next()));
            }
            Rounds = rounds;
        }
    }
}
