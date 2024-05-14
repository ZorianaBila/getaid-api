using GetAidBackend.Auth.Entities;
using GetAidBackend.Auth.Storage.Abstractions;
using GetAidBackend.Storage.Implementations;
using MongoDB.Driver;

namespace GetAidBackend.Auth.Storage.Implementations
{
    public class RefreshTokenRepository : RepositoryBase<RefreshTokenInfo>, IRefreshTokenRepository
    {
        private static readonly string _collectionName = "refreshTokens";

        public RefreshTokenRepository(IMongoClient client) : base(client, _collectionName)
        {
        }

        public async Task<RefreshTokenInfo> GetByRefreshToken(string refreshToken)
        {
            var rt = await _collection
                .Find(rt => rt.Token == refreshToken)
                .Limit(1)
                .FirstOrDefaultAsync();

            if (rt?.ExpireAt >= DateTime.UtcNow)
                return rt;

            return null;
        }

        public async Task Create(string userId, string refreshToken, DateTime expirationDate)
        {
            expirationDate = expirationDate.ToUniversalTime();
            var rt = new RefreshTokenInfo()
            {
                UserId = userId,
                Token = refreshToken,
                ExpireAt = expirationDate
            };

            await Add(rt);
        }

        public async Task<RefreshTokenInfo> DeleteByToken(string refreshToken)
        {
            return await _collection.FindOneAndDeleteAsync(rt => rt.Token == refreshToken);
        }
    }
}