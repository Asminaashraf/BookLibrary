using System.ComponentModel.DataAnnotations;
using BookCentreProject.Models;

namespace BookCentreProject.ViewModel
{
    public class BookViewModel
    {
        [Required(ErrorMessage = "Name is required")]


        public string Name { get; set; }
        [Required]

        public string Author { get; set; }
        [Required]

        public string Language { get; set; }
        [Required]

        public int Price { get; set; }
        [Required]

        public BookType Type { get; set; }
        public IFormFile Image { get; set; }



    }
   
}

