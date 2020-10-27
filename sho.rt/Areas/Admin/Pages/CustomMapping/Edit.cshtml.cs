using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using sho.rt.Data;
using sho.rt.Model;

namespace sho.rt.Areas.Admin.Pages.CustomMapping
{
    [Authorize(Policy = "RequireAdministratorRole")]
    public class EditModel : PageModel
    {
        private readonly sho.rt.Data.ApplicationDbContext _context;

        public EditModel(sho.rt.Data.ApplicationDbContext context)
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
           ViewData["Owner"] = new SelectList(_context.Users, "Id", "Email");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(CustomMapping).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomMappingExists(CustomMapping.Id))
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

        private bool CustomMappingExists(long id)
        {
            return _context.CustomMapping.Any(e => e.Id == id);
        }
    }
}
