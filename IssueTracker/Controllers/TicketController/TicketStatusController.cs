using AutoMapper;
using Library.Contracts;
using Library.Entities;
using Library.Entities.DTO.TicketDto;
using Library.Entities.Models.Tickets;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IssueTracker.Controllers
{
    [Route("api/status")]
    [ApiController]
    public class TicketStatusController : ControllerBase
    {
        private IRepositoryManager _repo;
        private ILoggerManager _logger;
        private IMapper _mapper;
        protected RepositoryContext _context;

        public TicketStatusController(IRepositoryManager repo, ILoggerManager logger, IMapper mapper, RepositoryContext context)
        {
            _repo = repo;
            _logger = logger;
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTicketStatus()
        {
            var ticketTypes = await _repo.TicketStatus.GetAllTicketStatus();
            var ticketTypeToReturn = _mapper.Map<IEnumerable<TicketStatusVmDto>>(ticketTypes);
            return Ok(ticketTypeToReturn);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTicketStatus(TicketStatusVmDto newstatus)
        {
            if (newstatus == null)
            {
                _logger.LogError("Ticket object sent from client is null.");
                return BadRequest("Empty Ticket Cannot Be Created");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the Ticket");
                return UnprocessableEntity(ModelState);
            }

            var statusToCreate = _mapper.Map<TicketStatus>(newstatus);
            _repo.TicketStatus.CreateTicketStatus(statusToCreate);
            await _repo.Save();

            return Ok("status Created Successfully");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTicketStatus(Guid id, [FromBody] TicketStatusVmDto status)
        {
            if (status == null)
            {
                _logger.LogError("Ticket status object sent from client is null.");
                return BadRequest("Empty Ticket Cannot Be Created");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the Ticket status");
                return UnprocessableEntity(ModelState);
            }

            //var statusExist = await _repo.TicketStatus.GetTicketStatus(id);
            //if (statusExist == null)
            //{
            //    _logger.LogError("Ticket status object sent from client is null.");
            //    return BadRequest("Empty Ticket Cannot Be Updated");
            //}
            var statusToUpdate = _mapper.Map<TicketStatus>(status);

            _repo.TicketStatus.UpdateTicketStatus(statusToUpdate);
            await _repo.Save();
            return Ok("status Updated Successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicketStatus(Guid id)
        {
            if (id == null)
            {
                _logger.LogError("Ticket status object sent from client is null.");
                return BadRequest("Empty Ticket Cannot Be Created");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the Ticket status");
                return UnprocessableEntity(ModelState);
            }
            var statusExist = await _repo.TicketStatus.GetTicketStatus(id);
            if (statusExist == null)
            {
                _logger.LogError("Ticket status object sent from client is null.");
                return BadRequest("Empty Ticket Cannot Be Created");
            }
            _repo.TicketStatus.DeleteTicketStatus(statusExist);
            await _repo.Save();
            return Ok("status Deleted Successfully");
        }
    }
}