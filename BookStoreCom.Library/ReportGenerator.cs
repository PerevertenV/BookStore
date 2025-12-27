using ClosedXML.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace BookStoreCom.Library
{
    [Guid("75836385-be2f-43c0-b717-c93d4ed98b0a")]
    [ComVisible(true)]
    public interface IReportGenerator
    {
        void GenerateExcelReport(string dataJson, string outputPath);
        void GenerateReport(string dataJson, string outputPath);
    }

    [Guid("d4bc8b46-2e59-405d-acda-882d0de8a63a")]
    [ComVisible(true)]
    [ProgId("BookStore.ReportGen")] 
    public class ReportGenerator : IReportGenerator
    {
        public void GenerateExcelReport(string dataJson, string outputPath)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                Document pdfDoc = new Document(PageSize.A4, 40, 40, 40, 40);
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();

                string logoPath = "book.png";
                if (System.IO.File.Exists(logoPath))
                {
                    iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(logoPath);
                    logo.ScaleAbsolute(60, 60);
                    logo.Alignment = Element.ALIGN_LEFT;
                    pdfDoc.Add(logo);
                }

                var titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18);
                var normalFont = FontFactory.GetFont(FontFactory.HELVETICA, 12);

                Paragraph title = new Paragraph("BookStore — Book Report", titleFont)
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingAfter = 10
                };
                pdfDoc.Add(title);

                pdfDoc.Add(new Paragraph("Generated on: " + DateTime.Now.ToString("dd.MM.yyyy HH:mm"), normalFont));
                pdfDoc.Add(new Paragraph("Report Author: JustStore Admin", normalFont));
                pdfDoc.Add(new Paragraph(" "));

                PdfPTable table = new PdfPTable(3)
                {
                    WidthPercentage = 100
                };
                table.SetWidths(new float[] { 10f, 60f, 30f });
                var books = "books";
                foreach (var book in books)
                {
                    table.AddCell(book.ToString());
                    table.AddCell(book.ToString());
                    table.AddCell(book.ToString());
                }

                pdfDoc.Add(table);
                pdfDoc.Add(new Paragraph(" "));

                if (books != null)
                {
                    double avgPrice = books.Average(b => b);
                    double maxPrice = books.Max(b => b);
                    double minPrice = books.Min(b => b);
                    int count = 8;

                    pdfDoc.Add(new Paragraph($"Total number of books: {count}", normalFont));
                    pdfDoc.Add(new Paragraph($"Average price: {avgPrice:0.00} UAH", normalFont));
                    pdfDoc.Add(new Paragraph($"Most expensive book: {books.First(b => b == maxPrice)} ({maxPrice:0.00} UAH)", normalFont));
                    pdfDoc.Add(new Paragraph($"Cheapest book: {books.First(b => b == minPrice)} ({minPrice:0.00} UAH)", normalFont));
                }

                pdfDoc.Add(new Paragraph("\nThank you for using the BookStore system!", normalFont));
                pdfDoc.Add(new Paragraph("© 2025 JustStore", FontFactory.GetFont(FontFactory.HELVETICA_OBLIQUE, 10)));

                pdfDoc.Close();
                writer.Close();

            }
        }
        public void GenerateReport(string dataJson, string outputPath)
        {
            using (var workbook = new XLWorkbook())
            {
                var books = new List<string>();

                var wsBooks = workbook.Worksheets.Add("Books");

                wsBooks.Cell(1, 1).Value = "ID";
                wsBooks.Cell(1, 2).Value = "Title";
                wsBooks.Cell(1, 3).Value = "Price (UAH)";

                wsBooks.Range("A1:C1").Style.Font.Bold = true;
                wsBooks.Range("A1:C1").Style.Fill.BackgroundColor = XLColor.AliceBlue;

                int row = 2;
                foreach (var b in books)
                {
                    wsBooks.Cell(row, 1).Value = b;
                    wsBooks.Cell(row, 2).Value = b;
                    wsBooks.Cell(row, 3).Value = b;
                    row++;
                }

                wsBooks.Columns().AdjustToContents();

                var categories = new List<string>();

                var wsCat = workbook.Worksheets.Add("Categories");

                wsCat.Cell(1, 1).Value = "ID";
                wsCat.Cell(1, 2).Value = "Name";

                wsCat.Range("A1:B1").Style.Font.Bold = true;
                wsCat.Range("A1:B1").Style.Fill.BackgroundColor = XLColor.LightGreen;

                row = 2;
                foreach (var c in categories)
                {
                    wsCat.Cell(row, 1).Value = c;
                    wsCat.Cell(row, 2).Value = c;
                    row++;
                }

                wsCat.Columns().AdjustToContents();

                var orders = new List<string>();

                var wsOrders = workbook.Worksheets.Add("Users");

                wsOrders.Cell(1, 1).Value = "User ID";
                wsOrders.Cell(1, 2).Value = "Name";
                wsOrders.Cell(1, 3).Value = "Role";
                wsOrders.Cell(1, 4).Value = "State";

                wsOrders.Range("A1:D1").Style.Font.Bold = true;
                wsOrders.Range("A1:D1").Style.Fill.BackgroundColor = XLColor.LightGray;

                row = 2;
                foreach (var o in orders)
                {
                    wsOrders.Cell(row, 1).Value = o;
                    wsOrders.Cell(row, 2).Value = o;
                    wsOrders.Cell(row, 3).Value = o;
                    wsOrders.Cell(row, 4).Value = o;
                    row++;
                }

                wsOrders.Columns().AdjustToContents();

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                }
            }

        }
    }
}
