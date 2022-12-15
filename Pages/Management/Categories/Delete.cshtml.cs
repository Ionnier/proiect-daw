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
    public class DeleteModel : PageModel
    {
        private readonly ProiectDAW.Context.ApplicationDbContext _context;

        public DeleteModel(ProiectDAW.Context.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Category Category { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null || _context.categories == null)
            {
                return NotFound();
            }

            var category = await _context.categories.FirstOrDefaultAsync(m => m.CategoryId == id);

            if (category == null)
            {
                return NotFound();
            }
            else 
            {
                Category = category;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null || _context.categories == null)
            {
                return NotFound();
            }
            var category = await _context.categories.FindAsync(id);

            if (category != null)
            {
                Category = category;
                _context.categories.Remove(Category);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
