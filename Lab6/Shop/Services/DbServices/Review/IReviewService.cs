using Shop.Models;

namespace Shop.Services.DbServices;
public interface IReviewService
{
	Task<bool> AddAsync(Review r);
	Task<bool> DeleteAsync(Review r);
	Task<Review?> GetByIdAsync(int reviewId);
	Task<IEnumerable<Review>?> GetListAsync();
	Task<IEnumerable<Review>?> GetListByproductIdAsync(int productId);
	Task<bool> UpdateAsync(Review r);
}