using AutoMapper;
using Library.Contracts;
using Library.Entities.DTO.TicketDto;
using Library.Entities.Models.Tickets;
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
        private IRepositoryManager _repo;
        private ILoggerManager _logger;
        private IMapper _mapper;
        public TicketController(IRepositoryManager repo, ILoggerManager logger, IMapper mapper)
        {
            _repo = repo;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTicket(TicketForCreationDto ticket)
        {
            if (ticket== null)
            {
                _logger.LogError("Ticket object sent from client is null.");
                return BadRequest("Empty Ticket Cannot Be Created");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the Ticket");
                return UnprocessableEntity(ModelState);
            }
            var ticketToCreate = _mapper.Map<Ticket>(ticket);
            ticketToCreate.CreatedAt = DateTime.Now;
            ticketToCreate.ProjectId = new Guid("C3F24DF1-60C1-45F2-DBE6-08D86FA01176");
            _repo.Ticket.CreateTicket(ticketToCreate);
            await _repo.Save();
            return Ok("Ticket Created Sucessfully");
        }

    }
}
