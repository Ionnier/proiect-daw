using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProiectDAW.Context;
using ProiectDAW.Models;

namespace ProiectDAW.Pages
{
    [Authorize]
    public class ProfileModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<IdentityUser> signInManager;
		private readonly UserManager<IdentityUser> _userManager;
		[BindProperty]
        public Profile profile { get; set; }

        public ProfileModel(ApplicationDbContext context, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager) 
        {
            _context= context;
            this.signInManager= signInManager;
            _userManager= userManager;
        }
        public IActionResult OnGet()
        {
			var user = _userManager.FindByNameAsync(User.Identity.Name).GetAwaiter().GetResult();
            var profile = _context.profiles
                .Where(el => el.UserId== user.Id)
                .Include(el => el.posts)
			    .Include(el => el.comments)
                .First();
            if (profile == null)
            {
                return RedirectToPage("Index");
            }
			this.profile = profile;
            return Page();
		}

        public IActionResult OnPost()
        { 
            var user = _context.profiles.Find(profile.UserId);

            if (!profile.AvatarUri.IsNullOrEmpty())
            {
                user.AvatarUri = profile.AvatarUri;
            }

			if (!profile.UserName.IsNullOrEmpty())
			{
				user.UserName = profile.UserName;
			}

			if (!profile.Description.IsNullOrEmpty()) 
            {
				user.Description = profile.Description;
			}

			_context.profiles.Update(
                user
            );

            _context.SaveChanges();

            return RedirectToPage("Profile");
        }
	}
}
