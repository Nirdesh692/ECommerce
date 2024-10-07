using ECommerce.Models;

namespace ECommerce.Services.Interface
{
    public interface IPaymentMethodService
    {
        Task<IEnumerable<PaymentMethod>> GetAllPaymentMethodAsync();
        Task<PaymentMethod> GetPaymentMethodByIdAsync(Guid Id);
    }
}
