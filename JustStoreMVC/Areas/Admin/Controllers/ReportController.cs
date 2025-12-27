using AutoMapper;
using ClosedXML.Excel;
using DataAccess.Repository.IRepository;
using iTextSharp.text;
using iTextSharp.text.pdf;
using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JustStoreMVC.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ReportController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public ReportController(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _env = env;
        }

        public ActionResult GenerateBookReport()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                Document pdfDoc = new Document(PageSize.A4, 40, 40, 40, 40);
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();

                string logoPath = Path.Combine(_env.WebRootPath, "images", "book.png");
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

                AddTableHeader(table, "ID");
                AddTableHeader(table, "Book Title");
                AddTableHeader(table, "Price (UAH)");

                var books = _mapper.Map<List<Product>>(_unitOfWork.Product.GetAll().ToList());

                foreach (var book in books)
                {
                    table.AddCell(book.Id.ToString());
                    table.AddCell(book.Title);
                    table.AddCell(book.Price.ToString("0.00"));
                }

                pdfDoc.Add(table);
                pdfDoc.Add(new Paragraph(" "));

                if (books.Any())
                {
                    double avgPrice = books.Average(b => b.Price);
                    double maxPrice = books.Max(b => b.Price);
                    double minPrice = books.Min(b => b.Price);
                    int count = books.Count;

                    pdfDoc.Add(new Paragraph($"Total number of books: {count}", normalFont));
                    pdfDoc.Add(new Paragraph($"Average price: {avgPrice:0.00} UAH", normalFont));
                    pdfDoc.Add(new Paragraph($"Most expensive book: {books.First(b => b.Price == maxPrice).Title} ({maxPrice:0.00} UAH)", normalFont));
                    pdfDoc.Add(new Paragraph($"Cheapest book: {books.First(b => b.Price == minPrice).Title} ({minPrice:0.00} UAH)", normalFont));
                }

                pdfDoc.Add(new Paragraph("\nThank you for using the BookStore system!", normalFont));
                pdfDoc.Add(new Paragraph("© 2025 JustStore", FontFactory.GetFont(FontFactory.HELVETICA_OBLIQUE, 10)));

                pdfDoc.Close();
                writer.Close();

                return File(stream.ToArray(), "application/pdf", "BookReport.pdf");
            }
        }

        private void AddTableHeader(PdfPTable table, string text)
        {
            var font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, BaseColor.WHITE);
            PdfPCell cell = new PdfPCell(new Phrase(text, font))
            {
                BackgroundColor = new BaseColor(0, 102, 204),
                HorizontalAlignment = Element.ALIGN_CENTER,
                Padding = 5
            };
            table.AddCell(cell);
        }

        public ActionResult GenerateExcelReport()
        {
            using (var workbook = new XLWorkbook())
            {
                var books = _mapper.Map<List<Product>>(_unitOfWork.Product.GetAll().ToList());

                var wsBooks = workbook.Worksheets.Add("Books");

                wsBooks.Cell(1, 1).Value = "ID";
                wsBooks.Cell(1, 2).Value = "Title";
                wsBooks.Cell(1, 3).Value = "Price (UAH)";

                wsBooks.Range("A1:C1").Style.Font.Bold = true;
                wsBooks.Range("A1:C1").Style.Fill.BackgroundColor = XLColor.AliceBlue;

                int row = 2;
                foreach (var b in books)
                {
                    wsBooks.Cell(row, 1).Value = b.Id;
                    wsBooks.Cell(row, 2).Value = b.Title;
                    wsBooks.Cell(row, 3).Value = b.Price;
                    row++;
                }

                wsBooks.Columns().AdjustToContents();

                var categories = _mapper.Map<List<Category>>(_unitOfWork.Category.GetAll().ToList());

                var wsCat = workbook.Worksheets.Add("Categories");

                wsCat.Cell(1, 1).Value = "ID";
                wsCat.Cell(1, 2).Value = "Name";

                wsCat.Range("A1:B1").Style.Font.Bold = true;
                wsCat.Range("A1:B1").Style.Fill.BackgroundColor = XLColor.LightGreen;

                row = 2;
                foreach (var c in categories)
                {
                    wsCat.Cell(row, 1).Value = c.ID;
                    wsCat.Cell(row, 2).Value = c.Name;
                    row++;
                }

                wsCat.Columns().AdjustToContents();

                var orders = _mapper.Map<List<ApplicationUser>>(_unitOfWork.ApplicationUser.GetAll().ToList());

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
                    wsOrders.Cell(row, 1).Value = o.Id;
                    wsOrders.Cell(row, 2).Value = o.Name;
                    wsOrders.Cell(row, 3).Value = o.Role;
                    wsOrders.Cell(row, 4).Value = o.State;
                    row++;
                }

                wsOrders.Columns().AdjustToContents();

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return File(stream.ToArray(),
                                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                                "BookStoreReport.xlsx");
                }
            }
        }


    }
}
