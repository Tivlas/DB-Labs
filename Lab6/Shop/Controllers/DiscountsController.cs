using Microsoft.AspNetCore.Mvc;
using Shop.Attributes;
using Shop.Models;
using Shop.Services.DbServices;

namespace Shop.Controllers
{
	public class DiscountsController : Controller
	{
		private readonly IDiscountService _discountService;
		private readonly IProductService _productService;

		public DiscountsController(IDiscountService discountService, IProductService productService)
		{
			_discountService = discountService;
			_productService = productService;
		}

		// GET: Discounts
		public async Task<IActionResult> Index()
		{
			var discounts = await _discountService.GetListAsync();
			return View(discounts);
		}

		// GET: Discounts/Details/5
		[CustomAuthorize(Roles = "Admin, DiscountManager")]
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var discount = await _discountService.GetByIdAsync(id.Value);
			if (discount == null)
			{
				return NotFound();
			}

			return View(discount);
		}

		// GET: Discounts/Create
		[CustomAuthorize(Roles = "Admin, DiscountManager")]
		public async Task<IActionResult> Create()
		{
			var products = await _productService.GetListAsync();
			ViewData["Products"] = products;
			return View();
		}

		// POST: Discounts/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[CustomAuthorize(Roles = "Admin, DiscountManager")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("DiscountId,Description,Name,Percent,IsActive")] Discount discount,
			List<int> selectedProductIds)
		{
			if (ModelState.IsValid && selectedProductIds.Count > 0)
			{
				await _discountService.AddAsync(discount, selectedProductIds);
				return RedirectToAction(nameof(Index));
			}
			return View(discount);
		}

		// GET: Discounts/Edit/5
		[CustomAuthorize(Roles = "Admin, DiscountManager")]
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var discount = await _discountService.GetByIdAsync(id.Value);
			if (discount == null)
			{
				return NotFound();
			}
			return View(discount);
		}

		// POST: Discounts/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[CustomAuthorize(Roles = "Admin, DiscountManager")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("DiscountId,Description,Name,Percent,IsActive")] Discount discount)
		{
			if (id != discount.DiscountId)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				if (await _discountService.UpdateAsync(discount) == false)
				{
					return Problem("Update failed");
				}
				return RedirectToAction(nameof(Index));
			}
			return View(discount);
		}

		// GET: Discounts/Delete/5
		[CustomAuthorize(Roles = "Admin, DiscountManager")]
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var discount = await _discountService.GetByIdAsync(id.Value);
			if (discount == null)
			{
				return NotFound();
			}

			return View(discount);
		}

		// POST: Discounts/Delete/5
		[CustomAuthorize(Roles = "Admin, DiscountManager")]
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (await _discountService.DeleteAsync(id) == false)
			{
				return Problem("Update failed");
			}
			return RedirectToAction(nameof(Index));
		}
	}
}
