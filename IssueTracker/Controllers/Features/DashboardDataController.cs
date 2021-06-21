using Library.Entities;
using Library.Entities.Models;
using Library.Entities.Models.Dashboard;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssueTracker.Controllers.Features
{
    [Route("api/dashboard")]
    [ApiController]
   // [Authorize(Roles = "Admin, Project Manager,Developer, Submitter")]
    public class DashboardDataController : ControllerBase
    {
        private readonly RepositoryContext _context;

        private readonly RoleManager<IdentityRole> _roleManager;
        private UserManager<ApplicationUser> _userManager;

        public DashboardDataController(RoleManager<IdentityRole> roleManager, RepositoryContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet("userrole")]
       public async Task<IActionResult> DashboardDataForRolesCount()
        {

            var allRoles = await _context.Roles.ToListAsync();
            List<UserRolesCount> allUserRoles = new List<UserRolesCount>();
            
         
            foreach (var role in allRoles)
            {
                UserRolesCount userRoles = new UserRolesCount();
                var userRoleCount = await _context.UserRoles.Where(x => x.RoleId == role.Id).CountAsync();
                userRoles.Role = role.Name;
                userRoles.Total = userRoleCount;
                allUserRoles.Add(userRoles);

            }

            return Ok(allUserRoles);
        
        }

        [HttpGet("ticketproject")]
        public async Task<IActionResult> ProjectTicketCount()
        {

            

            var projectCount = await _context.Projects.CountAsync();
            var ticketsCount = await _context.Tickets.CountAsync();

            return Ok(new {ProjectCount =projectCount, TicketCount=ticketsCount });

        }

    }
}
