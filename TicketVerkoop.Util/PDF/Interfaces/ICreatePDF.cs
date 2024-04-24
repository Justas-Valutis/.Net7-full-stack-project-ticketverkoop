using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketVerkoop.Domains.Entities;

namespace TicketVerkoop.Util.PDF.Interfaces
{
    public interface ICreatePDF
    {
        MemoryStream CreatePDFDocumentAsync(List<Bestelling> bestellings, string logoPath);
    }
}
