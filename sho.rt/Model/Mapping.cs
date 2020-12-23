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
        [Range(0, 26 * 26 * 26 * 26 * 26 - 1)]
        public Int64 Id { get; set; }

        public string Password { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9_.-]*$", ErrorMessage = "Please input letters and numbers only.")]
        [StringLength(5, MinimumLength = 1)]
        [NotMapped]
        public string ShortenedUrl
        {
            get => Base62.Encode(Id);
            set => Id = Base62.Decode(value);
        }

        public string Original { get; set; }

        public string OwnerId { get; set; }

        public IdentityUser Owner { get; set; }
    }
}
