using Library.Entities.Models.Projects;
using Library.Entities.Models.Tickets;
using Library.Entities.Models.UsersTickets;
using System;
using System.Collections.Generic;

namespace Library.Entities.DTO.TicketDto
{
    public class GetAllTicketVmDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string UpdatedByName { get; set; }
        public string UpdatedByEmail { get; set; }

        public string SubmittedByName { get; set; }
        public string SubmittedByEmail { get; set; }

        public ICollection<TicketComment> Comments { get; set; }

        public Guid TTypeId { get; set; }

        public TicketType TicketType { get; set; }

        public TicketStatus TicketStatus { get; set; }

        public TicketPriorityVmDto TicketPriorityVm { get; set; }

        public Project Project { get; set; }

        public ICollection<UserTicket> UsersTickets { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}