using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        //[NotMapped]
        //public IFormFile? Image { get; set; }
        //public string? ImageUrl { get; set; }
        public IEnumerable<Product>? Products { get; set; }
    }
}
