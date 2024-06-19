using event_service.DTOs;
using event_service.Entities;

namespace event_service.Services.IServices
{
    public interface IEventService
    {
        Task<ICollection<EventsDto>> GetEvents(int pageNumber);
        Task<ICollection<EventsDto>> FilterEvents(string Date, string City, string EventType, float MinPrice, float MaxPrice, int pageNumber);
        Task<ICollection<EventsDto>> GetEventsByDate(string Date);
        Task<ICollection<EventsDto>> GetEventsByTitle(string title);
        Task<ICollection<Category>> GetCategoriesByEventId(int id);
        Task<Event> GetEventById(int id);
        Task<ICollection<EventsDto>> GetEventsByOrganizer(string id, int pageNumber);
        Task<Event> GetEventById(string OrganizerId, int id);
        Task<ICollection<EventsDto>> GetEventsByType(string type);
        Task<int?> CreateEvent(EventReqDto Event, string OrganizerId);
        Task<bool> DeleteEvent(string OrganizerId, int id);
        Task<int?> UpdateEvent(EventReqDto Event, string OrganizerId, int id);
        Task<bool> IsEventExist(int id);
        Task<bool> IsTypeExist(string type);
        Task<bool> OrganizerHasEvents(string id);
    }
}
