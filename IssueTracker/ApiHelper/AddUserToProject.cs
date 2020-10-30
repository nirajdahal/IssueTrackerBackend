﻿using AutoMapper;
using Library.Contracts;
using Library.Entities.Models;
using Library.Entities.Models.Projects;
using Library.Entities.Models.UsersProjects;
using Library.Entities.Models.UsersTickets;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssueTracker.ApiHelper
{
    public static class AddUserToProject
    {
        public static async Task GenerateUserProject(UserManager<ApplicationUser> _userManager, IRepositoryManager _repo, ILoggerManager _logger, IMapper _mapper, Project project, IEnumerable<UserTicket> userTickets)
        {
            var userProjects = await _repo.UserProject.GetUserProject(project.Id);
            if (userProjects != null)
            {
                _repo.UserProject.RemoveProjectAndUser(userProjects);
            }
            var userTicketsForProject = userTickets.Where(x => x.Ticket.ProjectId.Equals(project.Id));
            List<UserProject> trackProject = new List<UserProject>();
            foreach (var userTicketForProject in userTicketsForProject)
            {
                UserProject userProject = new UserProject
                {
                    Id = userTicketForProject.Id,
                    ProjectId = project.Id
                };

                //this checks whether the track project list already has user added to it
                //because if we allow duplicate userproject in the list and later create userproject
                //it will throw an error
                var checkDup = trackProject.Any(x => x.Id.Equals(userProject.Id));

                if (!checkDup)
                {
                    trackProject.Add(userProject);
                }
            }
            foreach (var userProject in trackProject)
            {
                _repo.UserProject.CreateUserProject(userProject);
                await _repo.Save();
            }
        }
    }
}