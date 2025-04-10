using dotnet_rest_api_controller_based.Dtos;
using dotnet_rest_api_controller_based.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rest_api_controller_based.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthController(
            UserManager<ApplicationUser> userMenager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userMenager;
            _signInManager = signInManager;
        }

        [HttpPost("/register")]
        public async Task<IActionResult> Register (RegisterDto dto)
        {
            var user = new ApplicationUser {
                UserName = dto.Email,
                Email = dto.Email
            };

            var result = await _userManager.CreateAsync (user, dto.Password);
            if (!result.Succeeded) 
            {
                return BadRequest();
            }

            await _signInManager.SignInAsync (user, isPersistent: false);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var result = await _signInManager.PasswordSignInAsync(
                    dto.Email,
                    dto.Password,
                    isPersistent: false,
                    lockoutOnFailure: false
                );

            if (!result.Succeeded)
            {
                return Unauthorized();
            }

            return Ok();
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }

    }
}
