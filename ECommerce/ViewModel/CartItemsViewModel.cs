namespace ECommerce.ViewModel
{
    public class CartItemsViewModel
    {
        public Guid ProductId { get; set; }
        public bool? Selected { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double TotalPrice { get; set; }
        public string ImageUrl { get; set; }
        public int? StockQuantity { get; set; }
        public string Name { get; set; }
    }
}
