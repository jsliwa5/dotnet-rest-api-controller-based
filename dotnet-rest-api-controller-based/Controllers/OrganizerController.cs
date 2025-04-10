using dotnet_rest_api_controller_based.Models;
using dotnet_rest_api_controller_based.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rest_api_controller_based.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizerController : ControllerBase
    {

        private readonly IOrganizerRepository _organizerRepo;

        public OrganizerController(IOrganizerRepository organizerRepo)
        {
            _organizerRepo = organizerRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Organizer>>> GetOrganizers()
        { 
            var organizers = await _organizerRepo.GetOrganizersAsync();
            return Ok(organizers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Organizer>> GetOrganizer(int id)
        {
            var organizer = await _organizerRepo.GetOrganizerById(id);

            if (organizer == null)
            {
                return NotFound();
            }

            return Ok(organizer);
        }

        [HttpPost]
        public async Task<ActionResult<Organizer>> CreateOrganizer(Organizer organizer)
        {
            if (organizer == null)
            {
                return BadRequest();
            }

            await _organizerRepo.AddAsync(organizer);
            var isSaved = await _organizerRepo.SaveChangesAsync();

            if (isSaved) 
            {
                return CreatedAtAction(nameof(GetOrganizer), new { id = organizer.Id }, organizer);
            }

            return StatusCode(500, "Problem occured while saving the organizer.");

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrganizer(int id)
        {
            var organizer = _organizerRepo.DeleteAsync(id);

            var isDeleted = await _organizerRepo.SaveChangesAsync(); 

            if (isDeleted)
            {
                return NoContent();
            }

            return StatusCode(500, "Problem occurred while deleting the organizer.");
        }
    }
}
