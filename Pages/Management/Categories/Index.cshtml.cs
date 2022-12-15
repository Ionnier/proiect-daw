using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProiectDAW.Context;
using ProiectDAWd.Models;

namespace ProiectDAW.Pages.Management.Categories
{
    public class IndexModel : PageModel
    {
        private readonly ProiectDAW.Context.ApplicationDbContext _context;

        public IndexModel(ProiectDAW.Context.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Category> Category { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.categories != null)
            {
                Category = await _context.categories.ToListAsync();
            }
        }
    }
}
