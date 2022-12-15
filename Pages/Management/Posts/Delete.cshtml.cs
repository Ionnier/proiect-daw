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
    public class DeleteModel : PageModel
    {
        private readonly ProiectDAW.Context.ApplicationDbContext _context;

        public DeleteModel(ProiectDAW.Context.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Post Post { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.posts == null)
            {
                return NotFound();
            }

            var post = await _context.posts.FirstOrDefaultAsync(m => m.PostId == id);

            if (post == null)
            {
                return NotFound();
            }
            else 
            {
                Post = post;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.posts == null)
            {
                return NotFound();
            }
            var post = await _context.posts.FindAsync(id);

            if (post != null)
            {
                Post = post;
                _context.posts.Remove(Post);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
