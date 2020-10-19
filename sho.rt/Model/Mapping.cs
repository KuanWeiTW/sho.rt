using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace sho.rt.Model
{
    public class Mapping
    {
        public int Id { get; set; }

        public string Password { get; set; }

        public bool IsCustomized { get; set; }

        public string ShortenedUrl { get; set; }

        public string OriginalUrl { get; set; }
        
        public string OwnerId { get; set; }

        public IdentityUser Owner { get; set; }
    }
}
