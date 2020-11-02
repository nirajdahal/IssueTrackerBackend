using Library.Entities.Models.Comments;
using Library.Entities.Models.Projects;
using Library.Entities.Models.UsersTickets;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Entities.Models.Tickets
{
    public class Ticket
    {
        [Column("TicketId")]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(200, ErrorMessage = "Maximum length for the Title is 200 characters.")]
        public string Title { get; set; }

        [Required]
        [MaxLength(500, ErrorMessage = "Maximum length for the Title is 500 characters.")]
        public string Description { get; set; }

        public string UpdatedByName { get; set; }
        public string UpdatedByEmail { get; set; }

        public string SubmittedByName { get; set; }
        public string SubmittedByEmail { get; set; }

        public ICollection<TicketComment> Comments { get; set; }

        [ForeignKey(nameof(TicketType))]
        public Guid TTypeId { get; set; }

        public TicketType TicketType { get; set; }

        [ForeignKey(nameof(TicketStatus))]
        public Guid TStatusId { get; set; }

        public TicketStatus TicketStatus { get; set; }

        [ForeignKey(nameof(TicketPriority))]
        public Guid TPriorityId { get; set; }

        public TicketPriority TicketPriority { get; set; }

        [ForeignKey(nameof(Project))]
        public Guid ProjectId { get; set; }

        public Project Project { get; set; }

        public ICollection<UserTicket> UsersTickets { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}