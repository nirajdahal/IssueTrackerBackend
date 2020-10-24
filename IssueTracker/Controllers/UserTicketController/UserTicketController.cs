using AutoMapper;
using Library.Contracts;
using Library.Entities.DTO.TicketDto;
using Library.Entities.DTO.UserDto;
using Library.Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
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
        [Authorize]
        public async Task<IActionResult> GetTicketForUser(string id)
        {
            if (id == null)
            {
                _logger.LogError("Ticket id  sent from client is null.");
                return BadRequest("Ticket doesnot exist");
            }

            /* adding this code will help us get Application User in the
               code var user = await _repo.UserTicket.GetAllTicketsForUser(id);
               research about it why this is showing this behaviour as we can change the
               similar code in ticket and project as well
             */
            var userstickets = await _repo.UserTicket.GetAllTicketsForUser(id);

            var user = await _userManager.FindByIdAsync(id);

            if (user.UsersTickets != null)
            {
                var userTickets = user.UsersTickets.Select(x => x.Ticket).ToList();
                var ticketsVm = _mapper.Map<IEnumerable<GetAllTicketVmDto>>(userTickets);
                foreach (var users in ticketsVm)
                {
                    foreach (var userwithTicket in users.UsersTicketsVm)
                    {
                        var userVm = new ApplicationUser();
                        userVm.Id = userwithTicket.Id;
                        var userRoleD = await _userManager.GetRolesAsync(userVm);
                        //Adding user Role
                        userwithTicket.ApplicationUser.userRole = userRoleD.ToList();
                    }
                }
                return Ok(ticketsVm);
            }
            else
            {
                _logger.LogError("Invalid ticket id");
                return BadRequest();
            }
        }
    }
}