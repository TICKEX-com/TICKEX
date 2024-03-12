using event_service.Data;
using event_service.DTOs;
using event_service.Entities;
using event_service.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace event_service.Services
{
    public class EventService : IEventService
    {
        private readonly DataContext _context;

        public EventService(DataContext context)
        {
            _context = context;
        }

        public async Task<Event> GetEvent(int id)
        {
            return await _context.Events.FirstAsync(ev => ev.Id == id);
        }

        public async Task<ICollection<Event>> GetEvents()
        {
            return await _context.Events.OrderBy(ev => ev.Id).ToListAsync();
        }

        public async Task<bool> CreateEvent(EventReqDto Event)
        {
            Event ev = new()
            {
                Title = Event.Title,
                Description = Event.Description,
                Date = Event.Date,
                Location = Event.Location,
                MinPrize = Event.MinPrize,
                CategoryId = Event.CategoryId,
                OrganizerUsername = Event.OrganizerUsername,
            };
            
            _context.Events.Add(ev);
            var result = await _context.SaveChangesAsync();
            return result > 0;
            
        }
    }
}
