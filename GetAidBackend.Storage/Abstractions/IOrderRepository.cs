using GetAidBackend.Domain;
using MongoDB.Driver;

namespace GetAidBackend.Storage.Abstractions
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<List<Order>> GetUserOrders(string userId);

        Task DeleteUserOrders(string userId);

        Task DeliverOrders(string[] ids, IClientSessionHandle session);

        Task<List<Order>> GetByIds(string[] ids);
        Task CollectOrder(string id);
    }
}