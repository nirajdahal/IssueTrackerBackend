using Library.Entities.Models.Tickets;
using Library.Entities.Models.UsersProjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Entities.Models.Projects
{
    public class Project
    {
        [Column("ProjectId")]
        public Guid Id { get; set; }

        [MaxLength(200, ErrorMessage = "Maximum length for the Title is 200 characters.")]
        public string Title { get; set; }

        [MaxLength(500, ErrorMessage = "Maximum length for the Title is 500 characters.")]
        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public string SubmittedByName { get; set; }
        public string SubmittedByEmail { get; set; }

        public string UpdatedByName { get; set; }
        public string UpdatedByEmail { get; set; }
        public ICollection<Ticket> Ticket { get; set; }

        public virtual ICollection<ProjectManager> ProjectManagers { get; set; }

        public ICollection<UserProject> UsersProjects { get; set; }
    }
}