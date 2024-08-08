using System.ComponentModel.DataAnnotations;

namespace BookCentreProject.ViewModel
{
    public class RegisterVM
    {
        [Required]
        public String Name { get; set; }
        [Required]
        [EmailAddress]
        public String Email { get; set; }
        [Required]
        [DataType(DataType.Password)]

        public String Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and Confiirm password does not match")]
        public String ConfirmPassword { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public String Address { get; set; }
    }
}
