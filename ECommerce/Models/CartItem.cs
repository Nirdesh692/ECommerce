namespace ECommerce.Models
{
    public class CartItem
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public bool? Selected { get; set; }

        public Guid CartId { get; set; }
        public Cart? Cart { get; set; }
        
        public Guid ProductId { get; set; }
        public Product? Product { get; set; }
    }
}
