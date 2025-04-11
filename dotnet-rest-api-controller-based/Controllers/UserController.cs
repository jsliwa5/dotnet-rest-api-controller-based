using dotnet_rest_api_controller_based.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rest_api_controller_based.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UserController : ControllerBase
    {

        private readonly UserManager<ApplicationUser> _userManager;

        UserController(UserManager<ApplicationUser> userManager) {
            _userManager = userManager;
        }

        [HttpPost("{userId}")]
        public async Task<IActionResult> AddRoleToUser(string userId,[FromBody] string role)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if(user == null)
            {
                return NotFound("User with given Id does not exist");
            }

            var result = await _userManager.AddToRoleAsync(user, role);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok();

        }

        [HttpDelete("userId")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var userToBeDeleted = await _userManager.FindByIdAsync(userId);

            if (userToBeDeleted == null)
            {
                return NotFound();
            }

            var result = await _userManager.DeleteAsync(userToBeDeleted);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }



    }
}
