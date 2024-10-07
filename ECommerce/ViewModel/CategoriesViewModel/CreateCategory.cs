namespace ECommerce.ViewModel.CategoriesViewModel
{
    public class CreateCategory
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IFormFile? Image { get; set; }
        public string? ImageUrl { get; set; }
    }
}
