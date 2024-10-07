namespace ECommerce.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public DateTime OrderDate { get; set; }
        public double NetAmount { get; set; }
        public double GrossAmount { get; set; }
        public double TaxAmount { get; set; }
        public double Discount { get; set; }

        public string? UserId { get; set; }
        public User User { get; set; }
        
        public Guid? OrderStatusId { get; set; }
        public OrderStatus? OrderStatus { get; set; }
        
        public Guid? ShippingId { get; set; }
        public Shipping? Shipping { get; set; }
                
        public IEnumerable<OrderItem> OrderItems { get; set; }
    }
}
