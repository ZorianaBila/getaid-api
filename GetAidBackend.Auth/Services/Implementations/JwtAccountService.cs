using AutoMapper;
using GetAidBackend.Auth.Entities;
using GetAidBackend.Auth.Exceptions;
using GetAidBackend.Auth.Requests;
using GetAidBackend.Auth.Responses;
using GetAidBackend.Auth.Services.Abstractions;
using GetAidBackend.Auth.Storage.Abstractions;
using GetAidBackend.Domain;
using GetAidBackend.Services.Dtos;
using GetAidBackend.Services.Exceptions;
using GetAidBackend.Storage.Abstractions;

namespace GetAidBackend.Auth.Services.Implementations
{
    public class JwtAccountService : IAccountService
    {
        private readonly JwtTokenInfo _jwtInfo;
        private readonly ITokenService _tokenService;
        private readonly IRefreshTokenRepository _rtRepo;
        private readonly IUserRepository _userRepo;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMapper _mapper;

        public JwtAccountService(
            JwtTokenInfo jwtInfo,
            ITokenService tokenService,
            IRefreshTokenRepository rtRepo,
            IUserRepository userRepo,
            IPasswordHasher passwordHasher,
            IMapper mapper)
        {
            _jwtInfo = jwtInfo;
            _tokenService = tokenService;
            _rtRepo = rtRepo;
            _userRepo = userRepo;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
        }

        public async Task<User> Registration(RegisterRequest registerRequest)
        {
            var dbUser = await _userRepo.GetByEmail(registerRequest.Email);

            if (dbUser != null)
                throw new EmailAlreadyTakenException();

            var passwordHash = _passwordHasher.Hash(registerRequest.Password);

            var user = new User
            {
                PrivateData = registerRequest.PrivateData,
                Email = registerRequest.Email,
                PasswordHash = passwordHash,
                UserRole = UserRole.Consumer
            };

            await _userRepo.Add(user);

            return user;
        }

        public async Task<LoginResponse> Login(LoginRequest loginRequest)
        {
            var user = await _userRepo.GetByEmail(loginRequest.Email);

            if (user == null)
                throw new NotFoundException(nameof(User));

            if (!_passwordHasher.Verify(loginRequest.Password, user.PasswordHash))
                throw new PasswordMismatchException();

            return await Login(user);
        }

        public async Task<LoginResponse> Login(string userId)
        {
            var user = await _userRepo.GetById(userId);

            if (user == null)
                throw new NotFoundException(nameof(User));

            return await Login(user);
        }

        public async Task<LoginResponse> Login(User user)
        {
            var accessToken = _tokenService.GetAccessToken(user);
            var refreshToken = _tokenService.GetRefreshToken();
            DateTime accessTokenExpiration =
                DateTime.UtcNow.AddMinutes(_jwtInfo.AccessTokenExpiresInMinutes);
            DateTime refreshTokenExpiration =
                DateTime.UtcNow.AddMinutes(_jwtInfo.RefreshTokenExpiresInMinutes);

            await _rtRepo.Create(user.Id, refreshToken, refreshTokenExpiration);

            return new LoginResponse
            {
                User = _mapper.Map<User, UserDto>(user),
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                AccessTokenExpiresAt = accessTokenExpiration,
                RefreshTokenExpiresAt = refreshTokenExpiration
            };
        }

        public async Task<LoginResponse> RefreshToken(RefreshTokenRequest refreshTokenRequest)
        {
            string refreshToken = refreshTokenRequest.RefreshToken;

            var rt = await _rtRepo.GetByRefreshToken(refreshToken);

            if (rt == null)
                throw new BadInputException("Invalid refresh token.");

            var loginResponse = await Login(rt.UserId);
            await _rtRepo.DeleteByToken(refreshToken);

            return loginResponse;
        }

        public async Task ChangePassword(
            ChangePasswordRequest changePasswordRequest)
        {
            var dbUser = await _userRepo
                .GetByEmail(changePasswordRequest.UserEmail);

            if (dbUser == null)
                throw new NotFoundException(nameof(User));

            if (!_passwordHasher.Verify(
                    changePasswordRequest.OldPassword,
                    dbUser.PasswordHash))
                throw new PasswordMismatchException();

            var hashedNewPassword = _passwordHasher
                .Hash(changePasswordRequest.NewPassword);

            dbUser.PasswordHash = hashedNewPassword;

            await _userRepo.Update(dbUser);
        }
    }
}