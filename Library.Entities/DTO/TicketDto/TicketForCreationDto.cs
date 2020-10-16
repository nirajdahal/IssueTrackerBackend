using Library.Entities.Models;
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

        public Guid TTypeId { get; set; }

        public Guid TPriorityId { get; set; }

        public Guid TStatusId { get; set; }

        public Guid ProjectId { get; set; }

        public ApplicationUser SubmittedBy { get; set; }
    }
}