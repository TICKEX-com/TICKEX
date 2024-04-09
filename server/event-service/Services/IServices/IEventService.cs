using event_service.DTOs;
using event_service.Entities;

namespace event_service.Services.IServices
{
    public interface IEventService
    {
        Task<ICollection<Event>> GetEvents();
        Task<ICollection<Event>> GetEventsByDate(string Date);
        Task<ICollection<Event>> GetEventsByTitle(string title);
        Task<Event> GetEventById(int id);
        Task<ICollection<Event>> GetEventsByOrganizer(string id);
        Task<Event> GetEventById(string OrganizerId, int id);
        Task<ICollection<Event>> GetEventsByCategory(int id);
        Task<bool> CreateEvent(EventReqDto Event, string OrganizerId);
        Task<bool> DeleteEvent(string OrganizerId, int id);
        Task<bool> UpdateEvent(EventReqDto Event, string OrganizerId, int id);
        Task<bool> IsEventExist(int id);
        Task<bool> IsCategoryExist(int id);
        Task<bool> OrganizerHasEvents(string id);
    }
}
