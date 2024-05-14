using GetAidBackend.Auth.Entities;
using GetAidBackend.Storage.Abstractions;

namespace GetAidBackend.Auth.Storage.Abstractions
{
    public interface IRefreshTokenRepository : IRepository<RefreshTokenInfo>
    {
        Task Create(string userId, string refreshToken, DateTime expirationDate);

        Task<RefreshTokenInfo> DeleteByToken(string refreshToken);

        Task<RefreshTokenInfo> GetByRefreshToken(string refreshToken);
    }
}