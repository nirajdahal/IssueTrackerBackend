using AutoMapper;
using Library.Contracts;
using Library.Entities.DTO.TicketDto;
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
        [Authorize]
        public async Task<IActionResult> GetTicketForUser(string id)
        {
            if (id == null)
            {
            }
            var tickets = await _repo.Ticket.GetAllTickets();

            var xy = await _userManager.FindByIdAsync(id);

            if (xy.UsersTickets != null)
            {
                var xyz = xy.UsersTickets.Select(x => x.Ticket).ToList();
                var ticketsVm = _mapper.Map<IEnumerable<GetAllTicketVmDto>>(xyz);
                return Ok(ticketsVm);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}