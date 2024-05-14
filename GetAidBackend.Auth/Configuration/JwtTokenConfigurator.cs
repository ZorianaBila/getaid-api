using GetAidBackend.Auth.Entities;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace GetAidBackend.Auth.Configuration
{
    public static class JwtTokenConfigurator
    {
        public static IServiceCollection AddJwtTokenConfig(this IServiceCollection services, out JwtTokenInfo jwtTokenInfo)
        {
            var secretBase64 = Environment.GetEnvironmentVariable("JWT_SECRET");
            var secretBytes = Convert.FromBase64String(secretBase64);
            var secret = Encoding.UTF8.GetString(secretBytes);

            jwtTokenInfo = new JwtTokenInfo
            {
                Secret = secret,
                Issuer = GetEnvVar("JWT_ISSUER"),
                Audience = GetEnvVar("JWT_AUDIENCE"),
                AccessTokenExpiresInMinutes = int.Parse(GetEnvVar("JWT_AT_EXPIRES_IN_MIN")),
                RefreshTokenExpiresInMinutes = int.Parse(GetEnvVar("JWT_RT_EXPIRES_IN_MIN"))
            };

            services.AddSingleton(jwtTokenInfo);

            return services;
        }

        private static string GetEnvVar(string key)
        {
            return Environment.GetEnvironmentVariable(key);
        }
    }
}