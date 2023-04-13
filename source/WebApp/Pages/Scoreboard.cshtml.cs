using BLL;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Modules.Entities;

namespace WebApp.Pages
{
    public class ScoreboardModel : PageModel
    {
        private TournamentManager _tournamentManager;
        public Tournament Tournament { get; private set; }
        public List<KeyValuePair<User, int>> Scoreboard { get; private set; }
        public ScoreboardModel(TournamentManager tournamentManager)
        {
            _tournamentManager = tournamentManager;
        }
        public void OnGet(string id)
        {
            Tournament = _tournamentManager.GetTournamentBy(Guid.Parse(id));
            Scoreboard = Tournament.GetScoreboard();
        }
    }
}
