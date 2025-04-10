using dotnet_rest_api_controller_based.Models;

namespace dotnet_rest_api_controller_based.Repositories
{
    public interface IOrganizerRepository
    {
        Task<IEnumerable<Organizer>> GetOrganizersAsync();
        Task<Organizer?> GetOrganizerById(int id);
        Task AddAsync(Organizer organizer);
        Task DeleteAsync(int id);
        Task<bool> SaveChangesAsync();
    }
}
