using EventManagementApi.Models;
using EventManagementApi.Data;
using Microsoft.EntityFrameworkCore;

namespace EventManagementApi.Services
{
    public class EventService : IEventService
    {
        private readonly ApplicationDbContext _context;

        public EventService(ApplicationDbContext context)
        {
            _context = context;// _context is the bridge to our database. It's is injected using dependency injection
        }

        public async Task<IEnumerable<Event>> GetAllAsync() =>
            await _context.Events.ToListAsync();

        public async Task<Event?> GetByIdAsync(int id) =>
            await _context.Events.FindAsync(id);

        public async Task<Event> CreateAsync(Event ev)
        {
            _context.Events.Add(ev); // Add event to memory
            await _context.SaveChangesAsync(); // Add event to memory
            return ev;
        }

        public async Task<bool> UpdateAsync(int id, Event ev)
        {
            var existing = await _context.Events.FindAsync(id);
            if (existing == null) return false;

            existing.Title = ev.Title;
            existing.Description = ev.Description;
            existing.Location = ev.Location;
            existing.StartDateTime = ev.StartDateTime;
            existing.EndDateTime = ev.EndDateTime;
            existing.Organizer = ev.Organizer;
            existing.IsPublic = ev.IsPublic;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var ev = await _context.Events.FindAsync(id);
            if (ev == null) return false;

            _context.Events.Remove(ev);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
