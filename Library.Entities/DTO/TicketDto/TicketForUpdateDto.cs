﻿using Library.Entities.Models.UsersTickets;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Library.Entities.DTO.TicketDto
{
    public class TicketForUpdateDto
    {
        [Required]
        [MaxLength(60, ErrorMessage = "Maximum length for the Title is 60 characters.")]
        public string Title { get; set; }

        [Required]
        [MaxLength(160, ErrorMessage = "Maximum length for the Description is 160 characters.")]
        public string Description { get; set; }
    
        public Guid TTypeId { get; set; }
        public Guid TPriorityId { get; set; }
        public Guid ProjectId { get; set; }
        public ICollection<UserTicket> UsersTickets {get; set;}
       
    }
}
