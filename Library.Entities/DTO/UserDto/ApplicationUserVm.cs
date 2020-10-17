using System.Collections.Generic;

namespace Library.Entities.DTO.UserDto
{
    public class ApplicationUserVm
    {
        public string userName { get; set; }
        public string userEmail { get; set; }
        public List<string> userRole { get; set; }
    }
}