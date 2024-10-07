using ECommerce.Helper;
using ECommerce.Models;
using ECommerce.Repository.Interface;
using ECommerce.Services.Interface;
using ECommerce.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Services.Implementation
{
    public class ShopService : IShopService
    {

        private readonly IUnitOfWork _uow;
        public ShopService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<ShopViewModel> GetShopViewAsync(
            List<Guid>? categoryId, 
            double? minPrice, 
            double? maxPrice, 
            string sortOrder, 
            int pageNumber, 
            int pageSize, 
            string searchString)
        {
            IQueryable<Product> query = _uow.Repository<Product>()
                                            .Queryable()
                                            .Include(p => p.Category);

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(x => x.Name.Contains(searchString) ||
                                         x.Description.Contains(searchString));
            }

            if (categoryId != null && categoryId.Count > 0)
            {
                query = query.Where(x => categoryId.Contains(x.CategoryId));
            }

            if (minPrice.HasValue)
            {
                query = query.Where(x => x.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(x => x.Price <= maxPrice.Value);
            }

             switch (sortOrder)
            {
                case "name_desc":
                    query = query.OrderByDescending(x => x.Name);
                    break;
                case "lowtohigh":
                    query = query.OrderBy(x => x.Price);
                    break;
                case "hightolow":
                    query = query.OrderByDescending(x => x.Price);
                    break;
                default:
                    query = query.OrderBy(x => x.Name);
                    break;
            }

            var totalRecords = await query.CountAsync();
            var paginatedProducts = await query
                .Skip((pageNumber-1)*pageSize)
                .Take(pageSize)
                .ToListAsync();
            var product = paginatedProducts.Select(product => new Product
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Category = product.Category,
                ImageUrl =product.ImageUrl,
                CategoryId = product.CategoryId
            });
            var shopview = new ShopViewModel
            {
                Products = new PaginatedList<Product>(product, totalRecords, pageNumber, pageSize)
            };
            return shopview;
        }
    }
}
