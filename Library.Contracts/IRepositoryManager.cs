using Library.Contracts.ProjectManagerContracts;
using Library.Contracts.TicketContracts;
using Library.Contracts.UserProjectContracts;
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

        ITicketCommentRepository TicketComment { get; }

        IUserTicketRepository UserTicket { get; }
        IUserProjectRepository UserProject { get; }

        IProjectManagerRepository ProjectManager { get; }

        Task Save();
    }
}