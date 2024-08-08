using BookCentreProject.RepositoryPattern.Interface;
using Microsoft.AspNetCore.Mvc;
using BookCentreProject.Models;
using BookCentreProject.ViewModel;
using static System.Net.Mime.MediaTypeNames;

namespace BookCentreProject.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookLibrary bookLibrary;
        private readonly IWebHostEnvironment webHost;

        public BookController(IBookLibrary bookLibrary,IWebHostEnvironment webHost)
        {
            this.bookLibrary = bookLibrary;
            this.webHost = webHost;
        }
        public async Task<IActionResult> GetBooks()

        {
            var data = await bookLibrary.GetBooks();

            return View(data);
        }
        public async Task<IActionResult> AddBook()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddBook(BookViewModel  model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadFile(model);
                Book book = new Book()
                {
                    Name = model.Name,
                    Author = model.Author,
                    Language = model.Language,
                    Price = model.Price,
                    Type = model.Type,
                    Photo = uniqueFileName
                };
                await bookLibrary.AddBook(book);
                if (book.BookId == 0)
                {
                    TempData["Error"] = "Create Failed";

                }
                else
                {
                    TempData["Success"] = "Create Successfully";

                }

                return RedirectToAction("GetBooks");

                
            }

            return View();
        }
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            else
            {
                Book book = await bookLibrary.GetBookById(id);
                BookEditViewModel bookEditViewModel = new BookEditViewModel()
                {
                    Id=book.BookId,
                    Name=book.Name,
                    Author=book.Author,
                    Language=book.Language,
                    Price=book.Price,
                    Type=book.Type,
                    ExistingImage=book.Photo

                };
                if (book == null)
                {
                    return NotFound();
                }
                return View(bookEditViewModel);
            }


        }
        [HttpPost]
        public async Task<IActionResult> Edit(BookEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Book book = await bookLibrary.GetBookById(model.Id);
                book.Name = model.Name;
                book.Author = model.Author;
                book.Language = model.Language;
                book.Price = model.Price;
                book.Type = model.Type;
                if(model.Image != null)
                {
                    if(model.ExistingImage != null)
                    {
                        string filePath = Path.Combine(webHost.WebRootPath,"images",model.ExistingImage );
                        System.IO.File.Delete(filePath);
                    }
                    book.Photo = ProcessUploadFile(model);
                }
                await bookLibrary.UpdateBook(book);
                

                if (book.BookId == 0)
                {
                    TempData["Error"] = "Create Failed";

                }
                else
                {
                    TempData["Success"] = "Create Successfully";

                }

                return RedirectToAction("GetBooks");


            }

            return View();
        }

        private string ProcessUploadFile(BookViewModel model)
        {
            string uniqueFileName = null;
            if (model.Image != null)
            {
                string UploadFolder = Path.Combine(webHost.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
                string FilePath = Path.Combine(UploadFolder, uniqueFileName);
                using(var fileStream= new FileStream(FilePath, FileMode.Create))
                {
                    model.Image.CopyTo(fileStream);

                }
                
            }

            return uniqueFileName;
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest();

            }
            else
            {
                 bool status = await bookLibrary.DeleteBook(id);
                    if (status)
                    {
                        TempData["Success"] = "Delete Successfully";

                    }
                    else
                    {
                        TempData["Error"] = "Delete Failed";
                    }
                
                return RedirectToAction("GetBooks");
            }

        }

                

               
        public async Task<IActionResult> Details(int id)
        {
            Book book= await bookLibrary.GetBookById(id);



            return View(book);
        }
      




    }
}
