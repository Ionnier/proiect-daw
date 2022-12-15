using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ProiectDAW.Pages
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<IdentityUser> signInManager;
        public LogoutModel(SignInManager<IdentityUser> signInManager) { this.signInManager = signInManager; }
        public IActionResult OnGet()
        {
            signInManager.SignOutAsync().GetAwaiter();
            return RedirectToPage("Index");
        }
    }
}
