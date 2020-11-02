using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Entities.DTO.TicketDto
{
    public class TicketCommentVmDto
    {
        public Guid TicketId { get; set; }
        public string Description { get; set; }
    }
}