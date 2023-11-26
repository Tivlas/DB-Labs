using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Shop.Attributes;
using Shop.Models;
using Shop.Services.DbServices;

namespace Shop.Controllers
{
	public class CartController : Controller
	{
		private readonly ICartService _cartService;
		private readonly IClientService _clientService;
		private readonly IProductService _productService;
		private readonly IOrderService _orderService;

		public CartController(ICartService cartService, IClientService clientService,
			IProductService productService, IOrderService orderService)
		{
			_cartService = cartService;
			_clientService = clientService;
			_productService = productService;
			_orderService = orderService;
		}

		// GET: Cart
		[CustomAuthorize(Roles = "Client")]
		public async Task<IActionResult> Index()
		{
			var email = User.FindFirstValue(ClaimTypes.Email);
			if (email is null)
			{
				return Problem("Email is not in claims");
			}
			var clientId = await _clientService.GetIdByEmailAsync(email);
			if (clientId is null)
			{
				return Problem("No client with such email");
			}
			var cartId = await _cartService.GetCartIdByClientIdAsync(clientId.Value);
			if (cartId is null)
			{
				return Problem("No cart found for current client");
			}
			return View(await _cartService.GetItemListAsync(cartId.Value));
		}

		[HttpPost]
		public async Task<IActionResult> PlaceOrder(decimal cartPrice)
		{
			var email = User.FindFirstValue(ClaimTypes.Email);
			if (email is null)
			{
				return Problem("Email is not in claims");
			}
			var clientId = await _clientService.GetIdByEmailAsync(email);
			if (clientId is null)
			{
				return Problem("No client with such email");
			}

			if (await _orderService.AddAsync(new Order { ClientId = clientId.Value, Price = cartPrice }) == false)
			{
				return Problem("Add failed");
			}
			return Ok("Order placed successfully");
		}

		// GET: Cart/Details/5
		[CustomAuthorize(Roles = "Client")]
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var cartItem = await _cartService.GetItemByIdAsync(id.Value);
			if (cartItem == null)
			{
				return NotFound();
			}

			return View(cartItem);
		}

		// POST: Cart/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		[CustomAuthorize(Roles = "Client")]
		public async Task<IActionResult> Create(int productId, decimal productPrice, int productQuantity)
		{
			var email = User.FindFirstValue(ClaimTypes.Email);
			if (email is null)
			{
				return Problem("Email is not in claims");
			}
			var clientId = await _clientService.GetIdByEmailAsync(email);
			if (clientId is null)
			{
				return Problem("No client with such email");
			}
			var cartId = await _cartService.GetCartIdByClientIdAsync(clientId.Value);
			if (cartId is null)
			{
				return Problem("No cart found for current client");
			}

			if (await _cartService.AddItemAsync(new CartItem()
			{
				CartId = cartId.Value,
				ProductId = productId,
				ProductPrice = productPrice,
				ProductQuantity = productQuantity
			}) == false)
			{
				return Problem("Add failed!");
			}
			return RedirectToAction(nameof(Index));
		}

		// GET: Cart/Edit/5
		[CustomAuthorize(Roles = "Client")]
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var cartItem = await _cartService.GetItemByIdAsync(id.Value);
			if (cartItem == null)
			{
				return NotFound();
			}
			return View(cartItem);
		}

		// POST: Cart/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		[CustomAuthorize(Roles = "Client")]
		public async Task<IActionResult> Edit(int id, [Bind("CartItemId, ProductQuantity")] CartItem cartItem)
		{
			if (id != cartItem.CartItemId)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				if (await _cartService.UpdateItemAsync(cartItem) == false)
				{
					return Problem("Update failed!");
				}
				return RedirectToAction(nameof(Index));
			}
			return View(cartItem);
		}

		// GET: Cart/Delete/5
		[CustomAuthorize(Roles = "Client")]
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var cartItem = await _cartService.GetItemByIdAsync(id.Value);
			if (cartItem == null)
			{
				return NotFound();
			}

			return View(cartItem);
		}

		// POST: Cart/Delete/5
		[CustomAuthorize(Roles = "Client")]
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (await _cartService.DeleteItemAsync(id) == false)
			{
				return Problem("Delete failed!");
			}
			return RedirectToAction(nameof(Index));
		}
	}
}
