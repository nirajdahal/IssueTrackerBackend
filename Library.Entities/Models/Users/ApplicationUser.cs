using Library.Entities.Models.Tickets;
using Library.Entities.Models.UsersProjects;
using Library.Entities.Models.UsersTickets;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Library.Entities.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public ICollection<UserProject> UsersProjects { get; set; }
        public ICollection<UserTicket> UsersTickets { get; set; }
        public ICollection<TicketComment> TicketComments { get; set; }
    }
}