using Library.Contracts;
using Library.Entities;
using Library.Entities.Models.UsersTickets;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Repository
{
    public class UserTicketRepository : RepositoryBase<UserTicket>, IUserTicketRepository
    {
        public UserTicketRepository(RepositoryContext _context) : base(_context)
        {
        }

        public void CreateUserTicket(UserTicket userTicket)
        {
            Create(userTicket);
        }

        public void DeleteUserTicket(UserTicket userTicket)
        {
            Delete(userTicket);
        }

        public async Task<IEnumerable<UserTicket>> GetAllTicketsForUser(string id)
        {
            var ticketsTypes = await FindByCondition(x => x.Id.Equals(id))
                .Include(x => x.Ticket)
                .Include(x => x.ApplicationUser)
                .ToListAsync();
            return ticketsTypes;
        }

        public async Task<IEnumerable<UserTicket>> GetUserTicket(Guid ticketId)
        {
            var ticketType = await FindByCondition(x => x.TicketId.Equals(ticketId))
                .Include(x => x.ApplicationUser)
                .ToListAsync();
            return ticketType;
        }

        public void UpdateUserTicket(UserTicket userTicket)
        {
            Update(userTicket);
        }

        public void RemoveTicketAndUser(IEnumerable<UserTicket> userTickets)
        {
            _context.Set<UserTicket>().RemoveRange(userTickets);
        }
    }
}