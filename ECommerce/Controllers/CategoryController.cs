using AutoMapper;
using ECommerce.Models;
using ECommerce.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryServices _categoryServicecs;
        private readonly IMapper _mapper;
        public CategoryController(ICategoryServices categoryServicecs, IMapper mapper)
        {
            _categoryServicecs = categoryServicecs;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var category = await _categoryServicecs.GetAllCategoriesAsync();
            return View(category);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                await _categoryServicecs.AddCategoryAsync(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }
        public async Task<IActionResult> Edit(Guid id)
        {
            var product = await _categoryServicecs.GetCategoryByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, Category category)
        {
            if (!ModelState.IsValid)
            {
                await _categoryServicecs.UpdateCategoryAsync(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }
        public async Task<IActionResult> Delete(Guid id)
        {
            var category = await _categoryServicecs.GetCategoryByIdAsync(id);
            return View(category);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var categoryViewModel = await _categoryServicecs.GetCategoryByIdAsync(id);
            var category = _mapper.Map<Category>(categoryViewModel);
            await _categoryServicecs.DeleteCategoryAsync(category);
            return RedirectToAction(nameof(Index));
        }
    }
}
