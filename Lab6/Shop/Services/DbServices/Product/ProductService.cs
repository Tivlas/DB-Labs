using Microsoft.EntityFrameworkCore;
using Shop.Models;

namespace Shop.Services.DbServices;

public class ProductService : IProductService
{
	private readonly DbLabsContext _context;
	private readonly ILogger<ProductService> _logger;

	public ProductService(DbLabsContext context, ILogger<ProductService> logger)
	{
		_context = context;
		_logger = logger;
	}

	public async Task<IEnumerable<Product>?> GetListAsync()
	{
		return await _context.Products.FromSqlInterpolated($"select * from get_products_with_categories()")
			.Select(p => new Product
			{
				ProductId = p.ProductId,
				Name = p.Name,
				Brand = p.Brand,
				Price = p.Price,
				ProductCategoryId = p.ProductCategoryId,
				ProductCategory = p.ProductCategory,
				ProductionDate = p.ProductionDate,
				Quantity = p.Quantity,
				Description = p.Description,
			}).ToListAsync();
	}

	public async Task<IEnumerable<Product>?> GetListByCategoryIdAsync(int categoryId)
	{
		return await _context.Products.FromSqlInterpolated($"select * from get_products_with_categories_by_category_id({categoryId})")
			.Select(p => new Product
			{
				ProductId = p.ProductId,
				Name = p.Name,
				Brand = p.Brand,
				Price = p.Price,
				ProductCategoryId = p.ProductCategoryId,
				ProductCategory = p.ProductCategory,
				ProductionDate = p.ProductionDate,
				Quantity = p.Quantity,
				Description = p.Description,
			}).ToListAsync();
	}

	public async Task<Product?> GetByIdAsync(int id)
	{
		return await _context.Products.FromSqlInterpolated($"select * from get_product_with_category_by_id({id})")
			.Select(p => new Product
			{
				ProductId = p.ProductId,
				Name = p.Name,
				Brand = p.Brand,
				Price = p.Price,
				ProductCategoryId = p.ProductCategoryId,
				ProductCategory = p.ProductCategory,
				ProductionDate = p.ProductionDate,
				Quantity = p.Quantity,
				Description = p.Description,
			})
			.FirstOrDefaultAsync();
	}

	public async Task<bool> AddAsync(Product product)
	{
		try
		{
			await _context.Database.ExecuteSqlInterpolatedAsync($"call insert_product({product.ProductCategoryId},{product.Price},{product.Name},{product.ProductionDate},{product.Quantity},{product.Brand},{product.Description})");
			return true;
		}
		catch (Exception e)
		{
			_logger.LogError(e.StackTrace + ": " + e.Message);
		}
		return false;
	}

	public async Task<bool> UpdateAsync(Product product)
	{
		try
		{
			await _context.Database.ExecuteSqlInterpolatedAsync($"call update_product({product.ProductId},{product.ProductCategoryId},{product.Price},{product.Name},{product.ProductionDate},{product.Quantity},{product.Brand},{product.Description})");
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
			await _context.Database.ExecuteSqlInterpolatedAsync($"call delete_product_by_id({id})");
			return true;
		}
		catch (Exception e)
		{
			_logger.LogError(e.StackTrace + ": " + e.Message);
		}
		return false;
	}
}
