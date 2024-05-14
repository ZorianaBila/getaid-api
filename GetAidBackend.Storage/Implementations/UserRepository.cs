using GetAidBackend.Domain;
using GetAidBackend.Storage.Abstractions;
using MongoDB.Driver;

namespace GetAidBackend.Storage.Implementations
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        private static string _collectionName = "users";

        public UserRepository(IMongoClient client) : base(client, _collectionName)
        {
        }

        public async Task<User> GetByEmail(string email)
        {
            var norlmalizedEmail = email.ToLower();

            return await _collection.Find(p => p.Email == norlmalizedEmail)
                .Limit(1)
                .FirstOrDefaultAsync();
        }

        public async Task<User> UpdatePrivateData(string userId, UserPrivateData userPrivateData)
        {
            return await _collection.FindOneAndUpdateAsync<User>(
                _ => _.Id == userId,
                Builders<User>.Update.Set(_ => _.PrivateData, userPrivateData),
                new FindOneAndUpdateOptions<User>
                {
                    ReturnDocument = ReturnDocument.After
                });
        }
    }
}