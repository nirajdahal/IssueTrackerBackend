using Library.Entities.Models.UsersProjects;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.Entities.DTO.ProjectDto
{
    public class ProjectForCreation
    {
        [MaxLength(60, ErrorMessage = "Maximum length for the Title is 60 characters.")]
        public string Title { get; set; }

        [Required]
        [MaxLength(160, ErrorMessage = "Maximum length for the Title is 160 characters.")]
        public string Description { get; set; }

        public ICollection<ProjectManager> ProjectManagers; // get project managers using action method in user controller
    }
}