using EventManagementApi.Models;

namespace EventManagementApi.Services
{
    public interface IEventService
    {
        Task<IEnumerable<Event>> GetAllAsync();
        Task<Event?> GetByIdAsync(int id);
        Task<Event> CreateAsync(Event ev);
        Task<bool> UpdateAsync(int id, Event ev);   
        Task<bool> DeleteAsync(int id);
    }
}
