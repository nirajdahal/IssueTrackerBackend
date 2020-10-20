using AutoMapper;
using Library.Contracts;
using Library.Entities.DTO.ProjectDto;
using Library.Entities.DTO.UserDto;
using Library.Entities.Models;
using Library.Entities.Models.Projects;
using Library.Entities.Models.UsersProjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssueTracker.Controllers
{
    [Route("api/project")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private IRepositoryManager _repo;
        private ILoggerManager _logger;
        private UserManager<ApplicationUser> _userManager;
        private IMapper _mapper;

        public ProjectController(UserManager<ApplicationUser> userManager, IRepositoryManager repo, ILoggerManager logger, IMapper mapper)
        {
            _repo = repo;
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateProject(ProjectForCreation project)
        {
            if (project == null)
            {
                _logger.LogError("Project object sent from client is null.");
                return BadRequest("Empty Project Cannot Be Created");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the Project");
                return UnprocessableEntity(ModelState);
            }
            var projectToCreate = _mapper.Map<Project>(project);
            //Getting the username and email from jwt token to set it to CreatedBy name and email
            var userName = User.Claims.ToList()[1].Value;
            var userEmail = User.Claims.ToList()[2].Value;
            projectToCreate.SubmittedByName = userName;
            projectToCreate.SubmittedByEmail = userEmail;
            projectToCreate.CreatedAt = DateTime.Now;
            _repo.Project.CreateProject(projectToCreate);
            await _repo.Save();
            return Ok("Project Created Sucessfully");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProjects()
        {
            var projects = await _repo.Project.GetAllProjects();
            var projectToReturn = _mapper.Map<IEnumerable<ProjectDto>>(projects);
            return Ok(projectToReturn);
        }

        [HttpGet("idname")]
        public async Task<IActionResult> GetProjectsIdName()
        {
            var projects = await _repo.Project.GetAllProjects();
            var projectToReturn = _mapper.Map<IEnumerable<ProjectIdNameVm>>(projects);
            return Ok(projectToReturn);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProject(Guid id)
        {
            if (id == null)
            {
                _logger.LogError("Project object sent from client is null.");
                return BadRequest("The project you are searching doesnot exist");
            }

            /*remove data with this project id from userproject because
                we dont have any method which just updates the user project directly. For that we have to remove every
            project user realtionship from userproject and add new one from looking at tickets
            */
            var userProjects = await _repo.UserProject.GetUserProject(id);
            if (userProjects != null)
            {
                _repo.UserProject.RemoveProjectAndUser(userProjects);
            }

            /*get user value from all the tickets which belongs to specific project otherwise we wont be
            able to add user to a project. As all the ticket has project id and user refernce.
            i.e all users from ticket == all user of project */

            var tickets = await _repo.Ticket.GetAllTickets();
            var ticketsWithThisProject = tickets.Where(t => t.ProjectId.Equals(id)).ToList();
            var projectWithThisTicketUsers = ticketsWithThisProject.Select(x => x.UsersTickets);

            List<UserProject> trackProject = new List<UserProject>();
            foreach (var projectWithThisTicketUser in projectWithThisTicketUsers)
            {
                foreach (var user in projectWithThisTicketUser)
                {
                    UserProject userProject = new UserProject
                    {
                        Id = user.Id,
                        ProjectId = id
                    };

                    //this checks whether the track project list already has user added to it
                    //because if we allow duplicate userproject in the list and later create userproject
                    //it will throw an error
                    var checkDup = trackProject.Any(x => x.Id.Equals(userProject.Id));

                    if (!checkDup)
                    {
                        trackProject.Add(userProject);
                    }
                }
            }
            foreach (var userProject in trackProject)
            {
                _repo.UserProject.CreateUserProject(userProject);
                await _repo.Save();
            }

            var project = await _repo.Project.GetProject(id);

            var projectToReturn = _mapper.Map<ProjectDto>(project);

            foreach (var user in projectToReturn.UsersProjects)
            {
                var applicationUser = await _userManager.FindByIdAsync(user.Id);
                var userRole = await _userManager.GetRolesAsync(applicationUser);
                user.ApplicationUser = new ApplicationUserVm();
                user.ApplicationUser.userEmail = applicationUser.Email;
                user.ApplicationUser.userName = applicationUser.Name;
                user.ApplicationUser.userRole = userRole.ToList();
            }
            return Ok(projectToReturn);
        }

        [HttpPut(("{id}"))]
        [Authorize]
        public async Task<IActionResult> UpdateProject(Guid id, [FromBody] ProjectForUpdateDto projectToUpdate)
        {
            if (id == null)
            {
                _logger.LogError("Project object sent from client is null.");
                return BadRequest("The project you are searching doesnot exist");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the Project");
                return UnprocessableEntity(ModelState);
            }
            var originalproject = await _repo.Project.GetProject(id);

            if (originalproject == null)
            {
                _logger.LogError("Invalid project id");
                return BadRequest("The project that you are trying to update doesnot exist");
            }
            //tracking previous records because to update the project we need these information
            var user = await _repo.UserProject.GetUserProject(id);
            var submittedByName = originalproject.SubmittedByName;
            var submittedByEmail = originalproject.SubmittedByEmail;
            var createdAt = originalproject.CreatedAt;

            //Remove previous UserProject from database

            var userProject = await _repo.UserProject.GetUserProject(id);
            if (userProject != null)
            {
                _repo.UserProject.RemoveProjectAndUser(userProject);
                await _repo.Save();
            }

            //Mapping
            var finalprojectToUpdate = _mapper.Map(projectToUpdate, originalproject);
            finalprojectToUpdate.SubmittedByEmail = submittedByEmail;
            finalprojectToUpdate.SubmittedByName = submittedByName;
            finalprojectToUpdate.CreatedAt = createdAt;
            finalprojectToUpdate.UsersProjects = user.ToList();
            _repo.Project.UpdateProject(finalprojectToUpdate);
            await _repo.Save();
            return Ok("Project Created Sucessfully");
        }

        [HttpDelete(("{id}"))]
        public async Task<IActionResult> DeleteProject(Guid id)
        {
            if (id == null)
            {
                _logger.LogError("Project object sent from client is null.");
                return BadRequest("Empty Project Cannot Be Deleted");
            }
            var projectToDelete = await _repo.Project.GetProject(id);

            if (projectToDelete == null)
            {
                _logger.LogError("Invalid ticket id");
                return BadRequest("The ticket that you are trying to update doesnot exist");
            }
            _repo.Project.DeleteProject(projectToDelete);
            await _repo.Save();
            return Ok("Deleted Sucesfully");
        }
    }
}