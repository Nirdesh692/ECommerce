using ECommerce.Models;

namespace ECommerce.Services.Interface
{
    public interface IPaymentService
    {
        Task<Payment> AddPaymentAsync(Payment payment);
        Task<Payment> GetPaymentByIdAsync(Guid paymentId);
        Task UpdatePaymentStatusAsync(Guid paymentId, Guid paymentStatusId);
    }
}
