using ECommerce.Models;
using ECommerce.Repository.Interface;
using ECommerce.Services.Interface;

namespace ECommerce.Services.Implementation
{
    public class ShippingService : IShippingService
    {
        private readonly IUnitOfWork _uow;
        public ShippingService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<IEnumerable<Shipping>> GetAllShippingAsync()
        {
            var shipping = await _uow.Repository<Shipping>().GetAllAsync();
            return shipping;
        }
        public async Task<Shipping> AddShippingAsync(Shipping shipping)
        {
            var shippingStatus = _uow.Repository<ShippingStatus>().Queryable()
                                                                  .FirstOrDefault(ss => ss.Status == "Processing");
            shipping.ShippingStatusId = shippingStatus.Id;

            await _uow.Repository<Shipping>().AddAsync(shipping);
            await _uow.CompleteAsync();

            return shipping;
        }
        public async Task<Shipping> GetShippingByIdAsync(Guid shippingId)
        {
            return await _uow.Repository<Shipping>().GetByIdAsync(shippingId);
        }
        public async Task UpdateShippingStatusAsync(Guid shippingId, Shipping shipping)
        {
            var ship = await _uow.Repository<Shipping>().GetByIdAsync(shippingId);
            if (ship == null) throw new Exception("Shipping not found");

            ship.ShippingStatusId = shipping.ShippingStatusId;
            _uow.Repository<Shipping>().Update(ship);

            await _uow.CompleteAsync();
        }
        
    }
}
