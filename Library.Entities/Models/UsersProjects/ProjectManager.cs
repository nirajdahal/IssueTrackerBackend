using Library.Entities.Models.Projects;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Entities.Models.UsersProjects
{
    public class ProjectManager
    {
        [Key]
        public Guid projectManagerId { get; set; }

        [ForeignKey(nameof(Project))]
        public Guid ProjectId { get; set; }

        public Project Project { get; set; }

        [ForeignKey(nameof(ApplicationUser))]
        [Column("UserId")]
        public string Id { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}