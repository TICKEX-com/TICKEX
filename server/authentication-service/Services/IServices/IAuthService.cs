

using authentication_service.DTOs;

namespace authentication_service.Services.IServices
{
    public interface IAuthService
    {
        Task<string> RegisterClient(RegisterReqClientDto requestDto);
        Task<string> RegisterOrganizer(RegisterReqOrganizerDto requestDto);
        Task<LoginResponseDto> Login(LoginRequestDto requestDto);
        Task<bool> AssignRole(string email, string roleName);
    }
}
