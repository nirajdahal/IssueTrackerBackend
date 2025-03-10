﻿using AutoMapper;
using Library.Contracts;
using Library.Entities.DTO;
using Library.Entities.DTO.UserDto;
using Library.Entities.Enums;
using Library.Entities.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IssueTracker.Controllers
{
  
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private ApplicationSettings _appSettings;
        private readonly IMapper _mapper;
        private ILoggerManager _logger;

        public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IMapper mapper, IOptions<ApplicationSettings> appSettings, ILoggerManager logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _appSettings = appSettings.Value;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser(LoginModel model)
        {
            if (model == null)
            {
                _logger.LogError("Login Model sent from client is null.");
                return BadRequest("Empty User Cannot Be Logged In");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the Login");
                return UnprocessableEntity(ModelState);
            }
            var user = await _userManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                return BadRequest("Empty User Cannot Be Logged In");
            }

            var userRole = await _userManager.GetRolesAsync(user);
            var userRoles = new List<string> { "Admin", "Submitter", "Project Manager", "Developer" };

            foreach (var role in userRole)
            {
                var conditionForAuthority = userRoles.Contains(role.ToString());
                if (!conditionForAuthority)
                {
                    return Unauthorized("Unauthorized");
                }
            }
             if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                        new Claim("UserID", user.Id.ToString()),
                        new Claim("UserName", user.Name.ToString()),
                        new Claim("UserEmail", user.Email.ToString()),
                        new Claim(ClaimTypes.Role, userRole.FirstOrDefault())
                        }),
                        Expires = DateTime.UtcNow.AddDays(1),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("`!`)$^`%&`*@issu31r@ck3r!|¬9+*a-T-s|¬`(")), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                    var token = tokenHandler.WriteToken(securityToken);
                    return Ok(new { token });
                }
                else
                    return BadRequest(new { message = "Username or password is incorrect." });
           
         
            
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(RegisterModelDto model)
        {
            if (model == null)
            {
                _logger.LogError("Register Model sent from client is null.");
                return BadRequest("User Cannot Be Created as it is empty");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the Registration");
                return UnprocessableEntity(ModelState);
            }
            var result = await RegisterUserHelper(model);
            return Ok(result);
        }

        [HttpGet("developers")]
        public async Task<IActionResult> GetAllDevelopers()
        {
            var users = await _userManager.GetUsersInRoleAsync("Developer");
            var developersToReturn = _mapper.Map<IEnumerable<UserVm>>(users);
            return Ok(developersToReturn);
        }

        [HttpGet("pmanagers")]
        public async Task<IActionResult> GetAllPManagers()
        {
            var users = await _userManager.GetUsersInRoleAsync("Project Manager");
            var managerToReturn = _mapper.Map<IEnumerable<UserVm>>(users);
            return Ok(managerToReturn);
        }

        public async Task<object> RegisterUserHelper(RegisterModelDto model)
        {
            try
            {
                var user = new ApplicationUser
                {
                    Name = model.Name,
                    Email = model.Email,
                    UserName = model.Email
                };
                var userWithSameEmail = await _userManager.FindByEmailAsync(model.Email);
                if (userWithSameEmail == null)
                {
                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, UserRoles.default_role.ToString());

                        var userToReturn = _mapper.Map<RegisterModel>(model);
                        var output = ($"Sucessfully registered the user {user.Name}");
                        return output;
                    }

                    //this throws error
                    else
                    {
                        return result;
                    }
                }
                else
                {
                    return $"Sorry ! Email {user.Email } is already registered.";
                }
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
    }
}