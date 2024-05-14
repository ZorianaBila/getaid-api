using GetAidBackend.Auth.Requests;
using GetAidBackend.Auth.Responses;
using GetAidBackend.Domain;

namespace GetAidBackend.Auth.Services.Abstractions
{
    public interface IAccountService
    {
        Task ChangePassword(ChangePasswordRequest changePasswordRequest);

        Task<LoginResponse> Login(LoginRequest loginRequest);

        Task<LoginResponse> Login(string userId);

        Task<LoginResponse> Login(User user);

        Task<LoginResponse> RefreshToken(RefreshTokenRequest refreshTokenRequest);

        Task<User> Registration(RegisterRequest registerRequest);
    }
}