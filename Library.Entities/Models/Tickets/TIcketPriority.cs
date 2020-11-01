using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Entities.Models.Tickets
{
    public class TicketPriority
    {
        [Column("TPriorityId")]
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}