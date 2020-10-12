using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Library.Entities.DTO
{
    public class LoginModelDto
    {

        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
