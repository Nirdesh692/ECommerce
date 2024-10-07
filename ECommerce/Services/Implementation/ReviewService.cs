using ECommerce.Models;
using ECommerce.Repository.Implementation;
using ECommerce.Repository.Interface;
using ECommerce.Services.Interface;

namespace ECommerce.Services.Implementation
{
    public class ReviewService : IReviewService
    {
        private readonly IUnitOfWork _uow;
        public ReviewService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<IEnumerable<Review>> GetAllReviewsAsync()
        {
            var reviews = await _uow.Repository<Review>().GetAllAsync();
            return reviews;
        } 
        public async Task<Review> AddReviewAsync(Review review)
        {
            var reviews = new Review
            {
                Id = Guid.NewGuid(),
                Ratings = review.Ratings,
                Comment = review.Comment,
                ProductId = review.ProductId,
                UserId = review.UserId
            };

            await _uow.Repository<Review>().AddAsync(reviews);
            await _uow.CompleteAsync();

            return review;
        }
        public async Task<IEnumerable<Review>> GetReviewsByProductIdAsync(Guid productId)
        {
            return await _uow.Repository<Review>().FindAsync(r => r.ProductId == productId);
        }
    }
}
