using ECommerce.Helper;
using ECommerce.Models;
using ECommerce.ViewModel;

namespace ECommerce.Services.Interface
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(string userId, Guid shippingId);
        Task<PaginatedList<Order>> GetOrderByUserIdAsync(string userId, int pageNumber, int pageSize);
        Task<PaginatedList<Order>> GetAllOrdersAsync(int pageNumber, int pageSize);
        Task<OrderViewModel> GetOrderByIdAsync(Guid id);
        Task UpdateOrderStatusAsync(Guid orderId, Guid OrderStatusId);
    }
}
