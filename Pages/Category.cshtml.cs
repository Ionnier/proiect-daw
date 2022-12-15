using System.Reflection.Metadata.Ecma335;
using Azure.Core.GeoJson;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProiectDAW.Context;
using ProiectDAW.Models;
using ProiectDAWd.Models;

namespace ProiectDAW.Pages
{
    public class CategoryModel : PageModel
    {
        private readonly ApplicationDbContext _context;
		private readonly SignInManager<IdentityUser> _signInManager;
		private readonly UserManager<IdentityUser> _userManager;



        [BindProperty]
		public Category category { get; set; } = new Category();

		[BindProperty]

		public List<Post> posts { get; set; } = new List<Post>();

		[BindProperty]
		public bool ascendent { get; set; } = true;

        [BindProperty]
        public string postTitle { get; set; }

		[BindProperty]
		public string postBody { get; set; }

		[BindProperty]
		public long categoryId { get; set; }

		public CategoryModel(ApplicationDbContext applicationDbContext, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _context = applicationDbContext;
            _signInManager = signInManager;
			_userManager = userManager;
		}

        public IActionResult OnGet(long id, [FromQuery] string? direction)
        {
            var category = _context.categories.Find(id);
            if (category == null) {
                return NotFound();
            }
            this.category = category;
            posts = _context.posts.Where(post => post.CategoryId == id).ToList();
            if (direction == null || direction != "desc")
            {
                ascendent = true;
				posts.Sort((a, b) => a.CreatedDate.CompareTo(b.CreatedDate));
			} else
            {
                ascendent = false;
				posts.Sort((a, b) => a.CreatedDate.CompareTo(b.CreatedDate));
                posts.Reverse();
			}

            return Page();
        }
        public IActionResult OnPost()
        {
			if (!_signInManager.IsSignedIn(User))
            {
                return Unauthorized();
            }

			var user = _userManager.FindByNameAsync(User.Identity.Name).GetAwaiter().GetResult();

			var post = new Post
			{
				PostBody = postBody,
				PostTitle = postTitle,
				CategoryId = categoryId,
				UserId = user.Id,
			};

			if (!ModelState.IsValid)
            {
				var category = _context.categories.Find(categoryId);
				if (category == null)
				{
					return NotFound();
				}
				this.category = category;
				posts = _context.posts.Where(post => post.CategoryId == categoryId).ToList();

				return Page();    
            }


            _context.posts.Add(post);
            _context.SaveChanges();
			return OnGet(categoryId, null);
		}
	}
}
