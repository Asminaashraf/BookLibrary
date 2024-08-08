using System.ComponentModel.DataAnnotations;

namespace BookCentreProject.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        [Required(ErrorMessage ="Name is required")]

        
        public string Name { get; set; }
        [Required]

        public string Author { get; set; }
        [Required]

        public string Language { get; set; }
        [Required]

        public int Price { get; set; }
        [Required]

        public BookType Type { get; set; }
        public string Photo { get; set; }
            

    }
    public enum BookType
    {
        Romantic,
        Triller,
        Horror
    }
}
