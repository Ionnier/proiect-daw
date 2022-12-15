using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProiectDAW.Context;
using ProiectDAWd.Models;

namespace ProiectDAWd.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public List<Category> categories { get; set; } = new List<Category>();

        private readonly ApplicationDbContext applicationDbContext;

        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext applicationDbContext)
        {
            _logger = logger;
            this.applicationDbContext = applicationDbContext;
        }

        public void OnGet()
        {
            categories = applicationDbContext.categories.ToList();

        }
    }
}