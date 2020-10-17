using Library.Entities.Models.Tickets;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Contracts
{
    public interface ITicketTypeRepository : IRepositoryBase<TicketType>
    {
        Task<IEnumerable<TicketType>> GetAllTicketType();

        Task<TicketType> GetTicketType(Guid typeId);

        void CreateTicketType(TicketType type);

        void DeleteTicketType(TicketType type);

        void UpdateTicketType(TicketType type);
    }
}