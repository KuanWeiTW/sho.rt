using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using sho.rt.Data;
using sho.rt.Helper;
using sho.rt.Model;

namespace sho.rt.Pages
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
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
                if (shortenedUrl.Length > 5)
                {
                    var mapping = _context.CustomMapping.Find(Base62.Decode(shortenedUrl));
                    if (mapping == null)
                    {
                        ErrorMessage = "Not Found";
                        return Page();
                    }
                    if (!string.IsNullOrWhiteSpace(mapping.Password))
                    {
                        return RedirectToPage("./VerifyPassword", new { shortenedUrl = shortenedUrl });
                    }
                    return Redirect(mapping.OriginalUrl);
                }
                else
                {
                    var mapping = _context.Mapping.Find(Base62.Decode(shortenedUrl));
                    if (mapping == null)
                    {
                        ErrorMessage = "Not Found";
                        return Page();
                    }
                    if (!string.IsNullOrWhiteSpace(mapping.Password))
                    {
                        return RedirectToPage("./VerifyPassword", new { shortenedUrl = shortenedUrl });
                    }
                    return Redirect(mapping.OriginalUrl);
                }
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
                    Password = password,
                    Owner = await _userManager.GetUserAsync(HttpContext.User)
                };

                _context.Add(mapping);
                await _context.SaveChangesAsync();
                ShortenedUrl = Url.Page("/Index", null, new { shortenedUrl = "" }, protocol: Request.Scheme) + mapping.ShortenedUrl;
            }
            return Page();
        }
    }
}

