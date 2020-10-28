using AutoMapper;
using IssueTracker.ApiHelper;
using Library.Contracts;
using Library.Entities;
using Library.Entities.DTO.TicketDto;
using Library.Entities.DTO.UserDto;
using Library.Entities.Models;
using Library.Entities.Models.Tickets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssueTracker.Controllers
{
    [Route("api/ticket")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private ILoggerManager _logger;
        private IMapper _mapper;
        private UserManager<ApplicationUser> _userManager;
        private IRepositoryManager _repo;

        public TicketController(UserManager<ApplicationUser> userManager, IRepositoryManager repo, ILoggerManager logger, IMapper mapper, RepositoryContext context)
        {
            _repo = repo;
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllTickets()
        {
            var allProjectManagers = await _repo.ProjectManager.GetProjectManagers();
            var tickets = await _repo.Ticket.GetAllTickets();
            var ticketsVm = _mapper.Map<IEnumerable<GetAllTicketVmDto>>(tickets);

            //We only get user id from the application user
            //so we need to find the user by that id and
            //populate the user value to application user
            foreach (var userTickets in ticketsVm)
            {
                var userTicket = userTickets.UsersTicketsVm;

                foreach (var user in userTicket)
                {
                    var applicationUser = await _userManager.FindByIdAsync(user.Id);
                    var userRole = await _userManager.GetRolesAsync(applicationUser);
                    user.ApplicationUser = new ApplicationUserVm();
                    user.ApplicationUser.userEmail = applicationUser.Email;
                    user.ApplicationUser.userName = applicationUser.Name;
                    user.ApplicationUser.userRole = userRole.ToList();
                }
            }

            return Ok(ticketsVm);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetTicket(Guid id)
        {
            var tickets = await _repo.Ticket.GetTicket(id);

            if (tickets == null)
            {
                _logger.LogError("Ticket object sent from client is null.");
                return BadRequest("Ticket You Are Trying To Create Doesnot Exist");
            }
            var projectManagers = await _repo.ProjectManager.GetProjectManager(tickets.ProjectId);
            var ticketsVm = _mapper.Map<GetAllTicketVmDto>(tickets);

            var userTicket = ticketsVm.UsersTicketsVm;

            foreach (var user in userTicket)
            {
                var applicationUser = await _userManager.FindByIdAsync(user.Id);
                var userRole = await _userManager.GetRolesAsync(applicationUser);
                user.ApplicationUser = new ApplicationUserVm();
                user.ApplicationUser.userEmail = applicationUser.Email;
                user.ApplicationUser.userName = applicationUser.Name;
                user.ApplicationUser.userRole = userRole.ToList();
            }

            return Ok(ticketsVm);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateTicket(TicketForCreationDto ticket)
        {
            if (ticket == null)
            {
                _logger.LogError("Ticket object sent from client is null.");
                return BadRequest("Empty Ticket Cannot Be Created");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the Ticket");
                return UnprocessableEntity(ModelState);
            }
            ticket.TStatusId = new Guid("256097cd-4147-4c73-6355-08d86f48d2ba");

            var ticketToCreate = _mapper.Map<Ticket>(ticket);

            var userList = User.Claims.ToList();
            //Getting the username and email from jwt token to set it to CreatedBy name and email
            var userName = User.Claims.ToList()[1].Value;
            var userEmail = User.Claims.ToList()[2].Value;
            ticketToCreate.SubmittedByName = userName;
            ticketToCreate.SubmittedByEmail = userEmail;
            ticketToCreate.CreatedAt = DateTime.Now;
            _repo.Ticket.CreateTicket(ticketToCreate);
            await _repo.Save();
            return Ok("Ticket Created Sucessfully");
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateTicket(Guid id, [FromBody] TicketForUpdateDto ticketToUpdate)
        {
            if (ticketToUpdate == null)
            {
                _logger.LogError("Ticket object sent from client is null.");
                return BadRequest("Empty Ticket Cannot Be Created");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the Ticket");
                return UnprocessableEntity(ModelState);
            }

            var originalTicket = await _repo.Ticket.GetTicket(id);

            if (originalTicket == null)
            {
                _logger.LogError("Invalid ticket id");
                return BadRequest("The ticket that you are trying to update doesnot exist");
            }

            //getting usertickets from database and check the owner
            var usersticketFromDatabase = await _repo.UserTicket.GetUserTicket(id);

            var isOwner = CheckTicketOwner.IsTicketOwner(User, usersticketFromDatabase);
            if (!isOwner)
            {
                return Unauthorized("Sorry! You cannot modify this ticket.");
            }

            //updating the database so the previous record gets deleted in database
            _repo.UserTicket.RemoveTicketAndUser(usersticketFromDatabase);
            await _repo.Save();

            //Getting the username and email from jwt token to set it to created by name and email
            var userName = User.Claims.ToList()[1].Value;
            var userEmail = User.Claims.ToList()[2].Value;

            ticketToUpdate.UpdatedByName = userName;
            ticketToUpdate.UpdatedByEmail = userEmail;

            //Assigning previous submitted by value because the submitter name will get deleted when we apply put method
            //And dont have to use  this kind of trick for patch request but patch is little hard to implement
            var submmiteByName = originalTicket.SubmittedByName;
            var submittedByEmail = originalTicket.SubmittedByEmail;
            ticketToUpdate.SubmittedByName = submmiteByName;
            ticketToUpdate.SubmittedByEmail = submittedByEmail;
            ticketToUpdate.UpdatedAt = DateTime.Now;
            //now updating data
            _mapper.Map(ticketToUpdate, originalTicket);

            await _repo.Save();

            return Ok("Ticket Updated Sucessfully");
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteTicket(Guid id)
        {
            if (id == null)
            {
                _logger.LogError("Ticket id  sent from client is null.");
                return BadRequest("Empty Ticket Cannot Be Deleted");
            }
            var ticketExist = await _repo.Ticket.GetTicket(id);

            if (ticketExist == null)
            {
                _logger.LogError("Invalid ticket id");
                return BadRequest("The ticket that you are trying to delete doesnot exist");
            }

            var usersticketFromDatabase = await _repo.UserTicket.GetUserTicket(id);

            var isOwner = CheckTicketOwner.IsTicketOwner(User, usersticketFromDatabase);
            if (!isOwner)
            {
                return Unauthorized("Sorry! You cannot modify this ticket");
            }

            _repo.Ticket.DeleteTicket(ticketExist);
            await _repo.Save();
            return Ok("Sucessfully Deleted Ticket");
        }

        [HttpGet("dashboard")]
        [Authorize]
        public async Task<IActionResult> TicketDataForDashboard()
        {
            var tickets = await _repo.Ticket.GetAllTickets();
            var allTicketTypes = await _repo.TicketType.GetAllTicketType();
            var allTicketPriority = await _repo.TicketPriority.GetAllTicketPriority();
            var allTicketStatus = await _repo.TicketStatus.GetAllTicketStatus();

            var ticketsVm = _mapper.Map<IEnumerable<GetAllTicketVmDto>>(tickets);
            var allTicketTypesVm = _mapper.Map<IEnumerable<TicketTypeVmDto>>(allTicketTypes);
            var allTicketPriorityVm = _mapper.Map<IEnumerable<TicketPriorityVmDto>>(allTicketPriority);
            var allTicketStatusVm = _mapper.Map<IEnumerable<TicketStatusVmDto>>(allTicketStatus);

            // getting values for Ticket Type
            var ticketTypesVm = ticketsVm.Select(x => x.TicketTypeVm);
            List<DashboardData> ticketTypesList = new List<DashboardData>();
            foreach (var types in allTicketTypesVm)
            {
                var individualTicketType = ticketTypesVm.Where(x => x.Name.Equals(types.Name));
                var ticketTypeCount = individualTicketType.ToList().Count;
                var ticketTypeName = types.Name;
                var ticketTypeData = new DashboardData();
                ticketTypeData.Name = ticketTypeName;
                ticketTypeData.Count = ticketTypeCount;
                ticketTypesList.Add(ticketTypeData);
            }

            // getting values for Ticket Priority
            var ticketPriorityVm = ticketsVm.Select(x => x.TicketPriorityVm);
            List<DashboardData> ticketPriorityList = new List<DashboardData>();
            foreach (var priority in allTicketPriorityVm)
            {
                var individualPrority = ticketPriorityVm.Where(x => x.Name.Equals(priority.Name));
                var ticketPriorityCount = individualPrority.ToList().Count;
                var ticketPriorityName = priority.Name;
                var ticketPriorityData = new DashboardData();
                ticketPriorityData.Name = ticketPriorityName;
                ticketPriorityData.Count = ticketPriorityCount;
                ticketPriorityList.Add(ticketPriorityData);
            }

            // getting values for Ticket Status
            var ticketStatusVm = ticketsVm.Select(x => x.TicketStatusVm);
            List<DashboardData> ticketStatusList = new List<DashboardData>();
            foreach (var status in allTicketStatusVm)
            {
                var individualStatus = ticketStatusVm.Where(x => x.Name.Equals(status.Name));
                var ticketStatusCount = individualStatus.ToList().Count;
                var ticketStatusName = status.Name;
                var ticketStatusData = new DashboardData();
                ticketStatusData.Name = ticketStatusName;
                ticketStatusData.Count = ticketStatusCount;
                ticketStatusList.Add(ticketStatusData);
            }

            //var ticketsTypes = ticketsVm.Select(x => x.TicketTypeVm);
            //var ticketsPriority = ticketsVm.Select(x => x.TicketPriorityVm);
            //var ticketsStatus = ticketsVm.Select(x => x.TicketStatusVm);

            return Ok(ticketStatusList);
        }
    }

    public class DashboardData
    {
        public string Name { get; set; }

        public int Count { get; set; }
    }
}