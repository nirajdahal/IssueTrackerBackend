using System.ComponentModel.DataAnnotations;

namespace Library.Entities.DTO
{
    public class RegisterModelDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}