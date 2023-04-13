namespace Modules.Entities
{
    public class Game
    {
        public Game((User, User) players)
        {
            _players = players;
        }
        public Game((User, User) players, (int, int) scores) : this(players)
        {
            _players = players;
            _scores = scores;
        }
        public (int First, int Second) Scores
        {
            get => _scores;
            set
            {
                // allow if both values are zero
                if (value.First == value.Second && value.First == 0 ||
                    // allow if there's a special case of 30 - 29 result
                    value.First == 30 && value.Second == 29 || value.First == 29 && value.Second == 30)
                {
                    _scores = value;
                    return;
                }


                var referenceEx = $"\nReference: {_players.First} - {_players.Second} {value.First} - {value.Second}.";

                if (value.First > 30 || value.Second > 30 || value.First < 0 || value.Second < 0)
                    throw new ArgumentException($"Score results should be values between 0 and 30.{referenceEx}");

                if (Math.Max(value.First, value.Second) < 21)
                    throw new ArgumentException($"Atleast one player needs to have a score higher than 20.{referenceEx}");

                if (value.First < 20 && value.Second > 21 || value.Second < 20 && value.First > 21)
                    throw new ArgumentException($"A player cannot have more than 21 points, unless the other player has atleast 20 points.{referenceEx}");

                if (value.First >= 20 && value.Second >= 20 && Math.Abs(value.First - value.Second) != 2)
                    throw new ArgumentException($"Players cannot have a difference other than 2 points, unless one's score is lower than 20 or it's a 29 - 30.{referenceEx}");

                _scores = value;
            }
        }
        public User Winner
        {
            get
            {
                if (Scores.First == Scores.Second && Scores.First == 0)
                    return default;
                else
                    return Scores.First > Scores.Second ? _players.First : _players.Second;
            }
        }
        private (User First, User Second) _players  = new();
        private (int, int ) _scores = new();
        
    }
}
