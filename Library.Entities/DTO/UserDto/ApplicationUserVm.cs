using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Entities.DTO.UserDto
{
    public class ApplicationUserVm
    {
        public string userName { get; set; }
        public string userEmail { get; set; }
        public List<string> userRole { get; set; }
    }
}