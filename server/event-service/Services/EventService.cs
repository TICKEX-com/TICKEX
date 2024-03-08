using event_service.Data;
using event_service.Entities;
using event_service.Services.IServices;

namespace event_service.Services
{
    public class EventService : IEventService
    {
        private readonly DataContext _context;

        public EventService(DataContext context)
        {
            _context = context;
        }

        public Event GetEvent(int id)
        {
            return _context.Events.First(ev => ev.Id == id);
        }

        public ICollection<Event> GetEvents()
        {
            return _context.Events.OrderBy(ev => ev.Id).ToList();
        }
    }
}
