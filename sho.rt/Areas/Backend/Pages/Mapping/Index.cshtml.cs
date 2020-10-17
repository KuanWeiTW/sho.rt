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
    public class IndexModel : PageModel
    {
        private readonly sho.rt.Data.ApplicationDbContext _context;

        public IndexModel(sho.rt.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Mapping> Mapping { get;set; }

        public async Task OnGetAsync()
        {
            Mapping = await _context.Mapping.ToListAsync();
        }
    }
}
