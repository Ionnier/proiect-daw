using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProiectDAW.Context;

namespace ProiectDAW.Pages
{
    public class SignupModel : PageModel
    {
        public UserManager<IdentityUser> UserManager;
        private readonly ApplicationDbContext dbContext;
        public SignupModel(UserManager<IdentityUser> usrManager, ApplicationDbContext dbContext)
        {
            UserManager = usrManager;
            this.dbContext = dbContext;
        }

        [BindProperty]
		public string UserName { get; set; } = string.Empty;

        [BindProperty]
        [EmailAddress]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; } = string.Empty;

        [BindProperty]
		[DataType(DataType.Password)]
		public string Password { get; set; } = string.Empty;

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
				var user = new IdentityUser
				{
					UserName = UserName,
					Email = Email
				};
				var result = UserManager.CreateAsync(user, Password).GetAwaiter().GetResult();
                if (result.Succeeded)
                {
                    var user2 = UserManager.FindByNameAsync(UserName).GetAwaiter().GetResult();
                    if (user2 != null)
                    {
                         dbContext.profiles.Add(
                            new Models.Profile
                            {
                                UserId = user2.Id,
                                UserName= user2.UserName,
                            }
                        );
                        dbContext.SaveChanges();
                    }
                    return RedirectToPage("Login");
                }
                foreach (IdentityError err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
            }
            return Page();
        }   
    }
}
