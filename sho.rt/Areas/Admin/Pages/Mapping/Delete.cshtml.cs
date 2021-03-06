﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using sho.rt.Data;
using sho.rt.Model;

namespace sho.rt.Areas.Admin.Pages.Mapping
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
        public Model.Mapping Mapping { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Mapping = await _context.Mapping.FirstOrDefaultAsync(m => m.Id == id);

            if (Mapping == null)
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

            Mapping = await _context.Mapping.FindAsync(id);

            if (Mapping != null)
            {
                _context.Mapping.Remove(Mapping);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
