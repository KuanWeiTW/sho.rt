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

namespace sho.rt.Areas.Backend
{
    public class IndexModel : PageModel
    {
        private readonly sho.rt.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel(sho.rt.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Mapping> Mapping { get; set; }

        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            Mapping = await _context.Mapping.Where(m => m.Owner == user).ToListAsync();
        }
    }
}
