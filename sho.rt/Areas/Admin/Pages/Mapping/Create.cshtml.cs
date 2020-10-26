using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using sho.rt.Data;
using sho.rt.Model;

namespace sho.rt.Areas.Admin
{
    [Authorize(Policy = "RequireAdministratorRole")]
    public class CreateModel : PageModel
    {
        private readonly sho.rt.Data.ApplicationDbContext _context;

        public CreateModel(sho.rt.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Mapping Mapping { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Mapping.Add(Mapping);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
