namespace Modules.Entities
{
    public class Match
    {
        public Match(User first, User second)
        {
            Players = new(first, second);
        }
        public Match(User first, User second, int nrOfGames) : this(first, second)
        {
            if (nrOfGames < 1)
                throw new ArgumentException("Number of games must be a positive number.");
            for (int i = 0; i < nrOfGames; i++)
            {
                Games.Add(new Game((first, second)));
            }
        }
        public List<Game> Games { get; } = new();
        public (User First, User Second) Players
        {
            get => _players;
            init
            {
                if (value.First.Equals(value.Second))
                    throw new InvalidOperationException("Match must have 2 different players");
                _players = value;
            }
        }
        public User Winner
        {
            get
            {
                // int is amount of wins
                IDictionary<User, int> userWins = new Dictionary<User, int>();
                foreach (Game game in Games)
                {
                    if (game.Winner != null)
                    {
                        userWins.TryGetValue(game.Winner, out int wins);
                        userWins[game.Winner] = wins + 1;
                    }
                }

                return userWins.FirstOrDefault(userWin => userWin.Value >= Math.Ceiling(Games.Count / 2d)).Key;
            }
        }
        private (User, User) _players;
    }
}
