using Shop.Models;

namespace Shop.Services.DbServices;
public interface ICategoryService
{
	Task<bool> AddAsync(string name);
	Task<bool> DeleteAsync(int id);
	Task<IEnumerable<ProductCategory>?> GetListAsync();
	Task<ProductCategory?> GetByIdAsync(int id);
	Task<bool> UpdateAsync(int id, string newName);
}