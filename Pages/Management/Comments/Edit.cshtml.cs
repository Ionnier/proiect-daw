using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProiectDAW.Context;
using ProiectDAW.Models;

namespace ProiectDAW.Pages.Management.Comments
{
    public class EditModel : PageModel
    {
        private readonly ProiectDAW.Context.ApplicationDbContext _context;

        public EditModel(ProiectDAW.Context.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Comment Comment { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null || _context.comments == null)
            {
                return NotFound();
            }

            var comment =  await _context.comments.FirstOrDefaultAsync(m => m.CommentId == id);
            if (comment == null)
            {
                return NotFound();
            }
            Comment = comment;
           ViewData["PostId"] = new SelectList(_context.posts, "PostId", "PostTitle");
           ViewData["UserId"] = new SelectList(_context.profiles, "UserId", "UserId");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Comment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(Comment.CommentId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CommentExists(long id)
        {
          return _context.comments.Any(e => e.CommentId == id);
        }
    }
}
