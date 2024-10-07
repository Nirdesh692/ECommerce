using ECommerce.Models;
using ECommerce.Repository.Interface;
using ECommerce.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Services.Implementation
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IUnitOfWork _uow;
        private readonly IOrderService _orderService;
        public InvoiceService(IUnitOfWork uow, IOrderService orderService)
        {
            _uow = uow;
            _orderService = orderService;
        }
        public async Task<IEnumerable<Invoice>> GetAllInvoiceAsync()
        {
            var Invoices = await _uow.Repository<Invoice>().GetAllAsync();
            return Invoices;
        }
        public async Task<Invoice> CreateInvoiceAsync(string userId)
        {
            var order = await _uow.Repository<Order>().Queryable().LastOrDefaultAsync(x=>x.UserId == userId);
            var invoices = await GetAllInvoiceAsync();
            var count = invoices.Count();
            if (order == null)
            {
                throw new Exception("No orders found");
            }
            var invoice = new Invoice
            {
                Id = Guid.NewGuid(),
                OrderId = order.Id,
                InvoiceNumber = count + 1,
                InvoiceDate = DateOnly.FromDateTime(DateTime.Now),
                NetAmount = order.NetAmount,
                GrossAmount = order.GrossAmount,
                Discount = order.Discount,
                TaxAmount = order.TaxAmount,
                ShippingCharges = 0,
                Order = order
            };
            await _uow.Repository<Invoice>().AddAsync(invoice);
            await _uow.CompleteAsync();
            return invoice;
        }
        public async Task<Invoice> GetInvoiceByIdAsync(Guid id)
        {
            var invoice = await _uow.Repository<Invoice>().GetByIdAsync(id);
            return invoice;
        }
    }
}
