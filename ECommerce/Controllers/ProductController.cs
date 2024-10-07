using ECommerce.Models;
using ECommerce.Services.Interface;
using ECommerce.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ECommerce.Helper;
using AutoMapper;
using ECommerce.ViewModel.ProductsViewModel;

namespace ECommerce.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryServices _categoryServices;
        private readonly IShopService _shopService;
        private readonly ILogger<ProductController> _logger;
        private readonly IMapper _mapper;
        public ProductController(IProductService productService, ICategoryServices categoryServices, IShopService shopService, ILogger<ProductController> logger, IMapper mapper)
        {
            _productService = productService;
            _categoryServices = categoryServices;
            _shopService = shopService;
            _logger = logger;
            _mapper = mapper;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            try
            {
                var products = await _productService.GetAllProductsAsync();
                return View(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching all products.");
                return StatusCode(500, "Internal server error");
            }
        }

        public async Task<IActionResult> Details(Guid id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);
                if (product == null)
                {
                    _logger.LogWarning("Product with ID {ProductId} was not found.", id);
                    return NotFound();
                }
                return View(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching product details for Product ID {ProductId}.", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            try
            {
                await PopulateCategoryListAsync();
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while loading the Create Product page.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateProducts products)
        {
            if (ModelState.IsValid)
            {
                products.Id = Guid.NewGuid();
                try
                {
                    products.ImageUrl = await HandleImageUpload.UploadImageAsync(products.Image);

                    var product = _mapper.Map<Product>(products);

                    await _productService.AddProductAsync(product);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Error uploading file: {ex.Message}");
                }
            }
            await PopulateCategoryListAsync();
            return View(products);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Guid id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);
                if (product == null)
                {
                    _logger.LogWarning("Product with ID {ProductId} was not found.", id);
                    return NotFound();
                }
                await PopulateCategoryListAsync();
                return View(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while loading the Edit Product page for Product ID {ProductId}.", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Guid id, ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    productViewModel.Id = id;
                    productViewModel.ImageUrl = await HandleImageUpload.UploadImageAsync(productViewModel.Image);

                    var product = _mapper.Map<Product>(productViewModel);

                    await _productService.UpdateProductAsync(product);
                    _logger.LogInformation("Product {ProductName} was successfully updated.", productViewModel.Name);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while updating the product {ProductName}.", productViewModel.Name);
                    ModelState.AddModelError(string.Empty, $"Error updating product: {ex.Message}");
                }
            }
            await PopulateCategoryListAsync();
            return View(productViewModel);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);
                if (product == null)
                {
                    _logger.LogWarning("Product with ID {ProductId} was not found.", id);
                    return NotFound();
                }
                return View(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while loading the Delete Product page for Product ID {ProductId}.", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);
                if (product == null)
                {
                    _logger.LogWarning("Product with ID {ProductId} was not found for deletion.", id);
                    return NotFound();
                }

                var Product = _mapper.Map<Product>(product);

                await _productService.DeleteProductAsync(Product);
                _logger.LogInformation("Product {ProductName} was successfully deleted.", product.Name);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the product with ID {ProductId}.", id);
                return StatusCode(500, "Internal server error");
            }
        }

        public async Task<IActionResult> ProductByCategoryId(IEnumerable<Guid?> categoryId)
        {
            try
            {
                var product = await _productService.GetProductsByCategoryAsync(categoryId);
                return View(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving products by category.");
                return StatusCode(500, "Internal server error");
            }
        }

        private async Task PopulateCategoryListAsync()
        {
            ViewData["CategoryId"] = new SelectList(await _categoryServices.GetAllCategoriesAsync(), "Id", "Name");
        }


        public async Task<IActionResult> ShopView(List<Guid>? categoryIds, double? minPrice, double? maxPrice, string sortOrder = "", int pageNumber = 1, int pageSize = 10, string searchString = "")
        {
            try
            {
                var products = await _shopService.GetShopViewAsync(categoryIds, minPrice, maxPrice, sortOrder, pageNumber, pageSize, searchString);
                return View(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while loading the shop view.");
                return StatusCode(500, "Internal server error");
            }

        }
    }
}
