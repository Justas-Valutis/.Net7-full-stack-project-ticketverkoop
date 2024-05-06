using Microsoft.AspNetCore.Mvc;
using TicketVerkoop.Util.Mail.Interfaces;
using TicketVerkoop.Util.PDF.Interfaces;

namespace TicketVerkoop.Controllers
{
    public class EmailSendController : Controller
    {
        private readonly IEmailSend _emailSender;
        private readonly ICreatePDF _createPDF;
        //In ASP.NET Core kun je de IWebHostEnvironment service gebruiken om het pad 
        //naar de wwwroot map op te halen.
        private readonly IWebHostEnvironment _hostingEnvironment;

        public EmailSendController(IEmailSend emailSend, IWebHostEnvironment webHostEnvironment
            , ICreatePDF createPDF)
        {
            this._emailSender = emailSend;
            this._hostingEnvironment = webHostEnvironment;
            this._createPDF = createPDF;
        }


    }
}
