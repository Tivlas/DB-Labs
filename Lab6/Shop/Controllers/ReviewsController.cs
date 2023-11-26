using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shop.Attributes;
using Shop.Models;
using Shop.Services.DbServices;

namespace Shop.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly IReviewService _reviewService;
        private readonly IClientService _clientService;

        public ReviewsController(IReviewService reviewService, IClientService clientService)
        {
            _reviewService = reviewService;
            _clientService = clientService;
        }

        // GET: Reviews
        [HttpGet]
        public async Task<IActionResult> Index(int? productId)
        {
            IEnumerable<Review>? reviews = null;
            reviews = productId.HasValue ? await _reviewService.GetListByproductIdAsync(productId.Value)
                : await _reviewService.GetListAsync();
            return View(reviews);
        }

        // GET: Reviews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _reviewService.GetByIdAsync(id.Value);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // GET: Reviews/Create
        [CustomAuthorize(Roles = "Client")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [CustomAuthorize(Roles = "Client")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,Content,Rating")] Review review)
        {
            if (ModelState.IsValid)
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
                review.ClientId = clientId.Value;
				if (await _reviewService.AddAsync(review) == false)
                {
                    return Problem("Add failed");
                }
                return RedirectToAction(nameof(Index));
            }
			return BadRequest("Request can be created only if u came here from products page.");
		}

        // GET: Reviews/Edit/5
        [CustomAuthorize(Roles = "Client,Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

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
            var review = await _reviewService.GetByIdAsync(id.Value);
            if (review == null)
            {
                return NotFound();
            }
            if (review.ClientId != clientId)
            {
                return Forbid();
            }
            return View(review);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [CustomAuthorize(Roles = "Client,Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReviewId,ProductId,ClientId,Content,Rating,Date")] Review review)
        {
            if (id != review.ReviewId)
            {
                return NotFound();
            }

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
            if (review.ClientId != clientId)
            {
                return Forbid();
            }

            if (ModelState.IsValid)
            {
                if (await _reviewService.UpdateAsync(review) == false)
                {
                    return Problem("Update failed");
                }
                return RedirectToAction(nameof(Index));
            }
            return View(review);
        }

        // GET: Reviews/Delete/5
        [CustomAuthorize(Roles = "Client,Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _reviewService.GetByIdAsync(id.Value);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // POST: Reviews/Delete/5
        [CustomAuthorize(Roles = "Client,Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
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
            var review = await _reviewService.GetByIdAsync(id);
            if (review == null)
            {
                return NotFound();
            }
            if (review.ClientId != clientId)
            {
                return Forbid();
            }
            if (await _reviewService.DeleteAsync(new Review { ReviewId = id }) == false)
            {
                return Problem("Delete failed");
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
