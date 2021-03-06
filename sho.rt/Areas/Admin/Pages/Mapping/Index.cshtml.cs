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
    public class IndexModel : PageModel
    {
        private readonly sho.rt.Data.ApplicationDbContext _context;

        public IndexModel(sho.rt.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Model.Mapping> Mapping { get; set; }

        public async Task OnGetAsync()
        {
            Mapping = await _context.Mapping.Include(m => m.Owner).ToListAsync();
        }
    }
}
