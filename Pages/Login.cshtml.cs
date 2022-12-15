using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ProiectDAW.Pages
{
    public class LoginModel : PageModel
    {
		private readonly SignInManager<IdentityUser> _signInManager;

		public LoginModel(SignInManager<IdentityUser> signInManager)
		{
			_signInManager = signInManager;
		}

		[BindProperty]
		public string UserName { get; set; } = string.Empty;

		[BindProperty]
		[DataType(DataType.Password)]
		public string Password { get; set; } = string.Empty;

		public IActionResult OnGet()
        {
			if (_signInManager.IsSignedIn(User))
			{
				return RedirectToPage("Index");
			}
			return Page();
        }

		public IActionResult OnPost()
		{
            if (_signInManager.IsSignedIn(User))
            {
                return RedirectToPage("Index");
            }

            if (ModelState.IsValid)
			{
				var result = _signInManager.PasswordSignInAsync(
					UserName,
					Password,
					false,
					false
				).GetAwaiter().GetResult();
				if (result.Succeeded)
				{
					return RedirectToPage("Index");
				}
				ModelState.AddModelError("", "Invalid username or password");
			}
			return Page();
		}
	
	}
}
