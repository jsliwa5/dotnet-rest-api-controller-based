using Microsoft.AspNetCore.Identity;

namespace dotnet_rest_api_controller_based.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Event> Events { get; set; } = new List<Event>();
    }
}
