using Library.Entities.Models.UsersTickets;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Contracts
{
    public interface IUserTicketRepository : IRepositoryBase<UserTicket>
    {
        Task<IEnumerable<UserTicket>> GetAllUserTicket();

        Task<IEnumerable<UserTicket>> GetUserTicket(Guid userTicketId);

        void CreateUserTicket(UserTicket userTicket);

        void DeleteUserTicket(UserTicket userTicket);

        void UpdateUserTicket(UserTicket userTicket);

        void RemoveTicketAndUser(IEnumerable<UserTicket> userTickets);
    }
}