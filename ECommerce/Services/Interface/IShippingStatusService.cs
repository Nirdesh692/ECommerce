using ECommerce.Models;

namespace ECommerce.Services.Interface
{
    public interface IShippingStatusService
    {
        Task<IEnumerable<ShippingStatus>> GetAllShippingStatus();
        Task<ShippingStatus> GetShippingStatusById(Guid Id);
        Task AddShippingStatusAsync(ShippingStatus shippingStatus);
        Task UpdateShippingStatusAsync(ShippingStatus shippingStatus);
        Task DeleteShippingStatusAsync(ShippingStatus shippingStatus);
    }
}
