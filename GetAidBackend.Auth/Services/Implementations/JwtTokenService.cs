using GetAidBackend.Auth.Entities;
using GetAidBackend.Auth.Services.Abstractions;
using GetAidBackend.Domain;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GetAidBackend.Auth.Services.Implementations
{
    public class JwtTokenService : ITokenService
    {
        private readonly JwtTokenInfo _jwtInfo;

        public JwtTokenService(JwtTokenInfo jwtInfo)
        {
            _jwtInfo = jwtInfo;
        }

        public string GetAccessToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(_jwtInfo.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim("UserId", user.Id),
                        new Claim(ClaimTypes.Name, user.PrivateData.Name),
                        new Claim(ClaimTypes.Surname, user.PrivateData.SurName),
                        new Claim(ClaimTypes.MobilePhone, user.PrivateData.PhoneNumber),
                        new Claim("UserAge", user.PrivateData.Age.ToString()),
                        new Claim(ClaimTypes.Email, user.Email),
                    }),
                Expires = DateTime.UtcNow.AddMinutes(_jwtInfo.AccessTokenExpiresInMinutes),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature),
                Audience = _jwtInfo.Audience,
                Issuer = _jwtInfo.Issuer,
                NotBefore = DateTime.UtcNow
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string GetRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }

        public bool ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtInfo.Secret);

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                return validatedToken != null;
            }
            catch
            {
                return false;
            }
        }
    }
}