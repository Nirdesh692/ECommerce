using ECommerce.Models;

namespace ECommerce.Services.Interface
{
    public interface IShippingService
    {
        Task<Shipping> AddShippingAsync(Shipping shipping);
        Task<IEnumerable<Shipping>> GetAllShippingAsync();
        Task<Shipping> GetShippingByIdAsync(Guid shippingId);
        Task UpdateShippingStatusAsync(Guid shippingId, Shipping shipping);
    }
}
