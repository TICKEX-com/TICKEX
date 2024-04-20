using event_service.Data;
using event_service.Entities;

namespace event_service.Services.IServices
{
    public interface IUserService
    {
        public Task<ICollection<Organizer>> GetOrganizers();
        public Task<bool> CreateOrganizer(Organizer organizer);
        public Task<bool> UpdateOrganizer(Organizer organizer);
        public Task<bool> IsOrganizerExists(string id);
        public Task<Organizer> GetOrganizerById(string id);
    }
}
