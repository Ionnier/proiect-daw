using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProiectDAW.Context;
using ProiectDAW.Models;

namespace ProiectDAW.Pages
{
    public class PostModel : PageModel
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        [BindProperty]
        public Post post { get; set; } = new Post();

        public PostModel(ApplicationDbContext applicationDbContext, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            this.applicationDbContext = applicationDbContext;
            this._signInManager= signInManager;
            this._userManager= userManager;
        }
        public IActionResult OnGet(int id)
        {
           
		    this.post = applicationDbContext.posts
						    .Where(x => x.PostId == id)
							.Include(x => x.User)
							.Include(x => x.comments)
							.ThenInclude(x => x.User)
                            .First();
            this.post.comments.Sort((a, b) => a.CreatedDate.CompareTo(b.CreatedDate));
			return Page();
        }

        [BindProperty]
        public Comment comment { get; set; }

        public IActionResult OnPost()
        {
            if (!_signInManager.IsSignedIn(User))
            {
                return Unauthorized();
            }

			var user = _userManager.FindByNameAsync(User.Identity.Name).GetAwaiter().GetResult();

            comment.UserId = user.Id; 
			applicationDbContext.comments.Add(comment);
            applicationDbContext.SaveChanges();
            return OnGet(comment.PostId);
		}
    }
}
