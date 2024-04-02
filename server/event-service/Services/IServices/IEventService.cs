using event_service.DTOs;
using event_service.Entities;

namespace event_service.Services.IServices
{
    public interface IEventService
    {
        Task<ICollection<Event>> GetEvents();
        Task<Event> GetEvent(int id);
        Task<bool> CreateEvent(EventReqDto Event);
        Task<bool> IsEventExist(int id);

    }
}
