using Library.Entities.DTO.UserProjectsDto;
using System;
using System.Collections.Generic;

namespace Library.Entities.DTO.ProjectDto
{
    public class ProjectForTicketDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        public ICollection<ProjectManagerVmDto> ProjectManagers { get; set; }
    }
}