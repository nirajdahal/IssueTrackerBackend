using Library.Entities.Models.UsersProjects;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace IssueTracker.ApiHelper
{
    public class CheckProjectOwner
    {
        public static bool IsProjectOwner(ClaimsPrincipal User, IEnumerable<ProjectManager> userProject)
        {
            string userId = User.Claims.ToList()[0].Value;
            string userRole = User.Claims.ToList()[3].Value;

            var adminProjectManager = new List<string> { "Admin", "Project Manager" };

            var isTicketOwner = userProject.Any(x => x.Id.Equals(userId));
            var isAdminOrProjectManager = adminProjectManager.Contains(userRole);

            if (!(isTicketOwner || isAdminOrProjectManager))
            {
                return false;
            }
            return true;
        }
    }
}