using Library.Contracts.TicketContracts;
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
    public class TicketCommentRepository : RepositoryBase<TicketComment>, ITicketCommentRepository
    {
        public TicketCommentRepository(RepositoryContext _context) : base(_context)
        {
        }

        public void CreateTicketComment(TicketComment comment)
        {
            Create(comment);
        }

        public void DeleteTicketComment(TicketComment comment)
        {
            Delete(comment);
        }

        public async Task<IEnumerable<TicketComment>> GetAllTicketComment()
        {
            var ticketspriorities = await FindAll().ToListAsync();
            return ticketspriorities;
        }

        public async Task<List<TicketComment>> GetTicketComment(Guid ticketId)
        {
            var TicketComment = await FindAll().Where(x => x.TicketId.Equals(ticketId)).ToListAsync();
            return TicketComment;
        }

        public void UpdateTicketComment(TicketComment comment)
        {
            Update(comment);
        }
    }
}