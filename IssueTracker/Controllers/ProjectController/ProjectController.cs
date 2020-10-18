using AutoMapper;
using Library.Contracts;
using Library.Entities.DTO.ProjectDto;
using Library.Entities.Models.Projects;
using Library.Entities.Models.UsersProjects;
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
        private IMapper _mapper;

        public ProjectController(IRepositoryManager repo, ILoggerManager logger, IMapper mapper)
        {
            _repo = repo;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost]
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
            projectToCreate.CreatedAt = DateTime.Now;
            _repo.Project.CreateProject(projectToCreate);
            await _repo.Save();
            return Ok("Project Created Sucessfully");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProjects()
        {
            var projects = await _repo.Project.GetAllProjects();
            return Ok(projects);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProject(Guid id)
        {
            if (id == null)
            {
                _logger.LogError("Project object sent from client is null.");
                return BadRequest("The project you are searching doesnot exist");
            }

            //remove data with this project id from userproject because
            //we dont have any method which just updates the user project directly. For that we have to remove every
            //project user realtionship from userproject and add new one from looking at tickets
            var userProjects = await _repo.UserProject.GetUserProject(id);
            if (userProjects != null)
            {
                _repo.UserProject.RemoveProjectAndUser(userProjects);
            }

            //get user value from all the tickets which belongs to specific project otherwise we wont be
            //able to add user to a project. As all the ticket has project id and user refernce.
            //i.e all users from ticket == all user of project

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
            }

            var project = await _repo.Project.GetProject(id);

            return Ok(project);
        }

        [HttpPut(("{id}"))]
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
            var project = await _repo.Project.GetProject(id);
            return Ok(project);
        }
    }
}