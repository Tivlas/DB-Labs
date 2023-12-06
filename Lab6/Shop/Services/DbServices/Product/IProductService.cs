using Shop.Models;

namespace Shop.Services.DbServices;
public interface IProductService
{
	Task<bool> AddAsync(Product product);
	Task<bool> DeleteAsync(int id);
	Task<Product?> GetByIdAsync(int id);
	Task<IEnumerable<Product>?> GetListAsync();
	Task<bool> UpdateAsync(Product product);
	Task<IEnumerable<Product>?> GetListByCategoryIdAsync(int categoryId);
}