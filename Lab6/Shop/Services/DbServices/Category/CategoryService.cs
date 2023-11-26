using Microsoft.EntityFrameworkCore;
using Shop.Models;

namespace Shop.Services.DbServices;

public class CategoryService : ICategoryService
{
	private readonly DbLabsContext _context;
	private readonly ILogger<CategoryService> _logger;

	public CategoryService(DbLabsContext context, ILogger<CategoryService> logger)
	{
		_context = context;
		_logger = logger;
	}

	public async Task<ProductCategory?> GetByIdAsync(int id)
	{
		return await _context.ProductCategories.FromSqlInterpolated($"select * from product_category where product_category_id = {id}").FirstOrDefaultAsync();
	}

	public async Task<IEnumerable<ProductCategory>?> GetListAsync()
	{
		return await _context.ProductCategories.FromSqlRaw("select * from product_category").ToListAsync();
	}

	public async Task<bool> AddAsync(string name)
	{
		try
		{
			await _context.Database.ExecuteSqlInterpolatedAsync($"call insert_category({name})");
			return true;
		}
		catch (Exception e)
		{
			_logger.LogError(e.StackTrace + ": " + e.Message);
		}
		return false;
	}

	public async Task<bool> DeleteAsync(int id)
	{
		try
		{
			await _context.Database.ExecuteSqlInterpolatedAsync($"call delete_category_by_id({id})");
			return true;
		}
		catch (Exception e)
		{
			_logger.LogError(e.StackTrace + ": " + e.Message);
		}
		return false;
	}

	public async Task<bool> UpdateAsync(int id, string newName)
	{
		try
		{
			await _context.Database.ExecuteSqlInterpolatedAsync($"call update_category({id}, {newName})");
			return true;
		}
		catch (Exception e)
		{
			_logger.LogError(e.StackTrace + ": " + e.Message);
		}
		return false;
	}
}
