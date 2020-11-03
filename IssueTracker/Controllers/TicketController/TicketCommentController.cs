using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Library.Contracts;
using Library.Entities;
using Library.Entities.DTO.TicketDto;
using Library.Entities.Models.Tickets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IssueTracker.Controllers
{
    [Route("api/comments")]
    [ApiController]
    public class TicketCommentController : ControllerBase
    {
        private IRepositoryManager _repo;
        private ILoggerManager _logger;
        private IMapper _mapper;
        protected RepositoryContext _context;

        public TicketCommentController(IRepositoryManager repo, ILoggerManager logger, IMapper mapper, RepositoryContext context)
        {
            _repo = repo;
            _logger = logger;
            _mapper = mapper;
            _context = context;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Developer, Project Manager, Submitter")]
        public async Task<IActionResult> GetTicketComments(Guid id)
        {
            var ticketTypes = await _repo.TicketComment.GetTicketComment(id);
            var ticketTypeToReturn = _mapper.Map<IEnumerable<TicketCommentVmDto>>(ticketTypes);
            return Ok(ticketTypeToReturn);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Project Manager, Developer")]
        public async Task<IActionResult> CreateTicketComment(TicketCommentVmDto newcomment)
        {
            if (newcomment == null)
            {
                _logger.LogError("Ticket object sent from client is null.");
                return BadRequest("Empty Ticket Cannot Be Created");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the Ticket");
                return UnprocessableEntity(ModelState);
            }
            var userName = User.Claims.ToList()[1].Value;
            var userEmail = User.Claims.ToList()[2].Value;
            var commentToCreate = _mapper.Map<TicketComment>(newcomment);
            commentToCreate.CreatedBy = userName + " " + userEmail;
            commentToCreate.CreatedAt = DateTime.Now;
            _repo.TicketComment.CreateTicketComment(commentToCreate);
            await _repo.Save();

            return Ok("comment Created Successfully");
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Project Manager")]
        public async Task<IActionResult> UpdateTicketComment(Guid id, [FromBody] TicketCommentVmDto comment)
        {
            if (comment == null)
            {
                _logger.LogError("Ticket comment object sent from client is null.");
                return BadRequest("Empty Ticket Cannot Be Created");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the Ticket comment");
                return UnprocessableEntity(ModelState);
            }

            var commentToUpdate = _mapper.Map<TicketComment>(comment);

            _repo.TicketComment.UpdateTicketComment(commentToUpdate);
            await _repo.Save();
            return Ok("comment Updated Successfully");
        }

        //[HttpDelete("{id}")]
        //[Authorize(Roles = "Admin, Project Manager")]
        //public async Task<IActionResult> DeleteTicketComment(Guid id)
        //{
        //    if (id == null)
        //    {
        //        _logger.LogError("Ticket comment object sent from client is null.");
        //        return BadRequest("Empty Ticket Cannot Be Created");
        //    }
        //    if (!ModelState.IsValid)
        //    {
        //        _logger.LogError("Invalid model state for the Ticket comment");
        //        return UnprocessableEntity(ModelState);
        //    }
        //    var commentExist = await _repo.TicketComment.GetTicketComment(id);
        //    if (commentExist == null)
        //    {
        //        _logger.LogError("Ticket comment object sent from client is null.");
        //        return BadRequest("Empty Ticket Cannot Be Created");
        //    }
        //    _repo.TicketComment.DeleteTicketComment(commentExist);
        //    await _repo.Save();
        //    return Ok("comment Deleted Successfully");
        //}
    }
}