using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using sho.rt.Data;
using sho.rt.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sho.rt.Pages
{
    [Route("[controller]")]
    [ApiController]
    public class VideoStreamSourceController : ControllerBase
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public VideoStreamSourceController(ILogger<IndexModel> logger, ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        [HttpGet("/VideoStreamSource")]
        public ActionResult VideoSource(string shortenedUrl, string password)
        {
            if (shortenedUrl.Length > 5)
            {
                var mapping = _context.CustomMapping.Find(Base62.Decode(shortenedUrl));
                if (password != mapping.Password)
                {
                    return BadRequest();
                }
                else
                {
                    return PhysicalFile(mapping.Original, "application/octet-stream", true);
                }
            }
            else
            {
                var mapping = _context.Mapping.Find(Base62.Decode(shortenedUrl));
                if (password != mapping.Password)
                {
                    return BadRequest();
                }
                else
                {
                    return PhysicalFile(mapping.Original, "application/octet-stream", true);
                }
            }
        }
    }
}
