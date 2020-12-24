using System;
using System.Collections.Generic;
using System.IO;
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
    public class ImageModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public string ImageBase64String { get; set; }
        public ImageModel(ILogger<IndexModel> logger, ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }
        public IActionResult OnGet(string shortenedUrl)
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
                    ImageBase64String = "data:image/" + Path.GetExtension(mapping.Original).Substring(1) + ";base64," +
                        Extension.ConvertToBase64(new FileStream(mapping.Original, FileMode.Open, FileAccess.Read));
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
                    ImageBase64String = "data:image/" + Path.GetExtension(mapping.Original).Substring(1) + ";base64," +
                        Extension.ConvertToBase64(new FileStream(mapping.Original, FileMode.Open, FileAccess.Read));
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
                    ImageBase64String = "data:image/" + Path.GetExtension(mapping.Original).Substring(1) + ";base64," +
                        Extension.ConvertToBase64(new FileStream(mapping.Original, FileMode.Open, FileAccess.Read));
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
                    ImageBase64String = "data:image/" + Path.GetExtension(mapping.Original).Substring(1) + ";base64," +
                        Extension.ConvertToBase64(new FileStream(mapping.Original, FileMode.Open, FileAccess.Read));
                    return Page();
                }
            }
        }
    }
}
