using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Base62;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using sho.rt.Data;
using sho.rt.Model;

namespace sho.rt.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public string ErrorMessage { get; private set; } = "";
        public string ShortenedUrl { get; private set; } = "";
        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        public IActionResult OnGet(string shortenedUrl)
        {
            if (shortenedUrl == null)
            {
                return Page();
            }
            else
            {
                var mapping = _context.Mapping.FirstOrDefault(m => m.ShortenedUrl == shortenedUrl);
                if (mapping == null)
                {
                    ErrorMessage = "Not Found";
                    return Page();
                }
                if(!string.IsNullOrWhiteSpace(mapping.Password))
                {
                    return RedirectToPage("./VerifyPassword", new { shortenedUrl = shortenedUrl });
                }
                return Redirect(mapping.OriginalUrl);
            }
        }

        public async Task<IActionResult> OnPost(string url, string password)
        {
            if (url == null)
            {
                ErrorMessage = "url is empty";
            }
            else if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
            {
                ErrorMessage = "url is invalid";
            }
            else
            {
                Mapping mapping = new Mapping
                {
                    OriginalUrl = url,
                    ShortenedUrl = Guid.NewGuid().ToString(),
                    Password = password,
                    Owner = await _userManager.GetUserAsync(HttpContext.User)
                };

                _context.Add(mapping);
                await _context.SaveChangesAsync();
                mapping.ShortenedUrl = mapping.Id.ToBase62();
                _context.Update(mapping);
                await _context.SaveChangesAsync();
                ShortenedUrl = Url.Page("/Index", null, new { }, protocol: Request.Scheme) + mapping.ShortenedUrl;
            }
            return Page();
        }
    }
}

