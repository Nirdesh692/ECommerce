using AutoMapper;
using ECommerce.Models;
using ECommerce.Repository.Interface;
using ECommerce.Services.Interface;
using ECommerce.ViewModel;

namespace ECommerce.Services.Implementation
{
    public class CategoryService : ICategoryServices
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public CategoryService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CategoryViewModel>> GetAllCategoriesAsync()
        {
            var categories = await _uow.Repository<Category>().GetAllAsync();
            var categoriesView = _mapper.Map<IEnumerable<CategoryViewModel>>(categories);
            return categoriesView;
        }
        public async Task<CategoryViewModel> GetCategoryByIdAsync(Guid id)
        {
            var category = await _uow.Repository<Category>().GetByIdAsync(id);
            if (category == null) throw new Exception("Category not found");

            var categoryView = _mapper.Map<CategoryViewModel>(category);

            return categoryView;
        }
        public async Task AddCategoryAsync(Category category)
        {
            await _uow.Repository<Category>().AddAsync(category);
            await _uow.CompleteAsync();
        }
        public async Task UpdateCategoryAsync(Category category)
        {
            _uow.Repository<Category>().Update(category);
            await _uow.CompleteAsync();
        }
        public async Task DeleteCategoryAsync(Category category)
        {
            await _uow.Repository<Category>().Remove(category);
            await _uow.CompleteAsync();
        }

    }
}
