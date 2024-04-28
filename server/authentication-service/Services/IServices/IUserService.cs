using authentication_service.DTOs;
using authentication_service.Entities;

namespace authentication_service.Services.IServices
{
    public interface IUserService
    {
        public Task<UserDto> GetOrganizerByUsername(string username);
        public Task<UserDto> GetClientByUsername(string username);
        public Task<OrganizerDto> GetOrganizerById(string id);
        public Task<User> GetOrganizerById2(string id);
        public Task<ICollection<OrganizerDto>> GetOrganizers();
        public Task<string> GetOrganizerIdByUsername(string username);
        public Task<bool> IsOrganizerExist(string id);
        public Task<string> UpdateOrganizer(UpdateReqOrganizerDto requestDto, string id);
        public Task<bool> AcceptOrganizer(string id);
        public Task<bool> DeleteOrganizer(string id);
    }
}
