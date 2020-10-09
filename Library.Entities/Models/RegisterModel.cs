﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Library.Entities.Models
{
    public class RegisterModel
    {
        [Required]
        public string Name { get; set; }
   
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
