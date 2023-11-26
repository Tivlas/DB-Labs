using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Shop.Models;

namespace Shop.Services.DbServices;

public class ReviewService : IReviewService
{
	private readonly DbLabsContext _context;

	public ReviewService(DbLabsContext context)
	{
		_context = context;
	}

	public async Task<IEnumerable<Review>?> GetListAsync()
	{
		return await _context.Reviews.FromSqlRaw("select * from get_reviews()")
			.Select(r => new Review
			{
				ReviewId = r.ReviewId,
				ClientId = r.ClientId,
				ProductId = r.ProductId,
				Client = r.Client,
				Product = r.Product,
				Date = r.Date,
				Rating = r.Rating,
				Content = r.Content,
			}).ToListAsync();
	}

	public async Task<IEnumerable<Review>?> GetListByproductIdAsync(int productId)
	{
		return await _context.Reviews.FromSqlInterpolated($"select * from get_reviews_by_product_id({productId})")
			.Select(r => new Review
			{
				ReviewId = r.ReviewId,
				ClientId = r.ClientId,
				ProductId = r.ProductId,
				Client = r.Client,
				Product = r.Product,
				Date = r.Date,
				Rating = r.Rating,
				Content = r.Content,
			}).ToListAsync();
	}

	public async Task<Review?> GetByIdAsync(int reviewId)
	{
		return await _context.Reviews.FromSqlInterpolated($"select * from get_review_by_id({reviewId})")
			.Select(r => new Review
			{
				ReviewId = r.ReviewId,
				ClientId = r.ClientId,
				ProductId = r.ProductId,
				Client = r.Client,
				Product = r.Product,
				Date = r.Date,
				Rating = r.Rating,
				Content = r.Content,
			}).FirstOrDefaultAsync();
	}

	public async Task<bool> AddAsync(Review r)
	{
		try
		{
			await _context.Database.ExecuteSqlInterpolatedAsync($"call insert_review({r.ProductId},{r.ClientId},{r.Content},{r.Rating})");
			return true;
		}
		catch (Exception e)
		{

		}
		return false;
	}

	public async Task<bool> UpdateAsync(Review r)
	{
		try
		{
			await _context.Database.ExecuteSqlInterpolatedAsync($"call update_review({r.ReviewId},{r.Content},{r.Rating})");
			return true;
		}
		catch (Exception e)
		{

		}
		return false;
	}

	public async Task<bool> DeleteAsync(Review r)
	{
		try
		{
			await _context.Database.ExecuteSqlInterpolatedAsync($"call delete_review({r.ReviewId})");
			return true;
		}
		catch (Exception e)
		{

		}
		return false;
	}
}
