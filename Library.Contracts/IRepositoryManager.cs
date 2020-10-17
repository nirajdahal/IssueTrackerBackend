using System.Threading.Tasks;

namespace Library.Contracts
{
    public interface IRepositoryManager
    {
        IProjectRepository Project { get; }

        ITicketRepository Ticket { get; }

        ITicketTypeRepository TicketType { get; }

        ITicketStatusRepository TicketStatus { get; }

        ITicketPriorityRepository TicketPriority { get; }

        IUserTicketRepository UserTicket { get; }

        Task Save();
    }
}