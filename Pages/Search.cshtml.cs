using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProiectDAW.Context;
using ProiectDAW.Models;

namespace ProiectDAW.Pages
{
    public class SearchModel : PageModel
    {
        [BindProperty]
        public string QueryString { get; set; } = string.Empty;

        public List<Post> postsTitle { get; set; } = new List<Post>();

        public List<Post> postsContent { get; set; } = new List<Post>();

        private readonly ApplicationDbContext applicationDbContext;
        public SearchModel(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
       
        public IActionResult OnPost() {
            if (QueryString == null || QueryString.Length == 0)
            {
                return Redirect("Index");
            }

            postsTitle = applicationDbContext.posts.Where(el => el.PostTitle.ToLower().IndexOf(QueryString.ToLower()) != -1).ToList();
            postsContent = applicationDbContext.posts.Where(el => el.PostBody.ToLower().IndexOf(QueryString.ToLower()) != -1).ToList();

            return Page();
        }    
    }
}
