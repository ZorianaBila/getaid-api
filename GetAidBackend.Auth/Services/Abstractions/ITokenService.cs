using GetAidBackend.Domain;

namespace GetAidBackend.Auth.Services.Abstractions
{
    public interface ITokenService
    {
        string GetAccessToken(User user);

        string GetRefreshToken();

        bool ValidateToken(string token);
    }
}