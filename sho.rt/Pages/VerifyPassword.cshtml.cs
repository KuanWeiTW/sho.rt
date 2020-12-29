using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using sho.rt.Data;
using sho.rt.Helper;
using sho.rt.Model;

namespace sho.rt.Pages
{
    public class VerifyPasswordModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public string ErrorMessage { get; private set; } = "";
        public string ShortenedUrl { get; private set; } = "";
        public string FormAction { get; private set; } = "";
        public VerifyPasswordModel(ILogger<IndexModel> logger, ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGet(string shortenedUrl)
        {
            if (shortenedUrl.Length > 5)
            {
                var mapping = await _context.CustomMapping.FindAsync(Base62.Decode(shortenedUrl));
                if(mapping.MappingType==MappingType.URL)
                {
                    FormAction = "/VerifyPassword";
                }
                else if (mapping.MappingType == MappingType.IMAGE)
                {
                    FormAction = "/ImageContent";
                }
                else if(mapping.MappingType==MappingType.VIDEO)
                {
                    FormAction = "/VideoContent";
                }
                else if (mapping.MappingType == MappingType.AUDIO)
                {
                    FormAction = "/AudioContent";
                }
            }
            else
            {
                var mapping = await _context.Mapping.FindAsync(Base62.Decode(shortenedUrl));
                if (mapping.MappingType == MappingType.URL)
                {
                    FormAction = "/VerifyPassword";
                }
                else if (mapping.MappingType == MappingType.IMAGE)
                {
                    FormAction = "/ImageContent";
                }
                else if (mapping.MappingType == MappingType.VIDEO)
                {
                    FormAction = "/VideoContent";
                }
                else if (mapping.MappingType == MappingType.AUDIO)
                {
                    FormAction = "/AudioContent";
                }
            }
            ShortenedUrl = shortenedUrl;
            return Page();
        }

        public async Task<IActionResult> OnPost(string shortenedUrl, string password)
        {
            if (shortenedUrl.Length > 5)
            {
                var mapping = await _context.CustomMapping.FindAsync(Base62.Decode(shortenedUrl));
                if (mapping == null)
                {
                    return NotFound();
                }
                else if (mapping.Password == password)
                {
                    return Redirect(mapping.Original);
                }
                else
                {
                    ShortenedUrl = shortenedUrl;
                    ErrorMessage = "Wrong Password!";
                    return Page();
                }
            }
            else
            {
                var mapping = await _context.Mapping.FindAsync(Base62.Decode(shortenedUrl));
                if (mapping == null)
                {
                    return NotFound();
                }
                else if (mapping.Password == password)
                {
                    return Redirect(mapping.Original);
                }
                else
                {
                    ShortenedUrl = shortenedUrl;
                    ErrorMessage = "Wrong Password!";
                    return Page();
                }
            }
        }
    }
}

