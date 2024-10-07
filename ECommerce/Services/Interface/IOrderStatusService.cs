using ECommerce.Models;

namespace ECommerce.Services.Interface
{
    public interface IOrderStatusService
    {
        Task<IEnumerable<OrderStatus>> GetAllOrderStatusAsync();
        Task<OrderStatus> GetOrderStatusById(Guid Id);
        Task AddOrderStatusAsync(OrderStatus orderStatus);
        Task UpdateOrderStatusAsync(OrderStatus orderStatus);
        Task DeleteOrderStatusAsync(OrderStatus orderStatus);
    }
}
