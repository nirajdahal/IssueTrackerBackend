﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Library.Contracts;
using Library.Entities.DTO;
using Library.Entities.DTO.ProjectDto;
using Library.Entities.Models.Projects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IssueTracker.Controllers
{
    [Route("api/project")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private IRepositoryManager _repo;
        private ILoggerManager _logger;
        private IMapper _mapper;
        public ProjectController(IRepositoryManager repo, ILoggerManager logger, IMapper mapper)
        {
            _repo = repo;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject(ProjectForCreation project)
        {
            if (project == null)
            {
                _logger.LogError("Project object sent from client is null.");
                return BadRequest("Empty Project Cannot Be Created");
            }


            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the Project");
                return UnprocessableEntity(ModelState);
            }
            var projectToCreate = _mapper.Map<Project>(project);
            projectToCreate.CreatedAt = DateTime.Now;
            _repo.Project.CreateProject(projectToCreate);
            await _repo.Save();
            return Ok();
        }
    }
}
