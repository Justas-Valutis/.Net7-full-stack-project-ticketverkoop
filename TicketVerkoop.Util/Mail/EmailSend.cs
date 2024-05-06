using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TicketVerkoop.Util.Mail.Interfaces;

namespace TicketVerkoop.Util.Mail
{
    public class EmailSend : IEmailSend
    {
        private readonly EmailSettings _emailSettings;


        public EmailSend(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendEmailAttachmentAsync(string email, string subject, string message, Stream attachmentStream, string attachmentName, bool isBodyHtml = false)
        {
            var mail = new MailMessage(); 
            mail.To.Add(new MailAddress(email));
            mail.From = new
            MailAddress("robsievandenbroucke@gmail.com"); 
            mail.Subject = subject;
            mail.Body = message;
            mail.Attachments.Add(new Attachment(attachmentStream, attachmentName));
            mail.IsBodyHtml = true;
            try
            {
                await SmtpMailAsync(mail);
            }
            catch (Exception ex)
            { throw ex; }
        }

        private async Task SmtpMailAsync(MailMessage mail)
        {
            using (var smtp = new SmtpClient(_emailSettings.MailServer))
            {
                smtp.Port = _emailSettings.MailPort;
                smtp.EnableSsl = true;
                smtp.Credentials =
                new NetworkCredential(_emailSettings.Sender,
                _emailSettings.Password);
                await smtp.SendMailAsync(mail);
            }
        }
    }
}
