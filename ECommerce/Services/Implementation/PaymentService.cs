using AutoMapper;
using ECommerce.Models;
using ECommerce.Repository.Interface;
using ECommerce.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Services.Implementation
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<PaymentService> _logger;
        private readonly IMapper _mapper;
        public PaymentService(IUnitOfWork uow, ILogger<PaymentService> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _uow = uow;
        }
        public async Task<Payment> AddPaymentAsync(Payment payment)
        {
            var status = await _uow.Repository<PaymentStatus>().Queryable().FirstOrDefaultAsync(ps => ps.Status == "Unpaid");
            payment.PaymentStatusId = status.Id;
            await _uow.Repository<Payment>().AddAsync(payment);
            await _uow.CompleteAsync();

            return payment;
        }
        public async Task<Payment> GetPaymentByIdAsync(Guid paymentId)
        {
            return await _uow.Repository<Payment>().GetByIdAsync(paymentId);
        }
        public async Task UpdatePaymentStatusAsync(Guid paymentId, Guid paymentStatusId)
        {
            var payment = await _uow.Repository<Payment>().GetByIdAsync(paymentId);
            if (payment == null) throw new Exception("Payment not found");

            payment.PaymentStatusId = paymentStatusId;
            _uow.Repository<Payment>().Update(payment);
            await _uow.CompleteAsync();
        }
    }
}
