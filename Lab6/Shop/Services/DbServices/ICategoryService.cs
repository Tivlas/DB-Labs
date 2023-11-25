using Shop.Models;

namespace Shop.Services.DbServices;
public interface ICategoryService
{
	Task<bool> AddCategoryAsync(string name);
	Task<bool> DeleteAsync(int id);
	Task<IEnumerable<ProductCategory>?> GetCategoriesAsync();
	Task<ProductCategory?> GetCategoryByIdAsync(int id);
	Task<bool> UpdateAsync(int id, string newName);
}