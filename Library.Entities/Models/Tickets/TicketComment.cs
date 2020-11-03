using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Entities.Models.Tickets
{
    public class TicketComment
    {
        [Key]
        public Guid Id { get; set; }

        [MaxLength(300, ErrorMessage = "The comment length cannot exceed more than 300")]
        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public Guid TicketId { get; set; }

        public string CreatedBy { get; set; }
    }
}