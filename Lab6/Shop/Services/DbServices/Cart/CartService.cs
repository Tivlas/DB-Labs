using Microsoft.EntityFrameworkCore;
using Shop.Models;

namespace Shop.Services.DbServices;

public class CartService : ICartService
{
	private readonly DbLabsContext _context;
	private readonly ILogger<CartService> _logger;

	public CartService(DbLabsContext context, ILogger<CartService> logger)
	{
		_context = context;
		_logger = logger;
	}



	public async Task<IEnumerable<CartItem>?> GetItemListAsync(int cartId)
	{
		return await _context.CartItems.FromSqlInterpolated($"select * from get_cart_items_by_cart_id({cartId})")
			.Select(ci => new CartItem
			{
				CartItemId = ci.CartItemId,
				CartId = ci.CartId,
				Cart = ci.Cart,
				ProductId = ci.ProductId,
				Product = ci.Product,
				ProductQuantity = ci.ProductQuantity,
				ProductPrice = ci.ProductPrice,
			}).ToListAsync();
	}

	public async Task<CartItem?> GetItemByIdAsync(int id)
	{
		return await _context.CartItems.FromSqlInterpolated($"select * from get_cart_item_by_id({id})")
			.Select(ci => new CartItem
			{
				CartItemId = ci.CartItemId,
				CartId = ci.CartId,
				Cart = ci.Cart,
				ProductId = ci.ProductId,
				Product = ci.Product,
				ProductQuantity = ci.ProductQuantity,
				ProductPrice = ci.ProductPrice,
			}).FirstOrDefaultAsync();
	}

	public async Task<bool> AddItemAsync(CartItem ci)
	{
		try
		{
			await _context.Database.ExecuteSqlInterpolatedAsync($"call insert_cart_item({ci.CartId},{ci.ProductId},{ci.ProductQuantity},{ci.ProductPrice})");
			return true;
		}
		catch (Exception e)
		{
			_logger.LogError(e.StackTrace + ": " + e.Message);
		}
		return false;
	}

	public async Task<bool> UpdateItemAsync(CartItem ci)
	{
		try
		{
			await _context.Database.ExecuteSqlInterpolatedAsync($"call update_cart_item_product_quantity({ci.CartItemId},{ci.ProductQuantity})");
			return true;
		}
		catch (Exception e)
		{
			_logger.LogError(e.StackTrace + ": " + e.Message);
		}
		return false;
	}

	public async Task<bool> DeleteItemAsync(int id)
	{
		try
		{
			await _context.Database.ExecuteSqlInterpolatedAsync($"call delete_cart_item_by_id({id})");
			return true;
		}
		catch (Exception e)
		{
			_logger.LogError(e.StackTrace + ": " + e.Message);
		}
		return false;
	}

	public async Task<int?> GetCartIdByClientIdAsync(int clientId)
	{
		var cart = await _context.Carts.FromSqlInterpolated($"select * from cart where client_id = {clientId}").FirstOrDefaultAsync();
		return cart?.CartId;
	}
}
