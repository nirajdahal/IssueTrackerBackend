using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Library.Entities.Models.Tickets
{
    public class TicketStatus
    {
        [Column("TStatusId")]
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
