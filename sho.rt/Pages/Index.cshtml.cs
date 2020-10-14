using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace sho.rt.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public string Message { get; private set; } = "";
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public IActionResult OnGet(string param)
        {
            if (param == null)
            {
                return Page();
            }
            else
            {
                return Redirect("https://www.google.com");
            }
        }

        public IActionResult OnPost(string url)
        {
            if (url == null)
            {
                Message = "url is empty";
            }
            else if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
            {
                Message = "url is invalid";
            }
            else
            {
                Message = url;
            }
            return Page();
        }
    }
}
