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
        [MaxLength(160, ErrorMessage = "Maximum length for the Description is 160 characters.")]
        public string Description { get; set; }

        public string SubmitterName { get; set; }

        [MaxLength(160, ErrorMessage = "Maximum length for the Comments is 160 characters.")]
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
    }
}
