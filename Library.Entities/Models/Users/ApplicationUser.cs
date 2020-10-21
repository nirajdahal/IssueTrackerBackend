using Library.Entities.Models.Tickets;
using Library.Entities.Models.UsersProjects;
using Library.Entities.Models.UsersTickets;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.Entities.Models
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(60, ErrorMessage = "Maximum length for the Title is 60 characters.")]
        public string Name { get; set; }

        public ICollection<UserProject> UsersProjects { get; set; }
        public ICollection<UserTicket> UsersTickets { get; set; }
        public ICollection<TicketComment> TicketComments { get; set; }
    }
}