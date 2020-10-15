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

        [MaxLength(60, ErrorMessage = "Maximum length for the Title is 60 characters.")]
        public string Title { get; set; }

        [MaxLength(160, ErrorMessage = "Maximum length for the Title is 160 characters.")]
        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }
        public ICollection<Ticket> Ticket { get; set; }
        public ICollection<UserProject> UsersProjects { get; set; }
    }
}