using Library.Entities.Models.Projects;
using Library.Entities.Models.Tickets;
using Library.Entities.Models.UsersProjects;
using Library.Entities.Models.UsersTickets;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Library.Entities.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name{ get; set; }
        public ICollection<UserProject> UsersProjects{ get; set; }
        public ICollection<UserTicket> UsersTickets { get; set; }
    }
}
