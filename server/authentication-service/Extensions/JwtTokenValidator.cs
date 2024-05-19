using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace authentication_service.Extensions
{
    public class JwtTokenValidator
    {
        private readonly string _issuer;
        private readonly string _audience;
        private readonly SymmetricSecurityKey _securityKey;

        public JwtTokenValidator(string issuer, string audience, string secret)
        {
            _issuer = issuer;
            _audience = audience;
            _securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        }

        public string[] ValidateToken(string jwtToken)
        {
            try
            {
                // Token validation parameters
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = _securityKey,
                    ValidIssuer = _issuer,
                    ValidAudience = _audience
                };

                // Create token handler
                var tokenHandler = new JwtSecurityTokenHandler();

                // Validate the JWT token
                var principal = tokenHandler.ValidateToken(jwtToken, tokenValidationParameters, out _);

                // Extract user roles from claims
                var roles = principal.Claims.Where(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")
                            .Select(c => c.Value)
                            .ToArray();

                return roles;
            }
            catch (Exception ex)
            {
                // Token validation failed
                // You can log the error or handle it as needed
                throw new Exception("Token validation failed: " + ex.Message);
            }
        }
    }
}
