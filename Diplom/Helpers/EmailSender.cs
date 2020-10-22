using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Diplom.Helpers
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            await Task.Factory.StartNew(() =>
            {
                MailMessage _message = new MailMessage("nics.company.email@gmail.com", email);
                _message.Subject = subject;
                _message.Body = message;
                _message.IsBodyHtml = true;
                var client = new SmtpClient();
                client.Host = "smtp.gmail.com";
                client.EnableSsl = true;
                NetworkCredential NC = new NetworkCredential("nics.company.email@gmail.com", "Qqazqaz21");
                client.UseDefaultCredentials = true;
                client.Credentials = NC;
                client.Port = 587;
                client.Send(_message);
            });
        }
    }
}
