using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace sho.rt.Helper
{

    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;
        private Dictionary<string, string> _mailSettings;
        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
            _mailSettings = _configuration.GetSection("MailSettings").GetChildren()
                  .ToDictionary(x => x.Key, x => x.Value);
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            MailMessage mailMessage = new MailMessage(_mailSettings["Sender"], email, subject, message);
            SmtpClient client = new SmtpClient(_mailSettings["Server"]);

            client.Port = int.Parse(_mailSettings["Port"]);
            client.Credentials = new NetworkCredential(_mailSettings["Id"], _mailSettings["Pw"]);
            client.EnableSsl = true;
            mailMessage.IsBodyHtml = true;
            return client.SendMailAsync(mailMessage);

        }

        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    var domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;

            }
        }
    }
}
