using TicketVerkoop.Domains.Entities;
using TicketVerkoop.Util.PDF.Interfaces;
using QRCoder;
using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.IO.Font.Constants;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using System.Drawing;

namespace TicketVerkoop.Util.PDF
{
    public class CreatePDF : ICreatePDF
    {
        
        public MemoryStream CreatePDFDocumentAsync(List<Ticket> tickets, string logoPath)
        {
            //Genereren van de PDF - factuur
            using (MemoryStream stream = new MemoryStream())
            {
                PdfWriter writer = new PdfWriter(stream);
                PdfDocument pdf = new PdfDocument(writer);
                iText.Layout.Document document = new iText.Layout.Document(pdf);

                // Factuurinformatie toevoegen
                document.Add(new Paragraph("Jupiler Pro League Ticket").SetFontSize(20));
                foreach (var ticket in tickets)
                {
                    //TICKETID KRIJG IK WEL, REST NIET
                    document.Add(new Paragraph("Ticketnummer: " + ticket.TicketId.ToString()));
                    //wanneer ik hieronder de match wil toevoegen, krijg ik een nullpointerexception (omdat er geen match word meegegeven blijkbaar)
                    document.Add(new Paragraph("Ploeg: " + ticket.Match.PloegThuis.Naam.ToString() + " - " + ticket.Match.PloegUit.Naam.ToString()));
                    document.Add(new Paragraph("Stadium: " + ticket.Match.Stadium.Naam.ToString()));
                    document.Add(new Paragraph("Besteldatum: " + ticket.Match.Datum.ToShortDateString()));
                    document.Add(new Paragraph("Prijs: " + ticket.Bestelling.TotalPrijs.ToString() + " €"));
                    foreach (var zitplaats in ticket.Zitplaats)
                    {
                        string ringZoneLocatie = zitplaats.Section.Ring.ToString() + " " + zitplaats.Section.Ring.ZoneLocatie.ToString();
                        document.Add(new Paragraph("Zitplaats: " + ringZoneLocatie));
                    }
                }

                //QR-Code
                // Binnen de GeneratePdf methode
                string qrContent = "";
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrContent, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                Bitmap qrCodeImage = qrCode.GetGraphic(5);
                iText.Layout.Element.Image qrCodeImageElement = new
                iText.Layout.Element.Image(ImageDataFactory.Create(BitmapToBytes(qrCodeImage))).SetHorizontalAlignment(HorizontalAlignment.CENTER);
                document.Add(qrCodeImageElement);


                document.Close();
                return new MemoryStream(stream.ToArray());

            }
        }

        // This method is for converting bitmap into a byte array
        private static byte[] BitmapToBytes(Bitmap img)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }
    }
}
