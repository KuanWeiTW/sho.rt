using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace sho.rt.Helper
{
    public static class Extension
    {
        public static string ConvertToBase64(this Stream stream)
        {
            BinaryReader br = new System.IO.BinaryReader(stream);
            Byte[] bytes = br.ReadBytes((Int32)stream.Length);
            return Convert.ToBase64String(bytes, 0, bytes.Length);
        }
    }
}
