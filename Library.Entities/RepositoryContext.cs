
using Library.Entities.Models;
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
            : base(options) {
        }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }
    }
}
