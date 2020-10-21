using System.ComponentModel.DataAnnotations;

namespace Library.Entities.Models
{
    public class RegisterModel
    {
        [Required]
        [MaxLength(30, ErrorMessage = "Maximum length for the Password is 30 characters.")]
        public string Name { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Maximum length for the Email is 50 characters.")]
        public string Email { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Maximum length for the Password is 20 characters.")]
        public string Password { get; set; }
    }
}