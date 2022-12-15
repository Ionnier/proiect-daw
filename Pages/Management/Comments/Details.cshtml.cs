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
    public class DetailsModel : PageModel
    {
        private readonly ProiectDAW.Context.ApplicationDbContext _context;

        public DetailsModel(ProiectDAW.Context.ApplicationDbContext context)
        {
            _context = context;
        }

      public Comment Comment { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null || _context.comments == null)
            {
                return NotFound();
            }

            var comment = await _context.comments.FirstOrDefaultAsync(m => m.CommentId == id);
            if (comment == null)
            {
                return NotFound();
            }
            else 
            {
                Comment = comment;
            }
            return Page();
        }
    }
}
