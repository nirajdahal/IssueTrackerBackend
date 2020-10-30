using AutoMapper;
using IssueTracker.ApiHelper;
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
            var newGuid = Guid.NewGuid(); ;
            projectToCreate.Id = newGuid;
            //Getting the username and email from jwt token to set it to CreatedBy name and email
            var userName = User.Claims.ToList()[1].Value;
            var userEmail = User.Claims.ToList()[2].Value;
            projectToCreate.SubmittedByName = userName;
            projectToCreate.SubmittedByEmail = userEmail;
            projectToCreate.CreatedAt = DateTime.Now;
            if (projectToCreate.ProjectManagers != null)
            {
                foreach (var projectManager in project.ProjectManagers)
                {
                    ProjectManager projectManagerToAdd = new ProjectManager();
                    projectManagerToAdd.Id = projectManager.Id;
                    projectManagerToAdd.ProjectId = newGuid;
                }
            }
            _repo.Project.CreateProject(projectToCreate);
            await _repo.Save();
            return Ok("Project Created Sucessfully");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProjects()
        {
            /* using this code var projectManagers = await _repo.ProjectManager.GetProjectManagers();
             helps us to get project manager in this code var projects = await _repo.Project.GetAllProjects();
             It is something to reasearch about why ef core shows this behaviour
             */
            var projectManagers = await _repo.ProjectManager.GetProjectManagers();
            var projects = await _repo.Project.GetAllProjects();
            var userTickets = await _repo.UserTicket.GetUsersTickets();
            foreach (var project in projects)
            {
                await AddUserToProject.GenerateUserProject(_userManager, _repo, _logger, _mapper, project, userTickets);
            }

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
            var projectManagers = await _repo.ProjectManager.GetProjectManagers();
            var project = await _repo.Project.GetProject(id);
            var userTickets = await _repo.UserTicket.GetUsersTickets();
            await AddUserToProject.GenerateUserProject(_userManager, _repo, _logger, _mapper, project, userTickets);
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

            var projectManager = await _repo.ProjectManager.GetProjectManager(id);

            //check for owner
            var isOwner = CheckProjectOwner.IsProjectOwner(User, projectManager);
            if (!isOwner)
            {
                return Unauthorized("Sorry! You cannot modify this project.");
            }

            var submittedByName = originalproject.SubmittedByName;
            var submittedByEmail = originalproject.SubmittedByEmail;
            var createdAt = originalproject.CreatedAt;
            var userProjects = originalproject.UsersProjects;

            //Have to figure out a way to update many to many realtionship from anywhere otherwise like this
            //the code will be much longer if we have
            //Remove previous Project Manager from database
            if (projectManager != null)
            {
                _repo.ProjectManager.RemoveProjectAndManager(projectManager);
                await _repo.Save();
            }

            //Remove previous UserProject from database
            if (userProjects != null)
            {
                _repo.UserProject.RemoveProjectAndUser(userProjects);
                await _repo.Save();
            }

            //Mapping
            var finalprojectToUpdate = _mapper.Map(projectToUpdate, originalproject);
            finalprojectToUpdate.SubmittedByEmail = submittedByEmail;
            finalprojectToUpdate.SubmittedByName = submittedByName;
            finalprojectToUpdate.CreatedAt = createdAt;
            finalprojectToUpdate.UsersProjects = userProjects.ToList();
            _repo.Project.UpdateProject(finalprojectToUpdate);
            await _repo.Save();
            return Ok("Project Updated Sucessfully");
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

            var projectManager = await _repo.ProjectManager.GetProjectManager(id);

            //check for owner
            var isOwner = CheckProjectOwner.IsProjectOwner(User, projectManager);
            if (!isOwner)
            {
                return Unauthorized("Sorry! You cannot modify this project.");
            }
            _repo.Project.DeleteProject(projectToDelete);
            await _repo.Save();
            return Ok("Deleted Sucesfully");
        }
    }
}