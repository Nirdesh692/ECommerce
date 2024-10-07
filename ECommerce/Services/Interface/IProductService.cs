using ECommerce.Models;
using ECommerce.ViewModel;

namespace ECommerce.Services.Interface
{
    public interface IProductService
    {
        Task<IEnumerable<ProductViewModel>> GetAllProductsAsync();
        Task<IEnumerable<Product>> GetProductsByCategoryAsync(IEnumerable<Guid?>categoryId);
        Task<ProductViewModel> GetProductByIdAsync(Guid id);
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(Product product);
    }
}
