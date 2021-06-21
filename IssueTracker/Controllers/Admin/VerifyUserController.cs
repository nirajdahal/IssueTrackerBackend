using Library.Entities;
using Library.Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssueTracker.Controllers.Admin
{
    [Route("api/adminaccess")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    [EnableCors("CorsPolicy")]
    public class VerifyUserController : ControllerBase
    {
        private readonly RepositoryContext _context;

        private readonly RoleManager<IdentityRole> _roleManager;
        private UserManager<ApplicationUser> _userManager;
       
        public VerifyUserController(RoleManager<IdentityRole> roleManager, RepositoryContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
     
        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
        
                var users = await _context.Users.Select(x => new {Name= x.UserName,Email= x.Email,Id= x.Id}).ToListAsync() ;
                return Ok(users);
          
          
        }


        [HttpGet("roles")]
        public async Task<IActionResult> GetAllRoles()
        {

            var roles = await _context.Roles.Select(x => new { Id =x.Id, Name=x.Name}).ToListAsync();
            return Ok(roles);


        }
   
    
        [HttpPost("userrole")]
        public async Task<IActionResult> ChangeUserRole(UserRoleForModification userRole)
        {
            var userToUpdate = await _userManager.FindByIdAsync(userRole.UserId.ToString());
            var currentRole = await _userManager.GetRolesAsync(userToUpdate);
            if(userToUpdate! == null)
            {
                return NotFound("User cannot be found");
            }
            var userRoles = new List<string>();
            userRoles.Add(userRole.Role);
            await _userManager.RemoveFromRolesAsync(userToUpdate, currentRole);
            await _userManager.AddToRolesAsync(userToUpdate, userRoles);
            await _context.SaveChangesAsync();
            return Ok();
        }
    
        [HttpPost("createrole")]
        public async Task<IActionResult> CreateRolesAdmin(CreateRoles createRoles)
        {
            var roles = createRoles.Roles;

            foreach (var role in roles)
            {
                var roleExist = await _context.Roles.AnyAsync(r => r.Name == role);
                if (!roleExist)
                {
                    // We just need to specify a unique role name to create a new role
                    IdentityRole identityRole = new IdentityRole
                    {
                        Name = role
                    };

                    await _roleManager.CreateAsync(identityRole);
                    await _context.SaveChangesAsync();
                }
            }
            return Ok();
        }
    }
}
