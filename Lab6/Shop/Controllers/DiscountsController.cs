using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shop.Models;
using Shop.Services.DbServices;

namespace Shop.Controllers
{
    public class DiscountsController : Controller
    {
		private readonly IDiscountService _discountService;

		public DiscountsController(IDiscountService discountService)
        {
			_discountService = discountService;
		}

        // GET: Discounts
        public async Task<IActionResult> Index()
        {
            var discounts = await _discountService.GetListAsync();
              return discounts != null ? 
                          View(discounts) :
                          Problem("No discounts");
        }

        // GET: Discounts/Details/5
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
        public IActionResult Create()
        {
            return View();
        }

        // POST: Discounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DiscountId,Description,Name,Percent,IsActive")] Discount discount)
        {
            if (ModelState.IsValid)
            {
                await _discountService.AddAsync(discount);
                return RedirectToAction(nameof(Index));
            }
            return View(discount);
        }

        // GET: Discounts/Edit/5
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
                if(await _discountService.UpdateAsync(discount) == false)
                {
                    return Problem("Update failed");
                }
                return RedirectToAction(nameof(Index));
            }
            return View(discount);
        }

        // GET: Discounts/Delete/5
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
