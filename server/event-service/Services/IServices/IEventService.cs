using event_service.Entities;

namespace event_service.Services.IServices
{
    public interface IEventService
    {
        ICollection<Event> GetEvents();
        Event GetEvent(int id);
    }
}
