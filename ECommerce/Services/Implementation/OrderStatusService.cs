using ECommerce.Models;
using ECommerce.Repository.Interface;
using ECommerce.Services.Interface;

namespace ECommerce.Services.Implementation
{
    public class OrderStatusService : IOrderStatusService
    {
        private readonly IUnitOfWork _uow;
        public OrderStatusService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<IEnumerable<OrderStatus>> GetAllOrderStatusAsync()
        {
            var orderStatuses = await _uow.Repository<OrderStatus>().GetAllAsync();
            return orderStatuses;
        }
        public async Task<OrderStatus> GetOrderStatusById(Guid Id)
        {
            var orderStatus = await _uow.Repository<OrderStatus>().GetByIdAsync(Id);
            return orderStatus;
        }
        public async Task AddOrderStatusAsync(OrderStatus orderStatus)
        {
            await _uow.Repository<OrderStatus>().AddAsync(orderStatus);
            await _uow.CompleteAsync();
        }
        public async Task UpdateOrderStatusAsync(OrderStatus orderStatus)
        {
            _uow.Repository<OrderStatus>().Update(orderStatus);
            await _uow.CompleteAsync();
        }
        public async Task DeleteOrderStatusAsync(OrderStatus orderStatus)
        {
            await _uow.Repository<OrderStatus>().Remove(orderStatus);
            await _uow.CompleteAsync();
        }
    }
}
