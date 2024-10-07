using ECommerce.Models;

namespace ECommerce.ViewModel
{
    public class OrderViewModel
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public DateTime OrderDate { get; set; }
        public IEnumerable<OrderItemViewModel> OrderItems { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public ShippingViewModel Shipping { get; set; }
        public double NetAmount { get; set; }
        public double GrossAmount { get; set; }
        public double TaxAmount { get; set; }
        public double Discount { get; set; }
    }
}
