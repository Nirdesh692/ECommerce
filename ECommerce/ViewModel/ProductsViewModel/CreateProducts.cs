namespace ECommerce.ViewModel.ProductsViewModel
{
    public class CreateProducts
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int StockQuantity { get; set; }
        public DateTime? DateAdded { get; set; }
        public IFormFile? Image { get; set; }
        public string? ImageUrl { get; set; }
        public Guid CategoryId { get; set; }
    }
}
