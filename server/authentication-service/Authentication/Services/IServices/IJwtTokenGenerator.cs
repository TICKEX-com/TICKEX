using Authentication.Entities;

namespace Authentication.Services.IServices
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user, IEnumerable<string> roles);
    }
}
