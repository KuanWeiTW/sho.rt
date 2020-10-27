using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using sho.rt.Data;
using sho.rt.Model;

namespace sho.rt.Areas.Backend.Pages.CustomMapping
{
    public class DeleteModel : PageModel
    {
        private readonly sho.rt.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public DeleteModel(sho.rt.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Model.CustomMapping CustomMapping { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(HttpContext.User);
            CustomMapping = await _context.CustomMapping.FirstOrDefaultAsync(m => m.Id == id && m.Owner == user);

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
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var target = _context.CustomMapping.Find(CustomMapping.Id);
            if (user != target.Owner)
            {
                return Unauthorized();
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
