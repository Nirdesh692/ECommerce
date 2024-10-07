namespace ECommerce.ViewModel.CategoriesViewModel
{
    public class UpdateCategory
    {
        public string Name { get; set; }
        public IFormFile? Image { get; set; }
        public string? ImageUrl { get; set; }
    }
}
