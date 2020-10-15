﻿using AutoMapper;
using Library.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace IssueTracker.Controllers
{
    [Route("api/profile")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public UserProfileController(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet]
        //[Authorize(Roles ="Admin, Submitter")]
        //GET : /api/profile
        public async Task<Object> GetUserProfile()
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);

            return new
            {
                user.Name,
                user.Email,
                user.UserName,
            };
        }
    }
}