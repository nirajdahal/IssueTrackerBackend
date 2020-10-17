using Library.Entities.Models.Tickets;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Contracts
{
    public interface ITicketRepository : IRepositoryBase<Ticket>
    {
        Task<IEnumerable<Ticket>> GetAllTickets();

        Task<Ticket> GetTicket(Guid ticketId);

        void CreateTicket(Ticket ticket);

        void DeleteTicket(Ticket ticket);

        void UpdateTicket(Ticket ticket);

        //Task<IEnumerable<Project>> GetProjectByIds(IEnumerable<Guid> ids);
    }
}