namespace ECommerce.Models
{
    public class Shipping
    {
        public Guid Id { get; set; }
        public DateTime? ShippingDate { get; set; }
        public TimeOnly? DeliveryTime { get; set; }
        public string Address { get; set; }
        public Guid? ShippingStatusId { get; set; }
        public ShippingStatus? ShippingStatus { get; set; }
        
    }
}
