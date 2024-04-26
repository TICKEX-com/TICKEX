

using authentication_service.DTOs;

namespace authentication_service.Services.IServices
{
    public interface IAuthService
    {
        Task<string> RegisterClient(RegisterReqClientDto requestDto);
        Task<string> RegisterOrganizer(RegisterReqOrganizerDto requestDto);
        Task<LoginResponseDto> Login(HttpContext httpContext, LoginRequestDto requestDto);
        Task<bool> Logout(HttpContext httpContext);
        Task<bool> AssignRole(string email, string roleName);
    }
}
