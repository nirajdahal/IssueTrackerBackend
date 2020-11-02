using Library.Entities.Models;
using Library.Entities.Models.Projects;
using Library.Entities.Models.Tickets;
using Library.Entities.Models.UsersProjects;
using Library.Entities.Models.UsersTickets;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Library.Entities
{
    public class RepositoryContext : IdentityDbContext
    {
        public RepositoryContext(DbContextOptions options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProjectManager>().HasKey(sc => new { sc.ProjectId, sc.Id });
            modelBuilder.Entity<TicketComment>().HasKey(sc => new { sc.TicketId, sc.Id });
            modelBuilder.Entity<UserProject>().HasKey(sc => new { sc.ProjectId, sc.Id });
            modelBuilder.Entity<UserTicket>().HasKey(sc => new { sc.TicketId, sc.Id });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<Project> Projects { get; set; }
    }
}