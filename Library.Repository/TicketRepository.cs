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
    public class TicketRepository : RepositoryBase<Ticket>, ITicketRepository
    {
        public TicketRepository(RepositoryContext _context) : base(_context)
        {
        }

        public void CreateTicket(Ticket ticket)
        {
            Create(ticket);
        }

        public void DeleteTicket(Ticket ticket)
        {
            Delete(ticket);
        }

        public async Task<IEnumerable<Ticket>> GetAllTickets()
        {
            var tickets = await FindAll().OrderBy(t => t.Title).ToListAsync();
            return tickets;
        }

        public async Task<Ticket> GetTicket(Guid ticketId)
        {
            var tickets = await FindByCondition(t => t.Id.Equals(ticketId)).SingleOrDefaultAsync();
            return tickets;
        }

        public void UpdateTicket(Ticket ticket)
        {
            Update(ticket);
        }

        //public Task<IEnumerable<Ticket>> GetTicketByIds(IEnumerable<Guid> ids)
        //{
        //    throw new NotImplementedException();
        //}
    }
}