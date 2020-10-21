using Library.Entities.Models.UsersProjects;
using Library.Entities.Models.UsersTickets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IssueTracker.ApiHelper
{
    [Authorize]
    public static class CheckTicketOwner
    {
        public static bool IsTicketOwner(ClaimsPrincipal User, IEnumerable<UserTicket> userTicket)
        {
            string userId = User.Claims.ToList()[0].Value;
            string userRole = User.Claims.ToList()[3].Value;

            var adminProjectManager = new List<string> { "Admin", "Project Manager" };

            var isTicketOwner = userTicket.Any(x => x.Id.Equals(userId));
            var isAdminOrProjectManager = adminProjectManager.Contains(userRole);

            if (!(isTicketOwner || isAdminOrProjectManager))
            {
                return false;
            }
            return true;
        }
    }
}