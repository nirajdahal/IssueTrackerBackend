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
    [Route("api/type")]
    [ApiController]
    public class TicketTypeController : ControllerBase
    {
        private IRepositoryManager _repo;
        private ILoggerManager _logger;
        private IMapper _mapper;
        protected RepositoryContext _context;

        public TicketTypeController(IRepositoryManager repo, ILoggerManager logger, IMapper mapper, RepositoryContext context)
        {
            _repo = repo;
            _logger = logger;
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Developer, Project Manager, Submitter")]
        public async Task<IActionResult> GetAllTicketType()
        {
            var ticketTypes = await _repo.TicketType.GetAllTicketType();
            var ticketTypeToReturn = _mapper.Map<IEnumerable<TicketTypeVmDto>>(ticketTypes);
            return Ok(ticketTypeToReturn);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Developer, Project Manager, Submitter")]
        public async Task<IActionResult> CreateTicketType(TicketTypeVmDto newtype)
        {
            if (newtype == null)
            {
                _logger.LogError("Ticket object sent from client is null.");
                return BadRequest("Empty Ticket Cannot Be Created");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the Ticket");
                return UnprocessableEntity(ModelState);
            }

            var typeToCreate = _mapper.Map<TicketType>(newtype);
            _repo.TicketType.CreateTicketType(typeToCreate);
            await _repo.Save();

            return Ok("type Created Successfully");
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Developer, Project Manager")]
        public async Task<IActionResult> UpdateTicketType(Guid id, [FromBody] TicketTypeVmDto type)
        {
            if (type == null)
            {
                _logger.LogError("Ticket type object sent from client is null.");
                return BadRequest("Empty Ticket Cannot Be Created");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the Ticket type");
                return UnprocessableEntity(ModelState);
            }

            var typeToUpdate = _mapper.Map<TicketType>(type);

            _repo.TicketType.UpdateTicketType(typeToUpdate);
            await _repo.Save();
            return Ok("type Updated Successfully");
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Developer, Project Manager")]
        public async Task<IActionResult> DeleteTicketType(Guid id)
        {
            if (id == null)
            {
                _logger.LogError("Ticket type object sent from client is null.");
                return BadRequest("Empty Ticket Cannot Be Created");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the Ticket type");
                return UnprocessableEntity(ModelState);
            }
            var typeExist = await _repo.TicketType.GetTicketType(id);
            if (typeExist == null)
            {
                _logger.LogError("Ticket type object sent from client is null.");
                return BadRequest("Empty Ticket Cannot Be Created");
            }
            _repo.TicketType.DeleteTicketType(typeExist);
            await _repo.Save();
            return Ok("type Deleted Successfully");
        }
    }
}