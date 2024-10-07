using ECommerce.Services.Implementation;
using ECommerce.Services.Interface;
using ECommerce.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECommerce.Controllers
{
    public class ShopController : Controller
    {
        private readonly IShopService _shopService;
        private readonly ICategoryServices _categoryServices;
        public ShopController(IShopService shopService, ICategoryServices categoryServices)
        {
            _shopService = shopService;
            _categoryServices = categoryServices;
        }
        public async Task<IActionResult> ShopView(List<Guid>? categoryId, double? minPrice, double? maxPrice, string sortOrder = "", int pageNumber = 1, int pageSize = 8, string searchString = "")
        {
            var products = await _shopService.GetShopViewAsync(categoryId, minPrice, maxPrice,  sortOrder, pageNumber, pageSize, searchString);
            ViewData["CategoryId"]  = new SelectList(await _categoryServices.GetAllCategoriesAsync(), "Id", "Name");
            return View(products);
        }
    }
}
