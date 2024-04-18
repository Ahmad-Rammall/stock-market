using Azure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stock_Market_API.DTOs.User;
using Stock_Market_API.Interfaces;
using Stock_Market_API.Models;
using Stock_Market_API.Services;

namespace Stock_Market_API.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<User> _signInManager;
        public UserController(UserManager<User> userManager, ITokenService token, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _tokenService = token;
            _signInManager = signInManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDTO.UserName);

            if (user == null) return Unauthorized("Invalid User");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDTO.Password, false);

            if (!result.Succeeded) return Unauthorized("Invalid User");

            return Ok("Login Successful : " + _tokenService.CreateToken(user));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            try 
            { 
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var user = new User
                {
                    UserName = registerDTO.UserName,
                    Email = registerDTO.Email,
                };

                var createdUser = await _userManager.CreateAsync(user, registerDTO.Password);

                if (createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(user, "User");
                    if(roleResult.Succeeded)
                    {
                        var token = _tokenService.CreateToken(user);
                        return Ok("User Created : " + token);
                    } else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                } else
                {
                    return StatusCode(500, createdUser.Errors);
                }
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
