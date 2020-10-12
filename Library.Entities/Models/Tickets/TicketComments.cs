using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Library.Entities.Models.Tickets
{
    public class TicketComments
    {

        [Column("TCommentsId")]
        public Guid Id { get; set; }

        public string Description { get; set; }
    }
}
