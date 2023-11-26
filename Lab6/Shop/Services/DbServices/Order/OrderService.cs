using Microsoft.EntityFrameworkCore;
using Shop.Models;

namespace Shop.Services.DbServices;

public class OrderService : IOrderService
{
	private readonly DbLabsContext _context;

	public OrderService(DbLabsContext context)
	{
		_context = context;
	}

	public async Task<IEnumerable<Order>?> GetListAsync()
	{
		return await _context.Orders.FromSqlRaw("select * from get_orders()")
			.Select(o => new Order
			{
				OrderId = o.OrderId,
				Price = o.Price,
				Client = o.Client,
				ClientId = o.ClientId,
				Date = o.Date,
				OrderItems = o.OrderItems.Select(oi => new OrderItem
				{
					OrderItemId = oi.OrderItemId,
					ProductQuantity = oi.ProductQuantity,
					ProductPrice = oi.ProductPrice,
					Product = new Product
					{
						ProductId = oi.Product.ProductId,
						Name = oi.Product.Name,
						Description = oi.Product.Description,
						Brand = oi.Product.Brand,
						ProductCategory = oi.Product.ProductCategory,
						ProductCategoryId = oi.Product.ProductCategoryId,
						ProductionDate = oi.Product.ProductionDate,
						Quantity = oi.Product.Quantity,
						Price = oi.Product.Price,
					}
				}).ToList(),
			}).ToListAsync();
	}

	public async Task<bool> AddAsync(Order order)
	{
		try
		{
			await _context.Database.ExecuteSqlInterpolatedAsync($"call insert_order({order.ClientId},{order.Price})");
			return true;
		}
		catch (Exception)
		{

		}
		return false;
	}
}
