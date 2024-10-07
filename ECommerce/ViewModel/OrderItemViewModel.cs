namespace ECommerce.ViewModel
{
    public class OrderItemViewModel
    {
        public Guid? ProductId { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double TotalPrice { get; set; }
        public string ImageUrl { get; set; }
        public string Name { get; set; }
    }
}
