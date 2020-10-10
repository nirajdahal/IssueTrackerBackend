using AutoMapper;
using Library.Entities.DTO;
using Library.Entities.Enums;
using Library.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;

namespace IssueTracker.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;

        public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }


        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(RegisterModel model)
        {
            var result = await RegisterUserHelper(model);
            /* Use CreatedAtRoute method to return the user and use the help of
               GetUserMethod to return the user.
               return CreatedAtRoute("UserToReturn", new { id = useroReturn.Id
               }, useroReturn);
            */
            return Ok(result);

        }

        public async Task<object> RegisterUserHelper(RegisterModel model)
        {
            try
            {
                var user = new ApplicationUser
                {
                    Name= model.Name,
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
                        
                        var userToReturn = _mapper.Map<RegisterModelDto>(model);
                        var output =  ($"Sucessfully registered the user {user.Name}");
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
            catch(Exception ex)
            {
                return ex;
            }
            
        }
    }


}
