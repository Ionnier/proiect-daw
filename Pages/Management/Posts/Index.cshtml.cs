using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProiectDAW.Context;
using ProiectDAW.Models;

namespace ProiectDAW.Pages.Management.Posts
{
    public class IndexModel : PageModel
    {
        private readonly ProiectDAW.Context.ApplicationDbContext _context;

        public IndexModel(ProiectDAW.Context.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Post> Post { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.posts != null)
            {
                Post = await _context.posts
                .Include(p => p.Category)
                .Include(p => p.User).ToListAsync();
            }
        }
    }
}
