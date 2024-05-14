using GetAidBackend.Domain;

namespace GetAidBackend.Storage.Abstractions
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<List<Order>> GetUserOrders(string userId);

        Task DeleteUserOrders(string userId);
    }
}