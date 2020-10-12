using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Contracts
{
    public interface IRepositoryManager
    {
        IProjectRepository Project { get; }

        ITicketRepository Ticket { get; }

        Task Save();
    }
}
