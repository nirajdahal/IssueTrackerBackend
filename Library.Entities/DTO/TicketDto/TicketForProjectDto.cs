using System;

namespace Library.Entities.DTO.TicketDto
{
    public class TicketForProjectDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public TicketTypeVmDto TicketTypeVm { get; set; }

        public TicketStatusVmDto TicketStatusVm { get; set; }

        public TicketPriorityVmDto TicketPriorityVm { get; set; }
    }
}