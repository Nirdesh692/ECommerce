using ECommerce.Models;

namespace ECommerce.ViewModel
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int StockQuantity { get; set; }
        public double Price { get; set; }
        public IFormFile? Image { get; set; }
        public string? ImageUrl { get; set; }
        public Guid CategoryId { get; set; }
        public Category? Category { get; set; }
        public IEnumerable<Review>? Reviews { get; set; }
    }
}
