using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using dotnet_rest_api_controller_based.Models;
using dotnet_rest_api_controller_based.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rest_api_controller_based.Controllers
{
    [Route("api/events")]
    [ApiController]
    public class EventController : ControllerBase
    {

        private readonly IEventRepository _eventRepo;
        private readonly UserManager<ApplicationUser> _userManager;

        public EventController(IEventRepository eventRepo,
            UserManager<ApplicationUser> userManager)
        {
            _eventRepo = eventRepo;
            _userManager = userManager; 
        }

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Event>>> GetEvents()
        //{
        //    var events = await _eventRepo.GetEventsAsync();
        //    return Ok(events);
        //}

        //[HttpGet("{id}")]
        //public async Task<ActionResult<Event>> GetEvent(int id)
        //{
        //    var ev = await _eventRepo.GetEventByIdAsync(id);

        //    if (ev == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(ev);
        //}

        //[HttpPost]
        //public async Task<ActionResult<Event>> CreateEvent(Event ev)
        //{
        //    if (ev == null)
        //    {
        //        return BadRequest();
        //    }

        //    await _eventRepo.AddAsync(ev);
        //    var isSaved = await _eventRepo.SaveChangesAsync();

        //    if (isSaved)
        //    {
        //        return CreatedAtAction(nameof(GetEvent), new { id = ev.Id }, ev);
        //    }

        //    return StatusCode(500, "Problem occurred while saving the event.");
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteEvent(int id)
        //{
        //    await _eventRepo.RemoveAsync(id);

        //    var isDeleted = await _eventRepo.SaveChangesAsync();

        //    if (isDeleted) 
        //    {
        //        return NoContent();
        //    }

        //    return StatusCode(500, "Problem occurred while deleting the event.");

        //}

        [Authorize]
        [HttpPost("{id}/join")]
        public async Task<IActionResult> JoinEvent(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            var ev = await _eventRepo.GetEventByIdAsync(id);

            if (ev == null || user == null) 
            {
                return NotFound();
            }

            ev.Attendees.Add(user);
            await _eventRepo.SaveChangesAsync();

            return Ok();
        }
    }
}
