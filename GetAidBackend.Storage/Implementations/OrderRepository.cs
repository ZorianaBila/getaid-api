using GetAidBackend.Domain;
using GetAidBackend.Storage.Abstractions;
using MongoDB.Driver;

namespace GetAidBackend.Storage.Implementations
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        private static string _collectionName = "orders";

        public OrderRepository(IMongoClient client) : base(client, _collectionName)
        {
        }

        public async Task<List<Order>> GetUserOrders(string userId)
        {
            return await _collection.Find(_ => _.UserId == userId).ToListAsync();
        }

        public async Task DeleteUserOrders(string userId)
        {
            await _collection.DeleteManyAsync(_ => _.UserId == userId);
        }
    }
}