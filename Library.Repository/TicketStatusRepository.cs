using Library.Contracts;
using Library.Entities;
using Library.Entities.Models.Tickets;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repository
{
    public class TicketStatusRepository : RepositoryBase<TicketStatus>, ITicketStatusRepository
    {
        public TicketStatusRepository(RepositoryContext _context) : base(_context)
        {

        }
        public void CreateTicketStatus(TicketStatus status)
        {
            Create(status);
        }

        public void DeleteTicketStatus(TicketStatus status)
        {
            Delete(status);
        }

        public async Task<IEnumerable<TicketStatus>> GetAllTicketStatus()
        {
            var ticketsTypes = await FindAll().OrderBy(s => s.Name).ToListAsync();
            return ticketsTypes;
        }

        public async Task<TicketStatus> GetTicketStatus(Guid statusId)
        {
            var ticketType = await FindByCondition(x => x.Id.Equals(statusId)).SingleOrDefaultAsync();
            return ticketType;
        }

        public void UpdateTicketStatus(TicketStatus status)
        {
            Update(status);
        }
    }
}
