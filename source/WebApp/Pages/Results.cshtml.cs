using BLL;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Modules.Entities;

namespace WebApp.Pages
{
    public class ResultsModel : PageModel
    {
        private TournamentManager _tournamentManager;
        public Tournament Tournament { get; private set; }
        public ResultsModel(TournamentManager tournamentManager)
        {
            _tournamentManager = tournamentManager;
        }
        public void OnGet(string id)
        {
            Tournament = _tournamentManager.GetTournamentBy(Guid.Parse(id));
        }
    }
}
