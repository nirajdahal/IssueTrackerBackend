using Library.Entities.DTO.UserDto;
using System;

namespace Library.Entities.DTO.UserProjectsDto
{
    public class ProjectManagerVmDto
    {
        public Guid Id { get; set; }
        public ApplicationUserVm ApplicationUser { get; set; }
    }
}