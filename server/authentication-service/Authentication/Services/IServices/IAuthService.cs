using Authentication.DTOs;

namespace Authentication.Services.IServices
{
    public interface IAuthService
    {
        Task<string> Register(RegisterationRequestDto requestDto);
        Task<LoginResponseDto> Login(LoginRequestDto requestDto);
        Task<bool> AssignRole(string email, string roleName);
    }
}
