using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProiectDAW.Context;
using ProiectDAW.Models;

namespace ProiectDAW.Pages.Management.Comments
{
    public class IndexModel : PageModel
    {
        private readonly ProiectDAW.Context.ApplicationDbContext _context;

        public IndexModel(ProiectDAW.Context.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Comment> Comment { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.comments != null)
            {
                Comment = await _context.comments
                .Include(c => c.Post)
                .Include(c => c.User).ToListAsync();
            }
        }
    }
}
