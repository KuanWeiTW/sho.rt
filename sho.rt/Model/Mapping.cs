using Base62;
using Microsoft.AspNetCore.Identity;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace sho.rt.Model
{
    public class Mapping
    {
        public int Id { get; set; }

        public string Password { get; set; }

        [NotMapped]
        public string ShortenedUrl
        {
            get
            {
                return Id.ToBase62();
            }
            set
            {
                Id = value.FromBase62<int>();
            }
        }

        public string OriginalUrl { get; set; }

        public string OwnerId { get; set; }

        public IdentityUser Owner { get; set; }
    }
}
