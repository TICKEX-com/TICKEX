using authentication_service.DTOs;

namespace authentication_service.Services.IServices
{
    public interface IUserService
    {
        public Task<UserDto> GetOrganizerByUsername(string username);
        public Task<UserDto> GetClientByUsername(string username);
    }
}
