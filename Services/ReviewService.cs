using WebApplication1.Data.Repositories;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _repository;

        public ReviewService(IReviewRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<Review>> GetAllReviewsAsync()
        {
            return _repository.GetAllAsync();
        }

        public Task<Review?> GetByIdAsync(int id)
        {
            return _repository.GetByIdAsync(id);
        }

        public async Task SubmitReviewAsync(Review review, string userId)
        {
            review.UserId = userId;
            review.CreatedAt = DateTime.UtcNow;
            await _repository.AddAsync(review);
            await _repository.SaveChangesAsync();
        }


        public async Task UpdateAsync(Review review)
        {
            _repository.Update(review);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var review = await _repository.GetByIdAsync(id);
            if (review != null)
            {
                _repository.Delete(review);
                await _repository.SaveChangesAsync();
            }
        }
    }
}