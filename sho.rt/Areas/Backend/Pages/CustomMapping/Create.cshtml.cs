using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using sho.rt.Data;
using sho.rt.Model;

namespace sho.rt.Areas.Backend.Pages.CustomMapping
{
    public class CreateModel : PageModel
    {
        private readonly sho.rt.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public CreateModel(sho.rt.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
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
            var user = await _userManager.GetUserAsync(HttpContext.User);
            CustomMapping.Owner = user;
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
