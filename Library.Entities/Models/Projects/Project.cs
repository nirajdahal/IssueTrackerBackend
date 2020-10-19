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
        public DateTime UpdateAt { get; set; }
        public string SubmittedByName { get; set; }
        public string SubmittedByEmail { get; set; }

        public string UpdatedByName { get; set; }
        public string UpdatedByEmail { get; set; }
        public ICollection<Ticket> Ticket { get; set; }//ticket1, ticket2
        public ICollection<UserProject> UsersProjects { get; set; } // niraj, nirjala - delete niraj, nirjala, tulshi, bhagwati
    }
}