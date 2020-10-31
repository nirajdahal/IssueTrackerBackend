using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Library.Contracts;
using Library.Entities.DTO.TicketDto;
using Library.Entities.DTO.UserDto;
using Library.Entities.DTO.UserProjectsDto;
using Library.Entities.Models;
using Library.Entities.Models.UsersProjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IssueTracker.Controllers.UserProjectController
{
    [Route("api/userproject")]
    [ApiController]
    public class UserProjectController : ControllerBase
    {
        private IRepositoryManager _repo;
        private ILoggerManager _logger;
        private IMapper _mapper;
        private UserManager<ApplicationUser> _userManager;

        public UserProjectController(UserManager<ApplicationUser> userManager, IRepositoryManager repo, ILoggerManager logger, IMapper mapper)
        {
            _repo = repo;
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetProjectsForUser(string id)
        {
            if (id == null)
            {
                _logger.LogError("Ticket id  sent from client is null.");
                return BadRequest("Ticket doesnot exist");
            }

            var userExist = await _userManager.FindByIdAsync(id);
            if (userExist == null)
            {
                _logger.LogError("Ticket id  sent from client is null.");
                return NotFound("Ticket doesnot exist");
            }

            var projectManagers = await _repo.ProjectManager.GetProjectManagers();
            List<UserProject> usersProject = new List<UserProject>();
            string userRole = User.Claims.ToList()[3].Value;

            if (userRole == "Project Manager")
            {
                var data = projectManagers.Where(x => x.Id.Equals(id));
                usersProject = data.Select(x => x.Project).SelectMany(x => x.UsersProjects).ToList();
            }
            else
            {
                usersProject = (await _repo.UserProject.GetAllProjectsForUser(id)).ToList();
            }

            foreach (var userProject in usersProject)
            {
                var user = await _repo.UserProject.GetUserProject(userProject.ProjectId);
            }

            var userProjectsToReturn = _mapper.Map<IEnumerable<UserProjectVmDto>>(usersProject);
            return Ok(userProjectsToReturn.Select(x => x.Project));
        }

        [HttpGet("project/{id}")]
        [Authorize]
        public async Task<IActionResult> GetUsersProject(Guid Id)
        {
            var usersProject = await _repo.UserProject.GetUserProject(Id);
            return Ok(usersProject);
        }
    }
}