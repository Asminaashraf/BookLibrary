using BookCentreProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using BookCentreProject.RepositoryPattern.Interface;

namespace BookCentreProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBookLibrary bookLibrary;

        public HomeController(ILogger<HomeController> logger,IBookLibrary bookLibrary)
        {
            _logger = logger;
            this.bookLibrary = bookLibrary;
        }

        public async Task <IActionResult> Index(string searchString)
        {
             
            var data = await bookLibrary.GetBooks();
            if (!string.IsNullOrEmpty(searchString))
            {
                data = await bookLibrary.SearchBook(searchString);
            }

            return View(data);   
        }
        public async Task<IActionResult> DetailsHome(int id)
        {
            Book book = await bookLibrary.GetBookById(id);



            return View(book);
        }


       
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
