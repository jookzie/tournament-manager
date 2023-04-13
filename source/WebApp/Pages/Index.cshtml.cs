using BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Modules.Entities;
using Modules.Interfaces.BLL;
using System.Security.Claims;

namespace WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly TournamentManager _tournamentManager;
        private readonly IUserManager _userManager;
        public IList<Tournament> Tournaments { get; }
        public User AuthenticatedUser { get; private set; }
        public IndexModel(TournamentManager tournamentManager, IUserManager userManager)
        {
            _tournamentManager = tournamentManager;
            _userManager = userManager;
            Tournaments = _tournamentManager.GetAllTournaments().ToList();
        }
        public void OnGet()
        {
            AuthenticatedUser = GetAuthenticatedUser();
        }

        public IActionResult OnPostRegister(string tournID)
        {
            AuthenticatedUser = GetAuthenticatedUser();
            if (AuthenticatedUser is null)
                return new RedirectToPageResult("Login");
            try
            {
                if(!_tournamentManager.RegisterPlayer(Guid.Parse(tournID), AuthenticatedUser.ID))
                {
                    TempData["ErrorMessage"] = "You are already in this tournament.";
                }
                //For debugging purposes, schedules the tournament and fills the results
                //_tournamentManager.GetTournamentBy(Guid.Parse(tournID)).GenerateSchedule();
                //foreach (var round in _tournamentManager.GetTournamentBy(Guid.Parse(tournID)).Schedule.Rounds)
                //{
                //    foreach (var match in round.Matches)
                //    {
                //        match.Scores = new int[2] { 21, 0 };
                //    }
                //}
            }
            catch (InvalidOperationException)
            {
                TempData["ErrorMessage"] =
                    "You are already registered for another tournament with overlapping dates.";
            }
            return RedirectToPage("Index");
        }
        public IActionResult OnPostWithdraw(string tournID)
        {
            AuthenticatedUser = GetAuthenticatedUser();
            if (AuthenticatedUser is null)
                return RedirectToPage("Login");
            _tournamentManager.WithdrawPlayer(Guid.Parse(tournID), AuthenticatedUser.ID);
            return RedirectToPage("Index");
        }
        private User GetAuthenticatedUser()
        {
            if (User is not null && User.Identity.IsAuthenticated)
            {
                var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
                return _userManager.GetUserBy(email);
            }
            return null;
        }

    }
}