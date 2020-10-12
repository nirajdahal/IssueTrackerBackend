using Library.Entities.Models.Projects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Library.Entities.Models.UsersProjects
{
    public class UserProject
    {
        [ForeignKey(nameof(Project))]
        public Guid ProjectId { get; set; }

        public Project Project { get; set; }

        [ForeignKey(nameof(ApplicationUser))]
        public Guid UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
