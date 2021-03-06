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

namespace sho.rt.Pages
{
    public class AudioModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public string AudioSource { get; private set; }
        public string AudioType { get; private set; }

        public AudioModel(ILogger<IndexModel> logger, ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }
        public ActionResult OnGet(string shortenedUrl)
        {
            if (shortenedUrl.Length > 5)
            {
                var mapping = _context.CustomMapping.Find(Base62.Decode(shortenedUrl));
                if (!string.IsNullOrWhiteSpace(mapping.Password))
                {
                    return RedirectToPage("./VerifyPassword", new { shortenedUrl = shortenedUrl });
                }
                else
                {
                    AudioSource = "/StreamSource?shortenedUrl=" + shortenedUrl;
                    AudioType = "audio/" + mapping.Original.Split(".").Last();
                    return Page();
                }
            }
            else
            {
                var mapping = _context.Mapping.Find(Base62.Decode(shortenedUrl));
                if (!string.IsNullOrWhiteSpace(mapping.Password))
                {
                    return RedirectToPage("./VerifyPassword", new { shortenedUrl = shortenedUrl });
                }
                else
                {
                    AudioSource = "/StreamSource?shortenedUrl=" + shortenedUrl;
                    AudioType = "audio/" + mapping.Original.Split(".").Last();
                    return Page();
                }
            }
        }
        public IActionResult OnPost(string shortenedUrl, string password)
        {
            if (shortenedUrl.Length > 5)
            {
                var mapping = _context.CustomMapping.Find(Base62.Decode(shortenedUrl));
                if (password != mapping.Password)
                {
                    return RedirectToPage("./VerifyPassword", new { shortenedUrl = shortenedUrl });
                }
                else
                {
                    AudioSource = "/StreamSource?shortenedUrl=" + shortenedUrl + "password=" + password;
                    AudioType = "audio/" + mapping.Original.Split(".").Last();
                    return Page();
                }
            }
            else
            {
                var mapping = _context.Mapping.Find(Base62.Decode(shortenedUrl));
                if (password != mapping.Password)
                {
                    return RedirectToPage("./VerifyPassword", new { shortenedUrl = shortenedUrl });
                }
                else
                {
                    AudioSource = "/StreamSource?shortenedUrl=" + shortenedUrl + "&password=" + password;
                    AudioType = "audio/" + mapping.Original.Split(".").Last();
                    return Page();
                }
            }
        }
    }
}
