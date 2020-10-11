using AutoMapper;
using Library.Entities.DTO;
using Library.Entities.Enums;
using Library.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
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

        public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IMapper mapper, IOptions<ApplicationSettings> appSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }


        [HttpPost("login")]

        public async Task<IActionResult> LoginUser(LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                //get current role for the user
                var userRole = await _userManager.GetRolesAsync(user);
                IdentityOptions _options = new IdentityOptions();
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("UserID", user.Id.ToString()),
                        new Claim(ClaimTypes.Role, userRole.FirstOrDefault())
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
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
