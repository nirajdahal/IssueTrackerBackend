using Library.Contracts;
using Library.Contracts.ProjectManagerContracts;
using Library.Contracts.TicketContracts;
using Library.Contracts.UserProjectContracts;
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
        private ITicketCommentRepository _ticketcommentRepository;
        private ITicketPriorityRepository _ticketPriorityRepository;
        private IUserTicketRepository _userTicketRepository;
        private IUserProjectRepository _userProjectRepository;
        private IProjectManagerRepository _projectManagerRepository;

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

        public ITicketCommentRepository TicketComment
        {
            get
            {
                if (_ticketcommentRepository == null)
                {
                    _ticketcommentRepository = new TicketCommentRepository(_context);
                }
                return _ticketcommentRepository;
            }
        }

        public IUserTicketRepository UserTicket
        {
            get
            {
                if (_userTicketRepository == null)
                {
                    _userTicketRepository = new UserTicketRepository(_context);
                }
                return _userTicketRepository;
            }
        }

        public IUserProjectRepository UserProject
        {
            get
            {
                if (_userProjectRepository == null)
                {
                    _userProjectRepository = new UserProjectRepository(_context);
                }
                return _userProjectRepository;
            }
        }

        public IProjectManagerRepository ProjectManager
        {
            get
            {
                if (_projectManagerRepository == null)
                {
                    _projectManagerRepository = new ProjectManagerRepository(_context);
                }
                return _projectManagerRepository;
            }
        }

        public async Task Save() => await _context.SaveChangesAsync();
    }
}