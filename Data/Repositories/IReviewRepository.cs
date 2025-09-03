using WebApplication1.Models;

namespace WebApplication1.Data.Repositories
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> GetAllAsync();
        Task<Review?> GetByIdAsync(int id);
        Task AddAsync(Review review);
        void Update(Review review);
        void Delete(Review review);
        Task SaveChangesAsync();
    }
}