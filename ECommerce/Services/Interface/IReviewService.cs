using ECommerce.Models;

namespace ECommerce.Services.Interface
{
    public interface IReviewService
    {
        Task<IEnumerable<Review>> GetAllReviewsAsync();
        Task<Review> AddReviewAsync(Review review);
        Task<IEnumerable<Review>> GetReviewsByProductIdAsync(Guid id);
    }
}
