using ECommerce.Models;

namespace ECommerce.ViewModel
{
    public class CartViewModel
    {
        public Guid Id { get; set; }
        public string? UserId { get; set; }
        public double GrandTotal { get; set; }
        public string? Address { get; set; }
        public Guid? PaymentMethodId { get; set; }
        public Guid ShippingId { get; set; }
        public Guid OrderId { get; set; }
        public IEnumerable<CartItemsViewModel>? CartItems { get; set; }
    }
}
