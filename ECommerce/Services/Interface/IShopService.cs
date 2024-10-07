using ECommerce.Helper;
using ECommerce.ViewModel;

namespace ECommerce.Services.Interface
{
    public interface IShopService
    {
        Task<ShopViewModel> GetShopViewAsync(List<Guid>? categoryId, double? minPrice, double? maxPrice, string sortOrder, int pageNumber, int pageSize, string s);
    }
}
