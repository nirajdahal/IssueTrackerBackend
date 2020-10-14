using Library.Entities.Models.Projects;
using Library.Entities.Models.Tickets;
using Library.Entities.Models.UsersTickets;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Library.Entities.DTO.TicketDto
{
    public class TicketForCreationDto
    {


        [Required]
        [MaxLength(60, ErrorMessage = "Maximum length for the Title is 60 characters.")]
        public string Title { get; set; }

        [Required]
        [MaxLength(160, ErrorMessage = "Maximum length for the Description is 160 characters.")]
        public string Description { get; set; }

        public string SubmitterName { get; set; }

        [MaxLength(160, ErrorMessage = "Maximum length for the Comments is 160 characters.")]
        public ICollection<TicketComments> Comments { get; set; }

        public DateTime CreatedAt { get; set; }


        public Guid CompanyIdFrom { get; set; }



    }
}
