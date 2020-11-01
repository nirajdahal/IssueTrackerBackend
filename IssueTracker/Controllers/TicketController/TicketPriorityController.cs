using AutoMapper;
using Library.Contracts;
using Library.Entities;
using Library.Entities.DTO.TicketDto;
using Library.Entities.Models.Tickets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IssueTracker.Controllers
{
    [Route("api/priority")]
    [ApiController]
    public class TicketPriorityController : ControllerBase
    {
        private IRepositoryManager _repo;
        private ILoggerManager _logger;
        private IMapper _mapper;
        protected RepositoryContext _context;

        public TicketPriorityController(IRepositoryManager repo, ILoggerManager logger, IMapper mapper, RepositoryContext context)
        {
            _repo = repo;
            _logger = logger;
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Developer, Project Manager, Submitter")]
        public async Task<IActionResult> GetAllTicketPriority()
        {
            var ticketTypes = await _repo.TicketPriority.GetAllTicketPriority();
            var ticketTypeToReturn = _mapper.Map<IEnumerable<TicketPriorityVmDto>>(ticketTypes);
            return Ok(ticketTypeToReturn);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Project Manager")]
        public async Task<IActionResult> CreateTicketPriority(TicketPriorityVmDto newPriority)
        {
            if (newPriority == null)
            {
                _logger.LogError("Ticket object sent from client is null.");
                return BadRequest("Empty Ticket Cannot Be Created");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the Ticket");
                return UnprocessableEntity(ModelState);
            }

            var priorityToCreate = _mapper.Map<TicketPriority>(newPriority);
            _repo.TicketPriority.CreateTicketPriority(priorityToCreate);
            await _repo.Save();

            return Ok("Priority Created Successfully");
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Project Manager")]
        public async Task<IActionResult> UpdateTicketPriority(Guid id, [FromBody] TicketPriorityVmDto priority)
        {
            if (priority == null)
            {
                _logger.LogError("Ticket Priority object sent from client is null.");
                return BadRequest("Empty Ticket Cannot Be Created");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the Ticket Priority");
                return UnprocessableEntity(ModelState);
            }

            //var priorityExist = await _repo.TicketPriority.GetTicketPriority(id);
            //if (priorityExist == null)
            //{
            //    _logger.LogError("Ticket Priority object sent from client is null.");
            //    return BadRequest("Empty Ticket Cannot Be Updated");
            //}
            var priorityToUpdate = _mapper.Map<TicketPriority>(priority);

            _repo.TicketPriority.UpdateTicketPriority(priorityToUpdate);
            await _repo.Save();
            return Ok("Priority Updated Successfully");
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, Project Manager")]
        public async Task<IActionResult> DeleteTicketPriority(Guid id)
        {
            if (id == null)
            {
                _logger.LogError("Ticket Priority object sent from client is null.");
                return BadRequest("Empty Ticket Cannot Be Created");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the Ticket Priority");
                return UnprocessableEntity(ModelState);
            }
            var priorityExist = await _repo.TicketPriority.GetTicketPriority(id);
            if (priorityExist == null)
            {
                _logger.LogError("Ticket Priority object sent from client is null.");
                return BadRequest("Empty Ticket Cannot Be Created");
            }
            _repo.TicketPriority.DeleteTicketPriority(priorityExist);
            await _repo.Save();
            return Ok("Priority Deleted Successfully");
        }
    }
}