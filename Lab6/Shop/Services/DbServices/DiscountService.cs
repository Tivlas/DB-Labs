using Microsoft.EntityFrameworkCore;
using Shop.Models;

namespace Shop.Services.DbServices;

public class DiscountService : IDiscountService
{
	private readonly DbLabsContext _context;

	public DiscountService(DbLabsContext context)
	{
		_context = context;
	}

	public async Task<IEnumerable<Discount>?> GetListAsync()
	{
		return await _context.Discounts.FromSqlRaw("select * from get_discounts()").ToListAsync();
	}

	public async Task<Discount?> GetByIdAsync(int id)
	{
		return await _context.Discounts.FromSqlInterpolated($"select * from get_discount_by_id({id})").FirstOrDefaultAsync();
	}

	public async Task<bool> AddAsync(Discount ds)
	{
		try
		{
			await _context.Database.ExecuteSqlInterpolatedAsync($"call insert_discount({ds.Description},{ds.Name}, {ds.Percent},{ds.IsActive})");
			return true;
		}
		catch (Exception e)
		{

		}
		return false;
	}

	public async Task<bool> UpdateAsync(Discount ds)
	{
		try
		{
			await _context.Database.ExecuteSqlInterpolatedAsync($"call update_discount({ds.DiscountId},{ds.Description},{ds.Name}, {ds.Percent},{ds.IsActive})");
			return true;
		}
		catch (Exception e)
		{

		}
		return false;
	}

	public async Task<bool> DeleteAsync(Discount ds)
	{
		try
		{
			await _context.Database.ExecuteSqlInterpolatedAsync($"call delete_discount_by_id({ds.DiscountId})");
			return true;
		}
		catch (Exception e)
		{

		}
		return false;
	}
}
