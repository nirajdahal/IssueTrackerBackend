using Library.Contracts;
using Library.Entities;
using Library.Entities.Models.Tickets;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Repository
{
    public class TicketPriorityRepository : RepositoryBase<TicketPriority>, ITicketPriorityRepository
    {
        public TicketPriorityRepository(RepositoryContext _context) : base(_context)
        {
        }

        public void CreateTicketPriority(TicketPriority priority)
        {
            Create(priority);
        }

        public void DeleteTicketPriority(TicketPriority priority)
        {
            Delete(priority);
        }

        public async Task<IEnumerable<TicketPriority>> GetAllTicketPriority()
        {
            var ticketspriorities = await FindAll().OrderBy(p => p.Name).ToListAsync();
            return ticketspriorities;
        }

        public async Task<TicketPriority> GetTicketPriority(Guid priorityId)
        {
            var TicketPriority = await FindByCondition(x => x.Id.Equals(priorityId)).SingleOrDefaultAsync();
            return TicketPriority;
        }

        public void UpdateTicketPriority(TicketPriority priority)
        {
            Update(priority);
        }
    }
}