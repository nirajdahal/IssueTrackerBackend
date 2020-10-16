﻿using AutoMapper;
using Library.Contracts;
using Library.Entities;
using Library.Entities.DTO.TicketDto;
using Library.Entities.Models.Tickets;
using Library.Entities.Models.UsersTickets;
using Microsoft.AspNetCore.Authorization;
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
        private RepositoryContext _context;
        private ILoggerManager _logger;
        private IMapper _mapper;
        private IRepositoryManager _repo;

        public TicketController(IRepositoryManager repo, ILoggerManager logger, IMapper mapper, RepositoryContext context)
        {
            _repo = repo;
            _logger = logger;
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTickets()
        {
            var tickets = await _repo.Ticket.GetAllTickets();
            var ticketsVm = _mapper.Map<IEnumerable<GetAllTicketVmDto>>(tickets);
            return Ok(ticketsVm);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateTicket(TicketForCreationDto ticket)
        {
            if (ticket == null)
            {
                _logger.LogError("Ticket object sent from client is null.");
                return BadRequest("Empty Ticket Cannot Be Created");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the Ticket");
                return UnprocessableEntity(ModelState);
            }
            ticket.TStatusId = new Guid("256097cd-4147-4c73-6355-08d86f48d2ba");

            var ticketToCreate = _mapper.Map<Ticket>(ticket);

            //Getting the username and email from jwt token to set it to CreatedBy name and email
            var userName = User.Claims.ToList()[1].Value;
            var userEmail = User.Claims.ToList()[2].Value;
            ticketToCreate.SubmittedByName = userName;
            ticketToCreate.SubmittedByEmail = userEmail;
            ticketToCreate.CreatedAt = DateTime.Now;
            _repo.Ticket.CreateTicket(ticketToCreate);
            await _repo.Save();
            return Ok("Ticket Created Sucessfully");
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateTicket(Guid id, [FromBody] TicketForUpdateDto ticketToUpdate)
        {
            if (ticketToUpdate == null)
            {
                _logger.LogError("Ticket object sent from client is null.");
                return BadRequest("Empty Ticket Cannot Be Created");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the Ticket");
                return UnprocessableEntity(ModelState);
            }

            var originalTicket = await _repo.Ticket.GetTicket(id);
            if (originalTicket == null)
            {
                _logger.LogError("Invalid ticket id");
                return BadRequest("The ticket that you are trying to update doesnot exist");
            }

            //updating the database so the previous record gets deleted in database
            var usersticketToBeRemoved = _context.Set<UserTicket>().Where(x => (x.TicketId.Equals(id)));
            _context.Set<UserTicket>().RemoveRange(usersticketToBeRemoved);
            await _context.SaveChangesAsync();

            //Getting the username and email from jwt token to set it to created by name and email
            var userName = User.Claims.ToList()[1].Value;
            var userEmail = User.Claims.ToList()[2].Value;

            ticketToUpdate.UpdatedByName = userName;
            ticketToUpdate.UpdatedByEmail = userEmail;

            //Assigning previous submitted by value because the submitter name will get deleted when we apply put method
            //And dont have to use  this kind of trick for patch request but patch is little hard to implement
            var submmiteByName = originalTicket.SubmittedByName;
            var submittedByEmail = originalTicket.SubmittedByEmail;
            ticketToUpdate.SubmittedByName = submmiteByName;
            ticketToUpdate.SubmittedByEmail = submittedByEmail;
            //now updating data
            _mapper.Map(ticketToUpdate, originalTicket);

            await _repo.Save();

            return Ok("Ticket Updated Sucessfully");
        }
    }
}