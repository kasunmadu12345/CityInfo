using System.Net.Mail;
using System.Net;
using System;
using CityInfo.API.Services.Contract;
using Microsoft.Extensions.Options;
using System.Runtime;
using CityInfo.API.Models;
using System.Diagnostics;

namespace CityInfo.API.Services.Implementation
{
    public class LocalMailService : ILocalMailService
    {
        private readonly EmailSettings _emailSettings;

        public LocalMailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
            Debug.WriteLine($"FROM: {_emailSettings.FromEmail}");
        }
        public bool SendMail(string to, string subject, string body)
        {
            try
            {
                SmtpClient client = new SmtpClient("your-smtp-server");
                client.Port = 587;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("your-email", "your-password");
                client.EnableSsl = true;

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(_emailSettings.FromEmail);
                mail.To.Add(to);
                mail.Subject = subject;
                mail.Body = body;

                client.Send(mail);

                Console.WriteLine("Mail sent successfully.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send mail. Error: {ex.Message}");
                return false;
            }
        }


    }
}
