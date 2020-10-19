using Library.Entities.DTO.UserDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Entities.DTO.UserProjectsDto
{
    public class UserProjectVmDto
    {
        public string Id { get; set; }
        public ApplicationUserVm ApplicationUser { get; set; }
    }
}