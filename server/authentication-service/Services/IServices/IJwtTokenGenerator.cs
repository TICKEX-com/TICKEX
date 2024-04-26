

using authentication_service.Entities;

namespace authentication_service.Services.IServices
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user, IEnumerable<string> roles);
    }
}
