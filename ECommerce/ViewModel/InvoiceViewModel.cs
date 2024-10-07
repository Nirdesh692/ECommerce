using ECommerce.Models;

namespace ECommerce.ViewModel
{
    public class InvoiceViewModel
    {
        public IEnumerable<Invoice> Invoices {  get; set; }
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}
