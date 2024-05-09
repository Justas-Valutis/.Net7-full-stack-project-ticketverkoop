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
using TicketVerkoop.Services.Interfaces;

namespace TicketVerkoop.Util.PDF
{
    public class CreatePDF : ICreatePDF
    {

        public MemoryStream CreatePDFDocumentAsync(Bestelling bestelling, string logoPath)
        {
            //Genereren van de PDF - factuur
            using (MemoryStream stream = new MemoryStream())
            {
                PdfWriter writer = new PdfWriter(stream);
                PdfDocument pdf = new(writer);
                iText.Layout.Document document = new iText.Layout.Document(pdf);

                foreach (var abonnement in bestelling.Abonnements)
                {
                    document.Add(new Paragraph("Jupiler Pro League Abonnement").SetFontSize(20));
                    document.Add(new Paragraph("Abonnementnummer: " + abonnement.AbonnementId));
                    if (abonnement.Ploeg != null)
                    {
                        document.Add(new Paragraph("Ploeg: " + abonnement.Ploeg.Naam));
                    }
                    foreach (var zitplaats in abonnement.Zitplaats)
                    {
                        if (zitplaats.Section != null && zitplaats.Section.Ring != null)
                        {
                            string ringZoneLocatie = zitplaats.Section.Ring.ToString() + " " + zitplaats.Section.Ring.ZoneLocatie.ToString();
                            document.Add(new Paragraph("Zitplaats: " + ringZoneLocatie));
                        }
                    }
                    //prijs krijg ik 
                    document.Add(new Paragraph("Prijs: " + abonnement.Prijs + " €"));
                    //QR-Code
                    // Binnen de GeneratePdf methode
                    string qrContent = ""; // You need to set this to something meaningful
                    QRCodeGenerator qrGenerator = new QRCodeGenerator();
                    QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrContent, QRCodeGenerator.ECCLevel.Q);
                    QRCode qrCode = new QRCode(qrCodeData);
                    Bitmap qrCodeImage = qrCode.GetGraphic(5);
                    iText.Layout.Element.Image qrCodeImageElement = new iText.Layout.Element.Image(ImageDataFactory.Create(BitmapToBytes(qrCodeImage))).SetHorizontalAlignment(HorizontalAlignment.CENTER);
                    document.Add(qrCodeImageElement);
                }

                
                foreach (var ticket in bestelling.Tickets)
                {
                    document.Add(new Paragraph("Jupiler Pro League Ticket").SetFontSize(20));
                    //Ticketnummer, niet van belang maar krijg ik wel
                    document.Add(new Paragraph("Ticketnummer: " + ticket.TicketId));
                    if (ticket.Match != null)
                    {
                            string matchInfo = "Ploeg: ";
                            if (ticket.Match.PloegThuis != null)
                            {
                                matchInfo += ticket.Match.PloegThuis.Naam;
                            }
                            matchInfo += " - ";
                            if (ticket.Match.PloegUit != null)
                            {
                                matchInfo += ticket.Match.PloegUit.Naam;
                            }
                            document.Add(new Paragraph(matchInfo));
                            document.Add(new Paragraph("Stadium: " + ticket.Match.Stadium.Naam));
                            document.Add(new Paragraph("Match Datum: " + ticket.Match.Datum.ToShortDateString()));
                    }
                    //prijs krijg ik 
                    document.Add(new Paragraph("Prijs: " + ticket.Prijs + " €"));
                    // Loop through each zitplaats associated with the ticket
                    if(ticket.Zitplaats.Count == 0)
                    {
                        document.Add(new Paragraph("Zitplaats: Geen zitplaats"));
                    }
                    foreach (var zitplaats in ticket.Zitplaats)
                    {
                        if (zitplaats.Section != null && zitplaats.Section.Ring != null)
                        {
                            string ringZoneLocatie = zitplaats.Section.Ring.ToString() + " " + zitplaats.Section.Ring.ZoneLocatie.ToString();
                            document.Add(new Paragraph("Zitplaats: " + ringZoneLocatie));
                        }
                    }
                    //QR-Code
                    // Binnen de GeneratePdf methode
                    string qrContent = ""; // You need to set this to something meaningful
                    QRCodeGenerator qrGenerator = new QRCodeGenerator();
                    QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrContent, QRCodeGenerator.ECCLevel.Q);
                    QRCode qrCode = new QRCode(qrCodeData);
                    Bitmap qrCodeImage = qrCode.GetGraphic(5);
                    iText.Layout.Element.Image qrCodeImageElement = new iText.Layout.Element.Image(ImageDataFactory.Create(BitmapToBytes(qrCodeImage))).SetHorizontalAlignment(HorizontalAlignment.CENTER);
                    document.Add(qrCodeImageElement);
                }
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
