using Microsoft.AspNetCore.Mvc;
using Shop.Attributes;
using Shop.Models;
using Shop.Services.DbServices;

namespace Shop.Controllers
{
	public class ProductCategoriesController : Controller
	{
		private readonly ICategoryService _categoryService;

		public ProductCategoriesController(ICategoryService categoryService)
		{
			_categoryService = categoryService;
		}

		// GET: ProductCategories
		[CustomAuthorize(Roles = "Admin, ProductManager")]
		public async Task<IActionResult> Index()
		{
			var categories = await _categoryService.GetListAsync();
			return categories != null ? View(categories) : Problem("No categories.");
		}

		// GET: ProductCategories/Details/5
		[CustomAuthorize(Roles = "Admin, ProductManager")]
		public async Task<IActionResult> Details(int? id)
		{
			if (id is null)
			{
				return NotFound();
			}

			var productCategory = await _categoryService.GetByIdAsync(id.Value);
			if (productCategory == null)
			{
				return NotFound();
			}

			return View(productCategory);
		}

		// GET: ProductCategories/Create
		[CustomAuthorize(Roles = "Admin, ProductManager")]
		public IActionResult Create()
		{
			return View();
		}

		// POST: ProductCategories/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[CustomAuthorize(Roles = "Admin, ProductManager")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("ProductCategoryId,Name")] ProductCategory productCategory)
		{
			if (ModelState.IsValid)
			{
				await _categoryService.AddAsync(productCategory.Name);
				return RedirectToAction(nameof(Index));
			}
			return View(productCategory);
		}

		// GET: ProductCategories/Edit/5
		[CustomAuthorize(Roles = "Admin, ProductManager")]
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var productCategory = await _categoryService.GetByIdAsync(id.Value);
			if (productCategory == null)
			{
				return NotFound();
			}
			return View(productCategory);
		}

		// POST: ProductCategories/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[CustomAuthorize(Roles = "Admin, ProductManager")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("ProductCategoryId,Name")] ProductCategory productCategory)
		{
			if (id != productCategory.ProductCategoryId)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				if (await _categoryService.UpdateAsync(id, productCategory.Name) == false)
				{
					return Problem("Update failed");
				}
				return RedirectToAction(nameof(Index));
			}
			return View(productCategory);
		}

		// GET: ProductCategories/Delete/5
		[CustomAuthorize(Roles = "Admin, ProductManager")]
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var productCategory = await _categoryService.GetByIdAsync(id.Value);
			if (productCategory == null)
			{
				return NotFound();
			}

			return View(productCategory);
		}

		// POST: ProductCategories/Delete/5
		[CustomAuthorize(Roles = "Admin, ProductManager")]
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (await _categoryService.DeleteAsync(id) == false)
			{
				return Problem("Delete failed");
			}
			return RedirectToAction(nameof(Index));
		}
	}
}
