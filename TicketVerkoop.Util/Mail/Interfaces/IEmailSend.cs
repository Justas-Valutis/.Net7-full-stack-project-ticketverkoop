using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketVerkoop.Util.Mail.Interfaces
{
    public interface IEmailSend
    {
        Task SendEmailAttachmentAsync(string email, string subject, string message/*, Stream attachmentStream, string attachmentName, bool isBodyHtml = false*/);

    }
}
