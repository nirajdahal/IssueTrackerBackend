using Library.Entities.DTO.TicketDto;
using Library.Entities.DTO.UserProjectsDto;
using System;
using System.Collections.Generic;

namespace Library.Entities.DTO.ProjectDto
{
    public class ProjectVmDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<TicketForProjectDto> TicketVm { get; set; }
        public ICollection<UserProjectVmDto> UsersProjects { get; set; }
        public ICollection<ProjectManagerVmDto> ProjectManagers { get; set; }
    }
}