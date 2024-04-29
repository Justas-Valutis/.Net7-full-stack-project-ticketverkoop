using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
        public MemoryStream CreatePDFDocumentAsync(List<Bestelling> bestellings, string logoPath)
        {
            //Genereren van de PDF - factuur
            using (MemoryStream stream = new MemoryStream())
            {
                PdfWriter writer = new PdfWriter(stream);
                PdfDocument pdf = new PdfDocument(writer);
                iText.Layout.Document document = new iText.Layout.Document(pdf);

                // Paragraaf toevoegen
                //Paragraph paragraph = new Paragraph("Dit is een voorbeeld van een paragraaf.");
                //document.Add(paragraph);

                // Factuurinformatie toevoegen
                document.Add(new Paragraph("Ticket").SetFontSize(20));
                document.Add(new Paragraph("TicketId: ").SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA)).SetFontSize(16).SetFontColor(ColorConstants.BLUE));
                document.Add(new Paragraph("Datum: " + DateTime.Now.ToShortDateString()));
                document.Add(new Paragraph(""));

                // Tabel voor producten
                iText.Layout.Element.Table table = new iText.Layout.Element.Table(UnitValue.CreatePercentArray(2)).UseAllAvailableWidth();
                table.AddHeaderCell("Match");
                table.AddHeaderCell("Zitplaats");
                table.AddHeaderCell("Abonnement");
                table.AddHeaderCell("Ploeg");
                table.AddHeaderCell("Stadium");
                table.AddHeaderCell("Besteldatum");
                table.AddHeaderCell("Prijs");
                decimal totalPrice = 0;
                foreach (var ticket in bestellings)
                {
                    table.AddCell(ticket.Ticket.Match.ToString());
                    table.AddCell(ticket.Ticket.Zitplaats.ToString());
                    table.AddCell(ticket.Abonnement.ToString());
                    table.AddCell(ticket.Abonnement.PloegNaam.ToString());
                    table.AddCell(ticket.Abonnement.StadiaNaam.ToString());
                    table.AddCell(ticket.BestelDatum.ToString());
                    table.AddCell(ticket.Ticket.Zitplaats.Section.Prijs.ToString("C"));
                    decimal totalProductPrice = (decimal)ticket.Ticket.Zitplaats.Section.Prijs;  
                    table.AddCell(totalProductPrice.ToString("C"));
                    totalPrice += totalProductPrice;
                    if (totalPrice > 50)
                    {
                        table.AddCell(totalPrice.ToString("C"));
                    }
                    else
                    {
                        table.AddCell("");
                    }

                }
                document.Add(table);

                //Totaalbedrag andere manier (onder de tabel)
                //document.Add(new Paragraph($"Totaalbedrag: " + $"{totalPrice.ToString("C")}"));

                //QR-Code
                // Binnen de GeneratePdf methode
                string qrContent = "https://www.youtube.com/watch?v=dQw4w9WgXcQ";
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrContent, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                Bitmap qrCodeImage = qrCode.GetGraphic(50); // Grootte van 20 pixels
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
