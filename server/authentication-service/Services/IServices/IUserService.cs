using authentication_service.DTOs;

namespace authentication_service.Services.IServices
{
    public interface IUserService
    {
        public Task<bool> GetOrganizerByUsername(string username);
        public Task<bool> GetClientByUsername(string username);
    }
}
