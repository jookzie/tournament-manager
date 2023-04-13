using BLL;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Modules.Interfaces.BLL;
using System.Security.Claims;
using WebApp.DTOs;

namespace WebApp.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public UserAuth UserAuth { get; set; }
        private readonly Authenticator _authenticator;
        private readonly IUserManager _userManager;
        public LoginModel(Authenticator authenticator, IUserManager userManager)
        {
            _authenticator = authenticator;
            _userManager = userManager;
        }
        public void OnGet()
        {
            Thread.Sleep(100);
        }
        public IActionResult OnPost() // test credentials: username123, password123
        {
            if (!ModelState.IsValid) return Page();
            var user = _userManager.GetUserBy(UserAuth.Email);
            if (user is null || !_authenticator.Authenticate(UserAuth.Email, UserAuth.Password))
            {
                TempData["ErrorMessage"] = "No such user exists or credentials are invalid.";
                return RedirectToPage("Login");
            }
            SetupSessionCookie(user);
            Thread.Sleep(500);
            return RedirectToPage("Index");
        }
        private void SetupSessionCookie(Modules.Entities.User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, "User")
            };
            var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.SignInAsync(new ClaimsPrincipal(claimIdentity));
        }
    }
}
