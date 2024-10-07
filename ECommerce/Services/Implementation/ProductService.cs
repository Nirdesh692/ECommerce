using AutoMapper;
using ECommerce.Models;
using ECommerce.Repository.Interface;
using ECommerce.Services.Interface;
using ECommerce.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Services.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<ProductService> _logger;
        private IMapper _mapper;
        public ProductService(IUnitOfWork uow, ILogger<ProductService> logger, IMapper mapper)
        {
            _uow = uow;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ProductViewModel>> GetAllProductsAsync()
        {
            try
            {
                var products = _uow.Repository<Product>().Queryable()
                                                         .Include(p => p.Category).ToList();

                var product = _mapper.Map<IEnumerable<ProductViewModel>>(products);
                return product;

            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching all products.");
                return new List<ProductViewModel>();
            }
        }
        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(IEnumerable<Guid?> categoryId)
        {
            var products = await _uow.Repository<Product>().FindAsync(p => categoryId.Contains(p.CategoryId));

            return products;
        }
        public async Task<ProductViewModel> GetProductByIdAsync(Guid id)
        {
            var product = await _uow.Repository<Product>().GetByIdAsync(id);
            if (product == null) throw new Exception("Product not found");

            var productView = _mapper.Map<ProductViewModel>(product);

            return productView;
        }

        public async Task AddProductAsync(Product product)
        {
            await _uow.Repository<Product>().AddAsync(product);
            await _uow.CompleteAsync();
        }
        public async Task UpdateProductAsync(Product product)
        {
            _uow.Repository<Product>().Update(product);
            await _uow.CompleteAsync();
        }
        public async Task DeleteProductAsync(Product product)
        {
            await _uow.Repository<Product>().Remove(product);
            await _uow.CompleteAsync();
        }
    }
}
