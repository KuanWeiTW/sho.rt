using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using sho.rt.Data;
using sho.rt.Model;

namespace sho.rt.Areas.Admin.Pages.CustomMapping
{
    [Authorize(Policy = "RequireAdministratorRole")]
    public class DeleteModel : PageModel
    {
        private readonly sho.rt.Data.ApplicationDbContext _context;

        public DeleteModel(sho.rt.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Model.CustomMapping CustomMapping { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CustomMapping = await _context.CustomMapping
                .Include(c => c.Owner).FirstOrDefaultAsync(m => m.Id == id);

            if (CustomMapping == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CustomMapping = await _context.CustomMapping.FindAsync(id);

            if (CustomMapping != null)
            {
                _context.CustomMapping.Remove(CustomMapping);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
