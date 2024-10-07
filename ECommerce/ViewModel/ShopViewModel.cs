using ECommerce.Helper;
using ECommerce.Models;

namespace ECommerce.ViewModel
{
    public class ShopViewModel
    {
        public PaginatedList<Product> Products { get; set; }
        public IEnumerable<Category> Categories { get; set; }

        public Guid? CategoryId { get; set; }
        public Guid? ProductId { get; set; }
    }
}
