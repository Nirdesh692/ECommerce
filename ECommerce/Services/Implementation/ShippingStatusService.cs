using ECommerce.Models;
using ECommerce.Repository.Interface;
using ECommerce.Services.Interface;

namespace ECommerce.Services.Implementation
{
    public class ShippingStatusService : IShippingStatusService
    {
        private readonly IUnitOfWork _uow;
        public ShippingStatusService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<IEnumerable<ShippingStatus>> GetAllShippingStatus()
        {
            var shippingStatuses = await _uow.Repository<ShippingStatus>().GetAllAsync();
            return shippingStatuses;
        }
        public async Task<ShippingStatus> GetShippingStatusById(Guid Id)
        {
            var shippingStatus = await _uow.Repository<ShippingStatus>().GetByIdAsync(Id);
            return shippingStatus;
        }
        public async Task AddShippingStatusAsync(ShippingStatus shippingStatus)
        {
            await _uow.Repository<ShippingStatus>().AddAsync(shippingStatus);
            await _uow.CompleteAsync();
        }
        public async Task UpdateShippingStatusAsync(ShippingStatus shippingStatus)
        {
            _uow.Repository<ShippingStatus>().Update(shippingStatus);
            await _uow.CompleteAsync();
        }
        public async Task DeleteShippingStatusAsync(ShippingStatus shippingStatus)
        {
            await _uow.Repository<ShippingStatus>().Remove(shippingStatus);
            await _uow.CompleteAsync();
        }
    }
}
