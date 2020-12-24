using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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

        private readonly static string FILE_STORE = "/file";
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
                    return Redirect(mapping.Original);
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
                    if (mapping.MappingType == MappingType.URL)
                    {
                        return Redirect(mapping.Original);
                    }
                    else if (mapping.MappingType == MappingType.IMAGE)
                    {
                        return RedirectToPage("./ImageContent", new { shortenedUrl = shortenedUrl });
                    }
                    else if (mapping.MappingType == MappingType.VIDEO)
                    {
                        return RedirectToPage("./VideoContent", new { shortenedUrl = shortenedUrl });
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
            }
        }

        public async Task<IActionResult> OnPost(string type, string url, IFormFile image, IFormFile video, string password)
        {
            if (type == "url")
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
                        MappingType=MappingType.URL,
                        Original = url,
                        Password = password,
                        Owner = await _userManager.GetUserAsync(HttpContext.User)
                    };

                    _context.Add(mapping);
                    await _context.SaveChangesAsync();
                    ShortenedUrl = Url.Page("/Index", null, new { shortenedUrl = "" }, protocol: Request.Scheme) + mapping.ShortenedUrl;
                }
                return Page();
            }
            else if (type == "image")
            {
                if (image == null)
                {
                    ErrorMessage = "image is empty";
                }
                else
                {
                    string stored_name = System.IO.Path.Combine(FILE_STORE, Guid.NewGuid() + Path.GetExtension(image.FileName));
                    using (Stream fileStream = new FileStream(stored_name, FileMode.Create, FileAccess.Write))
                    {
                        await image.CopyToAsync(fileStream);
                    }
                    Mapping mapping = new Mapping
                    {
                        MappingType = MappingType.IMAGE,
                        Original = stored_name,
                        Password = password,
                        Owner = await _userManager.GetUserAsync(HttpContext.User)
                    };

                    _context.Add(mapping);
                    await _context.SaveChangesAsync();
                    ShortenedUrl = Url.Page("/Index", null, new { shortenedUrl = "" }, protocol: Request.Scheme) + mapping.ShortenedUrl;
                }
                return Page();
            }
            else if (type == "video")
            {
                if (video == null)
                {
                    ErrorMessage = "video is empty";
                }
                else
                {
                    string stored_name = System.IO.Path.Combine(FILE_STORE, Guid.NewGuid() + Path.GetExtension(video.FileName));
                    using (Stream fileStream = new FileStream(stored_name, FileMode.Create, FileAccess.Write))
                    {
                        await video.CopyToAsync(fileStream);
                    }
                    Mapping mapping = new Mapping
                    {
                        MappingType = MappingType.VIDEO,
                        Original = stored_name,
                        Password = password,
                        Owner = await _userManager.GetUserAsync(HttpContext.User)
                    };

                    _context.Add(mapping);
                    await _context.SaveChangesAsync();
                    ShortenedUrl = Url.Page("/Index", null, new { shortenedUrl = "" }, protocol: Request.Scheme) + mapping.ShortenedUrl;
                }
                return Page();
            }
            return BadRequest();
        }
    }
}

