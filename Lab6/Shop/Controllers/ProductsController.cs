using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shop.Attributes;
using Shop.Models;
using Shop.Services.DbServices;

namespace Shop.Controllers
{
	public class ProductsController : Controller
	{
		private readonly IProductService _productService;
		private readonly ICategoryService _categoryService;

		public ProductsController(IProductService productService, ICategoryService categoryService)
		{
			_productService = productService;
			_categoryService = categoryService;
		}

		// GET: Products
		public async Task<IActionResult> Index()
		{
			var products = await _productService.GetListAsync();
			return products is null ? Problem("No products") : View(products);
		}

		// GET: Products/Details/5
		[CustomAuthorize(Roles = "Admin,ProductManager")]
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var product = await _productService.GetByIdAsync(id.Value);
			if (product == null)
			{
				return NotFound();
			}

			return View(product);
		}

		// GET: Products/Create
		[CustomAuthorize(Roles = "Admin,ProductManager")]
		public async Task<IActionResult> Create()
		{
			ViewData["ProductCategoryId"] = new SelectList(await _categoryService.GetListAsync(), "ProductCategoryId", "Name");
			return View();
		}

		// POST: Products/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[CustomAuthorize(Roles = "Admin,ProductManager")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("ProductId,ProductCategoryId,Price,Name,ProductionDate,Quantity,Brand,Description")] Product product)
		{
			if (ModelState.IsValid)
			{
				await _productService.AddAsync(product);
				return RedirectToAction(nameof(Index));
			}
			ViewData["ProductCategoryId"] = new SelectList(await _categoryService.GetListAsync(), "ProductCategoryId", "Name", product.ProductCategoryId);
			return View(product);
		}

		// GET: Products/Edit/5
		[CustomAuthorize(Roles = "Admin,ProductManager")]
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var product = await _productService.GetByIdAsync(id.Value);
			if (product == null)
			{
				return NotFound();
			}
			ViewData["ProductCategoryId"] = new SelectList(await _categoryService.GetListAsync(), "ProductCategoryId", "Name", product.ProductCategoryId);
			return View(product);
		}

		// POST: Products/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[CustomAuthorize(Roles = "Admin,ProductManager")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductCategoryId,Price,Name,ProductionDate,Quantity,Brand,Description")] Product product)
		{
			if (id != product.ProductId)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				if (await _productService.UpdateAsync(product) == false)
				{
					return Problem("Update failed!");
				}
				return RedirectToAction(nameof(Index));
			}
			ViewData["ProductCategoryId"] = new SelectList(await _categoryService.GetListAsync(), "ProductCategoryId", "Name", product.ProductCategoryId);
			return View(product);
		}

		// GET: Products/Delete/5
		[CustomAuthorize(Roles = "Admin,ProductManager")]
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var product = await _productService.GetByIdAsync(id.Value);
			if (product == null)
			{
				return NotFound();
			}

			return View(product);
		}

		// POST: Products/Delete/5
		[CustomAuthorize(Roles = "Admin,ProductManager")]
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (await _productService.DeleteAsync(id) == false)
			{
				return Problem("Delete failed!");
			}
			return RedirectToAction(nameof(Index));
		}
	}
}
