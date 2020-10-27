using Microsoft.AspNetCore.Identity;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using sho.rt.Helper;

namespace sho.rt.Model
{
    public class Mapping
    {
        public Int64 Id { get; set; }

        public string Password { get; set; }

        [NotMapped]
        public string ShortenedUrl
        {
            get => Base62.Encode(Id);
            set => Id = Base62.Decode(value);
        }

        public string OriginalUrl { get; set; }

        public string OwnerId { get; set; }

        public IdentityUser Owner { get; set; }
    }
}
