﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Entities.Models.Tickets
{
    public class TicketType
    {
        [Column("TTypeId")]
        public Guid Id { get; set; }

        public string Name { get; set; }
        public ICollection<Ticket> Ticket { get; set; }
    }
}