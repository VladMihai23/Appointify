using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IReviewService
    {
        Task<IEnumerable<Review>> GetAllReviewsAsync();
        Task<Review?> GetByIdAsync(int id);
        Task SubmitReviewAsync(Review review, string userId);
        Task UpdateAsync(Review review);
        Task DeleteAsync(int id);
    }
}