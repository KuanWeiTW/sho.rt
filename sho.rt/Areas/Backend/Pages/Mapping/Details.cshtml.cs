using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using sho.rt.Data;
using sho.rt.Model;

namespace sho.rt.Areas.Backend
{
    public class DetailsModel : PageModel
    {
        private readonly sho.rt.Data.ApplicationDbContext _context;

        public DetailsModel(sho.rt.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Mapping Mapping { get; set; }

        public async Task<IActionResult> OnGetAsync(uint? id)
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
    }
}
