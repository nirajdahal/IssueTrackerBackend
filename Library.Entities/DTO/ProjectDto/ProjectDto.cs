using Library.Entities.Models.Tickets;
using Library.Entities.Models.UsersProjects;
using System;
using System.Collections.Generic;

namespace Library.Entities.DTO.ProjectDto
{
    public class ProjectDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<Ticket> Ticket { get; set; }
        public ICollection<UserProject> UsersProjects { get; set; }
    }
}