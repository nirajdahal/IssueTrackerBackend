﻿using AutoMapper;
using Library.Contracts;
using Library.Entities.DTO.TicketDto;
using Library.Entities.DTO.UserDto;
using Library.Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssueTracker.Controllers
{
    [Route("api/userticket")]
    [ApiController]
    public class UserTicketController : ControllerBase
    {
        private IRepositoryManager _repo;
        private ILoggerManager _logger;
        private IMapper _mapper;
        private UserManager<ApplicationUser> _userManager;

        public UserTicketController(UserManager<ApplicationUser> userManager, IRepositoryManager repo, ILoggerManager logger, IMapper mapper)
        {
            _repo = repo;
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Project Manager,Developer, Submitter")]
        public async Task<IActionResult> GetTicketsForUser(string id)
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
            /* adding this code will help us get Application User in the
              code var allTickets = await _repo.Ticket.GetAllTickets();
              research about it why this is showing this behaviour as we can change the
              similar code in ticket and project as well
            */
            var projectManagers = await _repo.ProjectManager.GetProjectManagers();
            var userstickets = await _repo.UserTicket.GetAllTicketsForUser(id);
            var allTickets = await _repo.Ticket.GetAllTickets();
            var usersTickets = allTickets.ToList().Select(x => x.UsersTickets);
            var managerTickets = allTickets.ToList().Select(x => x.Project.ProjectManagers);
            List<GetAllTicketVmDto> individualTickets = new List<GetAllTicketVmDto>();

            string userRole = User.Claims.ToList()[3].Value;

            if (userRole.Equals("Project Manager"))
            {
                foreach (var managerTicket in managerTickets)
                {
                    var tickets = managerTicket.Where(x => x.Id.Equals(id)).Select(x => x.Project.Ticket).ToList();
                    if (tickets.Count != 0)
                    {
                        var ticketToAdd = _mapper.Map<IEnumerable<GetAllTicketVmDto>>(tickets[0]);
                        individualTickets = ticketToAdd.ToList();
                    }
                }
            }
            else
            {
                foreach (var userTicket in usersTickets)
                {
                    var tickets = userTicket.Where(x => x.Id.Equals(id)).Select(x => x.Ticket).ToList();
                    if (tickets.Count != 0)
                    {
                        var ticketToAdd = _mapper.Map<GetAllTicketVmDto>(tickets[0]);
                        individualTickets.Add(ticketToAdd);
                    }
                }
            }

            foreach (var ticket in individualTickets)
            {
                foreach (var user in ticket.UsersTicketsVm)
                {
                    var userToFind = await _userManager.FindByIdAsync(user.Id);
                    var userRoleToAdd = await _userManager.GetRolesAsync(userToFind);

                    user.ApplicationUser = new ApplicationUserVm();
                    user.ApplicationUser.userEmail = userToFind.Email;
                    user.ApplicationUser.userName = userToFind.Name;
                    user.ApplicationUser.userRole = userRoleToAdd.ToList();
                }
            }

            return Ok(individualTickets);
        }
    }
}