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
    public class TicketTypeRepository : RepositoryBase<TicketType>, ITicketTypeRepository
    {
        public TicketTypeRepository(RepositoryContext _context) : base(_context)
        {
        }

        public void CreateTicketType(TicketType type)
        {
            Create(type);
        }

        public void DeleteTicketType(TicketType type)
        {
            Delete(type);
        }

        public async Task<IEnumerable<TicketType>> GetAllTicketType()
        {
            var ticketsTypes = await FindAll().OrderBy(t => t.Name).ToListAsync();
            return ticketsTypes;
        }

        public async Task<TicketType> GetTicketType(Guid typeId)
        {
            var ticketType = await FindByCondition(x => x.Id.Equals(typeId)).SingleOrDefaultAsync();
            return ticketType;
        }

        public void UpdateTicketType(TicketType type)
        {
            Update(type);
        }
    }
}