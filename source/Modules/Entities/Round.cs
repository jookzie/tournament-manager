namespace Modules.Entities
{
    public class Round
    {
        public List<Match> Matches { get; } = new List<Match>();
        public User Skipper { get; set; }
    }
}
