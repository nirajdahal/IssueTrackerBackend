using System.ComponentModel.DataAnnotations;

namespace Library.Entities.Models
{
    public class LoginModel
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Maximum length for the Email is 60 characters.")]
        public string Email { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Maximum length for the Password is 20 characters.")]
        public string Password { get; set; }
    }
}