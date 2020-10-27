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
    public class CustomMapping
    {
        [Range(14776334, 839299365868340223)]
        public Int64 Id { get; set; }

        public string Password { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9_.-]*$", ErrorMessage = "Please input letters and numbers only.")]
        [StringLength(10, MinimumLength = 5)]
        [NotMapped]
        [Required]
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
