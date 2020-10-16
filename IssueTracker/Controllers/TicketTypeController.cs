﻿using AutoMapper;
using Library.Contracts;
using Library.Entities;
using Library.Entities.Models.Tickets;
using Microsoft.AspNetCore.Mvc;
using System;
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

        [HttpPost]
        public async Task<IActionResult> CreateTicketType(TicketType type)
        {
            if (type == null)
            {
                _logger.LogError("Ticket Type object sent from client is null.");
                return BadRequest("Empty ticket Type Cannot Be Created");
            }
            _repo.TicketType.Create(type);
            await _repo.Save();
            return Ok("Ticket Type was Created Sucessfully");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTicketType(Guid id)
        {
            if (id == null)
            {
                _logger.LogError("Ticket Type object sent from client is null.");
                return BadRequest("Empty ticket Type Cannot Be Created");
            }
            var ticket = await _repo.TicketType.GetTicketType(id);
            return Ok(ticket);
        }
    }
}