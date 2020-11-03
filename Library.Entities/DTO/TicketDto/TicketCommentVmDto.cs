using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Entities.DTO.TicketDto
{
    public class TicketCommentVmDto
    {
        public Guid TicketId { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
    }
}