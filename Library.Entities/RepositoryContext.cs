
using Library.Entities.Models;
using Library.Entities.Models.Projects;
using Library.Entities.Models.Tickets;
using Library.Entities.Models.UsersProjects;
using Library.Entities.Models.UsersTickets;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace Library.Entities
{
    public class RepositoryContext : IdentityDbContext
    {
        public RepositoryContext(DbContextOptions options)
            : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<UserProject>().HasKey(sc => new { sc.ProjectId, sc.UserId });
            modelBuilder.Entity<UserTicket>().HasKey(sc => new { sc.TicketId, sc.UserId });
           
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }

        public DbSet<Ticket> Tickets{ get; set; }

        public DbSet<Project> Projects { get; set; }

    }
}
