using Library.Entities.Models.Tickets;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Entities.Models.UsersTickets
{
    public class UserTicket

    {
        [ForeignKey(nameof(Ticket))]
        public Guid TicketId { get; set; }

        public Ticket Ticket { get; set; }

        [ForeignKey(nameof(ApplicationUser))]
        [Column("UserId")]
        public string Id { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}