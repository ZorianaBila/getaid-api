using GetAidBackend.Domain;
using GetAidBackend.Services.Dtos;

namespace GetAidBackend.Services.Abstractionas
{
    public interface IOrderService : IServiceBase<Order, OrderDto>
    {
        Task<OrderDto> Add(string userId, Order order);
        Task CollectOrder(string orderId);
        Task DeleteUserOrders(string userId);
        Task<List<OrderDto>> GetAllOrders();
        Task<List<OrderDto>> GetNonCollectedOrders();
        Task<List<OrderDto>> GetNonDeliveredCollectedOrders();
        Task<List<OrderDto>> GetNonDeliveredOrders();
        Task<List<OrderDto>> GetUserOrders(string userId);
    }
}