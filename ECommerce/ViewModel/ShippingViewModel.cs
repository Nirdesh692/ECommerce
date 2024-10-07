namespace ECommerce.ViewModel
{
    public class ShippingViewModel
    {
        public string Address { get; set; }
        public Guid OrderId { get; set; } 
        public DateTime? ShippingDate { get; set; }
        public TimeOnly? DeliveryTime { get; set; }
    }
}  