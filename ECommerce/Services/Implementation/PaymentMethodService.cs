using ECommerce.Models;
using ECommerce.Repository.Interface;
using ECommerce.Services.Interface;

namespace ECommerce.Services.Implementation
{
    public class PaymentMethodService : IPaymentMethodService
    {
        private readonly IUnitOfWork _uow;
        public PaymentMethodService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<IEnumerable<PaymentMethod>> GetAllPaymentMethodAsync()
        {
            try
            {

                var paymentMethods = await _uow.Repository<PaymentMethod>().GetAllAsync();
                return paymentMethods;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<PaymentMethod> GetPaymentMethodByIdAsync(Guid Id)
        {
            var paymentMethod = await _uow.Repository<PaymentMethod>().GetByIdAsync(Id);
            return paymentMethod;
        }
        public async Task AddPaymentMethodAsync(PaymentMethod paymentMethod)
        {
            await _uow.Repository<PaymentMethod>().AddAsync(paymentMethod);
            await _uow.CompleteAsync();
        }
        public async Task UpdatePaymentMethodAsync(PaymentMethod paymentMethod)
        {
            _uow.Repository<PaymentMethod>().Update(paymentMethod);
            await _uow.CompleteAsync();
        }
        public async Task DeletePaymentMethodAsync(PaymentMethod paymentMethod)
        {
            await _uow.Repository<PaymentMethod>().Remove(paymentMethod);
            await _uow.CompleteAsync();
        }
    }
}
