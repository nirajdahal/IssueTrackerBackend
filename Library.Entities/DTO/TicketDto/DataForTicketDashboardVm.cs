using Library.Entities.Models.Tickets;
using System.Collections.Generic;

namespace Library.Entities.DTO.TicketDto
{
    public class DataForTicketDashboardVm
    {
        public List<DashboardDataForTicket> TicketPriorityData { get; set; }

        public List<DashboardDataForTicket> TicketTypeData { get; set; }

        public List<DashboardDataForTicket> TicketStatusData { get; set; }

        public int totalTickets { get; set; }
    }
}