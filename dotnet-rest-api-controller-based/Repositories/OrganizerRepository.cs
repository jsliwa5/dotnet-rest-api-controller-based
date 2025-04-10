using dotnet_rest_api_controller_based.DataDb;
using dotnet_rest_api_controller_based.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rest_api_controller_based.Repositories
{
    public class OrganizerRepository : IOrganizerRepository
    {

        private readonly AppDbContext _context;

        public async Task AddAsync(Organizer organizer)
        {
            await _context.Organizers.AddAsync(organizer); 
        }

        public async Task DeleteAsync(int id)
        {
            var OrganizerToBeRemoved = await _context.Organizers.FindAsync(id);
            if (OrganizerToBeRemoved != null)
            {
                _context.Remove(OrganizerToBeRemoved);
            }
        }

        //lazy loading
        public async Task<Organizer?> GetOrganizerById(int id)
        {
            return await _context.Organizers.FindAsync(id);
        }

        //lazy loading
        public async Task<IEnumerable<Organizer>> GetOrganizersAsync()
        {
            return await _context.Organizers.ToListAsync();
        }

        public Task<bool> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
