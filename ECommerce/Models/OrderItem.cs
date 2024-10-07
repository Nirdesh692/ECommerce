namespace ECommerce.Models
{
    public class OrderItem
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public Guid OrderId { get; set; }
        public Order? Order { get; set; }
        public Guid? ProductId {  get; set; }
        public Product? Products { get; set; }

    }
}
