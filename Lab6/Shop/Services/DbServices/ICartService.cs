using Shop.Models;

namespace Shop.Services.DbServices;
public interface ICartService
{
	Task<bool> AddItemAsync(CartItem ci);
	Task<bool> DeleteItemAsync(int id);
	Task<CartItem?> GetItemByIdAsync(int id);
	Task<IEnumerable<CartItem>?> GetItemListAsync(int cartId);
	Task<bool> UpdateItemAsync(CartItem ci);
	Task<int?> GetCartIdByClientIdAsync(int clientId);
}