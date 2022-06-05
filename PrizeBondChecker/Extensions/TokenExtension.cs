using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace PrizeBondChecker.Extensions
{
    public static class TokenExtension
    {
        public static IServiceCollection TokenAuthentication(this IServiceCollection services, IConfiguration configuration)
        {

            var key = Encoding.ASCII.GetBytes(configuration.GetSection("Tokens:Key").Value);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                   .AddJwtBearer(option =>
                   {
                       option.TokenValidationParameters = new TokenValidationParameters()
                       {
                           ValidateAudience = true,
                           ValidAudience = configuration.GetSection("Tokens")["Audience"],
                           ValidateIssuer = true,
                           ValidIssuer = configuration.GetSection("Tokens")["Issuer"],
                           RequireExpirationTime = true,
                           ValidateIssuerSigningKey = true,
                           RequireSignedTokens = true,
                           IssuerSigningKey = new SymmetricSecurityKey(key),
                           ValidateLifetime = true
                       };
                   });

            return services;
        }
    }
}
