using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace event_service.Extensions
{
    public static class Auth
    {
        public static WebApplicationBuilder AddAppAuthentication(this WebApplicationBuilder builder)
        {
            var settingsSection = builder.Configuration.GetSection("ApiSettings");
            var jwtOptions = settingsSection.GetSection("JwtOptions");

            var secret = jwtOptions.GetValue<string>("Secret");
            var issuer = jwtOptions.GetValue<string>("Issuer");
            var audience = jwtOptions.GetValue<string>("Audience");


            /*var secret = Environment.GetEnvironmentVariable("SECRET");
            var issuer = Environment.GetEnvironmentVariable("ISSUER");
            var audience = Environment.GetEnvironmentVariable("AUDIENCE");*/

            var key = Encoding.ASCII.GetBytes(secret);

            builder.Services.AddSingleton<IConfiguration>(builder.Configuration); // Inject IConfiguration

            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    ValidateAudience = true
                };
            });

            return builder;
        }
    }
}