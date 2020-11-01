using AutoMapper;
using Library.Contracts;
using Library.Entities.Models;
using Library.Entities.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace IssueTracker.Controllers
{
    [Route("api/profile")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private ILoggerManager _logger;

        public UserProfileController(UserManager<ApplicationUser> userManager, IMapper mapper, ILoggerManager logger)
        {
            _userManager = userManager;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        //[Authorize(Roles = "Admin, Submitter")]
        [Authorize]
        //GET : /api/profile
        public async Task<Object> GetUserProfile()
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);

            return new
            {
                user.Name,
                user.Email,
                user.UserName,
            };
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePassword data)
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                _logger.LogError("Ticket type object sent from client is null.");
                return BadRequest("Username or Password Was Wrong");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the Ticket type");
                return UnprocessableEntity(ModelState);
            }

            var comparePassword = await _userManager.CheckPasswordAsync(user, data.OldPassword);

            if (!comparePassword)
            {
                _logger.LogError("Ticket type object sent from client is null.");
                return BadRequest("Username or Password Was Wrong");
            }

            await _userManager.ChangePasswordAsync(user, data.OldPassword, data.NewPassword);
            return Ok("Password Changed Successfully");
        }
    }
}