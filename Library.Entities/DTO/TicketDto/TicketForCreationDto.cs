using System;
using System.ComponentModel.DataAnnotations;

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

        public string TTypeId { get; set; }

        public string TPriorityId { get; set; }

        public Guid TStatusId { get; set; }

        public string ProjectId { get; set; }
    }
}