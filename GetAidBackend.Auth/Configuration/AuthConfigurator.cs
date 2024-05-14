using GetAidBackend.Auth.Services.Abstractions;
using GetAidBackend.Auth.Services.Implementations;
using GetAidBackend.Auth.Storage.Abstractions;
using GetAidBackend.Auth.Storage.Implementations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace GetAidBackend.Auth.Configuration
{
    public static class AuthConfigurator
    {
        public static IServiceCollection AddAuthConfiguration(this IServiceCollection services)
        {
            services.AddJwtTokenConfig(out var jwtToken);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = true;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtToken.Issuer,

                    ValidateAudience = true,
                    ValidAudience = jwtToken.Audience,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey
                    (
                        Encoding.UTF8.GetBytes(jwtToken.Secret)
                    ),
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddTransient<IPasswordHasher, PasswordHasher>();
            services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddTransient<ITokenService, JwtTokenService>();
            services.AddTransient<IAccountService, JwtAccountService>();

            return services;
        }
    }
}