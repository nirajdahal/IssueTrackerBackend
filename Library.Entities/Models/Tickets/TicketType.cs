using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Library.Entities.Models.Tickets
{
    public class TicketType
    {
        [Column("TTypeId")]
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
