using Microsoft.AspNetCore.Mvc;
using Bookstore.Models;
using System.Xml.Linq;

namespace Bookstore.Controllers
{
    public class BookController : Controller
    {
        private Repository<Book> data { get; set; }
        private IWebHostEnvironment environment { get; set; }

        public BookController(BookstoreContext ctx, IWebHostEnvironment env)
        {
            data = new Repository<Book>(ctx);
            environment = env;
        }

        public RedirectToActionResult Index() => RedirectToAction("List");

        public ViewResult List(BookGridData values)
        {
            // create options for querying books
            var options = new QueryOptions<Book>
            {
                Includes = "Authors, Genre",
                OrderByDirection = values.SortDirection,
                PageNumber = values.PageNumber,
                PageSize = values.PageSize
            };

            if (values.IsSortByGenre)
                options.OrderBy = b => b.GenreId;
            else if (values.IsSortByPrice)
                options.OrderBy = b => b.Price;
            else
                options.OrderBy = b => b.Title;

            // create view model
            var vm = new BookListViewModel
            {
                Books = data.List(options),
                CurrentRoute = values,
                TotalPages = values.GetTotalPages(data.Count)
            };

            return View(vm);
        }

        public ViewResult Details(int id)
        {
            var book = data.Get(new QueryOptions<Book>
            {
                Where = b => b.BookId == id,
                Includes = "Authors, Genre"
            }) ?? new Book();

            return View(book);
        }

        [HttpPost]
        public RedirectToActionResult PageSize(BookGridData currentRoute)
        {
            return RedirectToAction("List", currentRoute.ToDictionary());
        }

        public RedirectToActionResult ExportToXml()
        {
            // Get all books from the database, including authors and genre.
            var books = data.List(new QueryOptions<Book>
            {
                Includes = "Authors, Genre",
                OrderBy = b => b.Title
            });

            // Create the XML document.
            var document = new XDocument(
                new XElement("Books",
                    books.Select(book =>
                        new XElement("Book",
                            new XElement("ID", book.BookId),
                            new XElement("Title", book.Title),
                            new XElement("Authors",
                                book.Authors.Select(author =>
                                    new XElement("Author", author.FullName)
                                )
                            ),
                            new XElement("Price", book.Price),
                            new XElement("Category", book.Genre?.Name ?? "")
                        )
                    )
                )
            );

            // Create the data directory inside wwwroot if it doesn't exist.
            string dataPath = Path.Combine(environment.WebRootPath, "data");
            Directory.CreateDirectory(dataPath);

            // Set the XML file path.
            string filePath = Path.Combine(dataPath, "books.xml");

            // Save the XML document.
            document.Save(filePath);

            // Return to the Book Catalog with a confirmation message.
            TempData["Message"] = "Books successfully exported to XML.";

            return RedirectToAction("List");
        }
    }
}
