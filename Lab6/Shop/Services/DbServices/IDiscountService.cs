using Shop.Models;

namespace Shop.Services.DbServices;
public interface IDiscountService
{
	Task<bool> AddAsync(Discount ds);
	Task<bool> DeleteAsync(int id);
	Task<Discount?> GetByIdAsync(int id);
	Task<IEnumerable<Discount>?> GetListAsync();
	Task<bool> UpdateAsync(Discount ds);
}