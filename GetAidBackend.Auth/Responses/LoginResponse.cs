using GetAidBackend.Services.Dtos;

namespace GetAidBackend.Auth.Responses
{
    public class LoginResponse
    {
        public UserDto User { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime AccessTokenExpiresAt { get; set; }
        public DateTime RefreshTokenExpiresAt { get; set; }
    }
}