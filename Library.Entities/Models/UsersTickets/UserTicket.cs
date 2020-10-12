using Library.Entities.Models.Tickets;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Library.Entities.Models.UsersTickets
{
    public class UserTicket
    {

        [ForeignKey(nameof(Ticket))]
        public Guid TicketId { get; set; }

        public Ticket Ticket { get; set; }

        [ForeignKey(nameof(ApplicationUser))]
        public Guid UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
