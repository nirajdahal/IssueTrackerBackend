using Library.Entities.Models.Tickets;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Contracts
{
    public interface ITicketPriorityRepository : IRepositoryBase<TicketPriority>
    {
        Task<IEnumerable<TicketPriority>> GetAllTicketPriority();

        Task<TicketPriority> GetTicketPriority(Guid priorityId);

        void CreateTicketPriority(TicketPriority priority);

        void DeleteTicketPriority(TicketPriority priority);

        void UpdateTicketPriority(TicketPriority priority);
    }
}