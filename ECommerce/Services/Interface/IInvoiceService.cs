using ECommerce.Models;

namespace ECommerce.Services.Interface
{
    public interface IInvoiceService
    {
        Task<IEnumerable<Invoice>> GetAllInvoiceAsync();
        Task<Invoice> GetInvoiceByIdAsync(Guid Id);
        Task<Invoice> CreateInvoiceAsync(string userId);
    }
}
