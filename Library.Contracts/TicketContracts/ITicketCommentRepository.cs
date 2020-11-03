using Library.Entities.Models.Tickets;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Contracts.TicketContracts
{
    public interface ITicketCommentRepository : IRepositoryBase<TicketComment>
    {
        Task<IEnumerable<TicketComment>> GetAllTicketComment();

        Task<List<TicketComment>> GetTicketComment(Guid commentId);

        void CreateTicketComment(TicketComment comment);

        void DeleteTicketComment(TicketComment comment);

        void UpdateTicketComment(TicketComment comment);
    }
}