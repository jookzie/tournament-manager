using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Modules.Enums;
using Modules.Interfaces.BLL;
using System.Data;
using WebApp.DTOs;

namespace WebApp.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public UserRegister UserRegister { get; set; }

        private readonly IUserManager _userManager;

        public RegisterModel(IUserManager userManager)
        {
            _userManager = userManager;
        }
        public void OnGet()
        {
            Thread.Sleep(100);
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();
            try
            {
                _userManager.AddUser(
                    UserRegister.ID,
                    UserRegister.Name,
                    UserRegister.Email,
                    UserRegister.Password,
                    AccountType.User);
                return RedirectToPage("Login");
            }
            catch (Exception ex) when (ex is ArgumentOutOfRangeException ||
                                       ex is FormatException ||
                                       ex is DuplicateNameException ||
                                       ex is ArgumentNullException)
            {
                TempData["ErrorMessage"] = ex.Message;
                return Page();
            }
        }

    }
}
