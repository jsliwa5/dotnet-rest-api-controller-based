using dotnet_rest_api_controller_based.DataDb;
using dotnet_rest_api_controller_based.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rest_api_controller_based.Repositories
{
    public class EventRepository : IEventRepository
    {

        private readonly AppDbContext _context;

        public EventRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Event ev)
        {
            await _context.Events.AddAsync(ev);
        }

        public async Task<Event?> GetEventByIdAsync(int id)
        {
            return await _context.Events.Include(e => e.Organizer).FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Event>> GetEventsAsync()
        {
            return await _context.Events.Include(e => e.Organizer).ToListAsync();
        }

        public async Task RemoveAsync(int id)
        {
            var EventToRemove = await _context.Events.FindAsync(id);

            if (EventToRemove != null)
            {
                 _context.Events.Remove(EventToRemove);
            }

        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}