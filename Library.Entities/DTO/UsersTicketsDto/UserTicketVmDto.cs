using Library.Entities.DTO.UserDto;
using Library.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Entities.DTO.UsersTicketsDto
{
    public class UserTicketVmDto
    {
        public string Id { get; set; }
        public ApplicationUserVm ApplicationUser { get; set; }
    }
}