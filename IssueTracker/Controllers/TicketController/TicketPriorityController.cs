using AutoMapper;
using Library.Contracts;
using Library.Entities;
using Library.Entities.DTO.TicketDto;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> GetAllTicketPriority()
        {
            var ticketTypes = await _repo.TicketPriority.GetAllTicketPriority();
            var ticketTypeToReturn = _mapper.Map<IEnumerable<TicketPriorityVmDto>>(ticketTypes);
            return Ok(ticketTypeToReturn);
        }
    }
}