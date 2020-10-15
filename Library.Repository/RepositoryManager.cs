using Library.Contracts;
using Library.Entities;
using System.Threading.Tasks;

namespace Library.Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private RepositoryContext _context;
        private IProjectRepository _projectRepository;
        private ITicketRepository _ticketRepository;
        private ITicketTypeRepository _ticketTypeRepository;
        private ITicketStatusRepository _ticketStatusRepository;
        private ITicketPriorityRepository _ticketPriorityRepository;

        public RepositoryManager(RepositoryContext context)
        {
            _context = context;
        }

        public IProjectRepository Project
        {
            get
            {
                if (_projectRepository == null)
                {
                    _projectRepository = new ProjectRepository(_context);
                }

                return _projectRepository;
            }
        }

        public ITicketRepository Ticket
        {
            get
            {
                if (_ticketRepository == null)
                {
                    _ticketRepository = new TicketRepository(_context);
                }
                return _ticketRepository;
            }
        }

        public ITicketTypeRepository TicketType
        {
            get
            {
                if (_ticketTypeRepository == null)
                {
                    _ticketTypeRepository = new TicketTypeRepository(_context);
                }
                return _ticketTypeRepository;
            }
        }

        public ITicketStatusRepository TicketStatus
        {
            get
            {
                if (_ticketStatusRepository == null)
                {
                    _ticketStatusRepository = new TicketStatusRepository(_context);
                }
                return _ticketStatusRepository;
            }
        }

        public ITicketPriorityRepository TicketPriority
        {
            get
            {
                if (_ticketPriorityRepository == null)
                {
                    _ticketPriorityRepository = new TicketPriorityRepository(_context);
                }
                return _ticketPriorityRepository;
            }
        }

        public async Task Save() => await _context.SaveChangesAsync();
    }
}