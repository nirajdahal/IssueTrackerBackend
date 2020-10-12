using Library.Contracts;
using Library.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private RepositoryContext _context;
        private IProjectRepository _projectRepository;
        private ITicketRepository _ticketRepository;
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
        public async Task Save() => await _context.SaveChangesAsync();
    }
}

