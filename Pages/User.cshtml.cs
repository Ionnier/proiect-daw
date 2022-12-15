using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProiectDAW.Context;
using ProiectDAW.Models;

namespace ProiectDAW.Pages
{
    public class UserModel : PageModel
    {

        private readonly ApplicationDbContext _context;

        [BindProperty]
        public Profile? profile { get; set; } = new Profile();


        public UserModel(ApplicationDbContext applicationDbContext) {
            _context= applicationDbContext;
        }

        public void OnGet(string Id)
        {
            try
            {
                profile = _context.profiles.Where(el => el.UserId == Id).Include(el => el.posts)
				.Include(el => el.posts)
				.Include(el => el.comments).First();
			} catch (Exception ex)
            {
                profile = null;
            }
            
		}
    }
}
