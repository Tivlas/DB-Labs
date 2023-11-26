using Shop.Models;

namespace Shop.Services.DbServices;
public interface IOrderService
{
	Task<bool> AddAsync(Order order);
	Task<IEnumerable<Order>?> GetListAsync();
}