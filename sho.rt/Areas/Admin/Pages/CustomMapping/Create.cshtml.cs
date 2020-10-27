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
    public class CreateModel : PageModel
    {
        private readonly sho.rt.Data.ApplicationDbContext _context;

        public CreateModel(sho.rt.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["Owner"] = new SelectList(_context.Users, "Id", "Email");
            return Page();
        }

        [BindProperty]
        public Model.CustomMapping CustomMapping { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.CustomMapping.Add(CustomMapping);
            await _context.Database.OpenConnectionAsync();
            try
            {
                await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.CustomMapping ON");
                await _context.SaveChangesAsync();
                await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.CustomMapping OFF");
            }
            finally
            {
                await _context.Database.CloseConnectionAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
