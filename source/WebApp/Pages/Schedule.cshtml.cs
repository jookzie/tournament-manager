using BLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Modules.Entities;

namespace WebApp.Pages
{
    public class ScheduleModel : PageModel
    {
        private TournamentManager _tournamentManager;
        public Tournament Tournament { get; private set; }
        public ScheduleModel(TournamentManager tournamentManager)
        {
            _tournamentManager = tournamentManager;
        }
        public void OnGet(string id)
        {
            Tournament = _tournamentManager.GetTournamentBy(Guid.Parse(id));
        }
    }
}
