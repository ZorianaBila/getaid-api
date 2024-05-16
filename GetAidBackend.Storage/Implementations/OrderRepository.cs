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
            return await _collection.Find(_ => _.UserId == userId)
                .SortByDescending(_ => _.DateTime)
                .ToListAsync();
        }

        public async Task DeleteUserOrders(string userId)
        {
            await _collection.DeleteManyAsync(_ => _.UserId == userId);
        }

        public async Task<List<Order>> GetByIds(string[] ids)
        {
            return await _collection.Find(Builders<Order>.Filter.In(_ => _.Id, ids)).ToListAsync();
        }

        public async Task DeliverOrders(string[] ids, IClientSessionHandle session)
        {
            await _collection.UpdateManyAsync(
                session,
                Builders<Order>.Filter.In(_ => _.Id, ids),
                Builders<Order>.Update.Set(_ => _.Delivered, true),
                new UpdateOptions()
                {
                    IsUpsert = false,
                });
        }

        public async Task CollectOrder(string id)
        {
            await _collection.UpdateOneAsync(
                _ => _.Id == id,
                Builders<Order>.Update.Set(_ => _.Collected, true),
                new UpdateOptions()
                {
                    IsUpsert = false
                });
        }
    }
}