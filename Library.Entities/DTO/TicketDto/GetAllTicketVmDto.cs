using Library.Entities.DTO.ProjectDto;
using Library.Entities.DTO.UsersTicketsDto;
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

        public TicketTypeVmDto TicketTypeVm { get; set; }

        public TicketStatusVmDto TicketStatusVm { get; set; }

        public TicketPriorityVmDto TicketPriorityVm { get; set; }

        public ProjectForTicketDto ProjectVm { get; set; }

        public ICollection<UserTicketVmDto> UsersTicketsVm { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}