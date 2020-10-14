using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Library.Contracts;
using Library.Entities;
using Library.Entities.Models.Tickets;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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


        [HttpPost]
        public async Task<IActionResult> CreateTicket(TicketPriority ticket)
        {

            _context.Add(ticket);
           await  _context.SaveChangesAsync();
            
            await _repo.Save();
            return Ok("Priority Created Sucessfully");
        }
    }
}
