using dotnet_rest_api_controller_based.Models;

namespace dotnet_rest_api_controller_based.Repositories
{
    public interface IEventRepository
    {
        Task<IEnumerable<Event>> GetEventsAsync();
        Task<Event?> GetEventByIdAsync(int id);
        Task AddAsync(Event ev);
        Task RemoveAsync(int id);
        Task<bool> SaveChangesAsync();
    }
}
