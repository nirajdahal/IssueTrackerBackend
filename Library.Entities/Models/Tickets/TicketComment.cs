using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Library.Entities.Models.Tickets
{
    public class TicketComment
    {

        [Column("TCommentsId")]
        public Guid CommentsId { get; set; }

        [MaxLength(160, ErrorMessage ="The comment length cannot exceed more than 160")]
        public string Description { get; set; }

        [ForeignKey(nameof(Ticket))]
        public Guid TicketId { get; set; }

        public Ticket Ticket{ get; set; }

        [ForeignKey(nameof(ApplicationUser))]


        [Column("UserId")]
        public string Id { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}
