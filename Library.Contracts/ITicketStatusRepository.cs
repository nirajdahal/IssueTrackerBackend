using Library.Entities.Models.Tickets;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Contracts
{
    public interface ITicketStatusRepository : IRepositoryBase<TicketStatus>
    {
        Task<IEnumerable<TicketStatus>> GetAllTicketStatus();

        Task<TicketStatus> GetTicketStatus(Guid statusId);

        void CreateTicketStatus(TicketStatus status);

        void DeleteTicketStatus(TicketStatus status);

        void UpdateTicketStatus(TicketStatus status);
    }
}
