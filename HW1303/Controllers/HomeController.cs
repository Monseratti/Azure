using HW1303.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using System.Xml.Linq;

namespace HW1303.Controllers
{
    public class HomeController : Controller
    {
        
        public HomeController(ILogger<HomeController> logger)
        {
        }

        public async Task<IActionResult> Index()
        {
            await DBController.CreateTable();
            List<Address> addresses = new List<Address>();
            var dataAddress = await DBController.SelectData("address");
            for (int i = 0; i < dataAddress.Rows.Count; i++)
            {
                Address address = new Address()
                {
                    Id = (int) dataAddress.Rows[i][0],
                    Country = dataAddress.Rows[i][1].ToString(),
                    City = dataAddress.Rows[i][2].ToString(),
                    Street = dataAddress.Rows[i][3].ToString(),
                    Building_no = (int) dataAddress.Rows[i][4]
                };
                addresses.Add(address);
            }
            ViewBag.Addresses = addresses;

            List<BookInfo> books = new List<BookInfo>();
            var dataBooks = await DBController.SelectData("book_info");
            for (int i = 0; i < dataBooks.Rows.Count; i++)
            {
                BookInfo book = new BookInfo()
                {
                    Id = (int)dataBooks.Rows[i][0],
                    Book_title = dataBooks.Rows[i][1].ToString(),
                    Authors = dataBooks.Rows[i][2].ToString(),
                    Publishing_name = dataBooks.Rows[i][3].ToString(),
                    Publishing_address_id = (int) dataBooks.Rows[i][4],
                    publishing_date = (DateTime)dataBooks.Rows[i][5]
                };
                books.Add(book);
            }
            ViewBag.Books = books.Join(addresses,b=>b.Publishing_address_id,a=>a.Id,
                (b, a) => new{
                    b.Id, 
                    b.Book_title, 
                    b.Authors, 
                    b.Publishing_name, 
                    address=$"{a.Country}, {a.City}, {a.Street}, {a.Building_no}",
                    b.publishing_date
                });

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CreateAddress()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAddress([Bind]Address address)
        {
            await DBController.AddData("address", address.Country, address.City, address.Street, address.Building_no.ToString());
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> CreateBookInfo()
        {
            List<Address> addresses = new List<Address>();
            var dataAddress = await DBController.SelectData("address");
            for (int i = 0; i < dataAddress.Rows.Count; i++)
            {
                Address address = new Address()
                {
                    Id = (int)dataAddress.Rows[i][0],
                    Country = dataAddress.Rows[i][1].ToString(),
                    City = dataAddress.Rows[i][2].ToString(),
                    Street = dataAddress.Rows[i][3].ToString(),
                    Building_no = (int)dataAddress.Rows[i][4]
                };
                addresses.Add(address);
            }
            ViewBag.Addresses = addresses;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateBookInfo([Bind] BookInfo book)
        {
            await DBController.AddData("book_info", 
                book.Book_title, 
                book.Authors, 
                book.Publishing_name, 
                book.Publishing_address_id.ToString(), 
                book.publishing_date.ToString());
            return RedirectToAction("Index");
        }
    }
}