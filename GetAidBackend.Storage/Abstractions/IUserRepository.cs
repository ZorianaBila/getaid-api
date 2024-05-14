using GetAidBackend.Domain;

namespace GetAidBackend.Storage.Abstractions
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByEmail(string email);

        Task<User> UpdatePrivateData(string userId, UserPrivateData userPrivateData);
    }
}