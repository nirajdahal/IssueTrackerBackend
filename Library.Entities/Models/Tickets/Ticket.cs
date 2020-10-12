using Library.Entities.Models.Projects;
using Library.Entities.Models.UsersTickets;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Library.Entities.Models.Tickets
{
    public class Ticket
    {
        [Column("TicketId")]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(60, ErrorMessage = "Maximum length for the Title is 60 characters.")]
        public string Title { get; set; }

        [Required]
        [MaxLength(160, ErrorMessage = "Maximum length for the Title is 160 characters.")]
        public string Description { get; set; }

        public string SubmitterName { get; set; }

        [MaxLength(160, ErrorMessage = "Maximum length for the Title is 160 characters.")]
        public string Comments { get; set; }

        [Required]
        public TicketType Type { get; set; }

        [Required]
        public TicketStatus Status { get; set; }

        [Required]
        public TicketPriority Priority { get; set; }

        [ForeignKey(nameof(Project))]
        public Guid ProjectId { get; set; }

        public Project Project { get; set; }

        public ICollection<UserTicket> UsersTickets { get; set; }
    }
}
