using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Models;

namespace Shop.Controllers;
public class HomeController : Controller
{
	private readonly ILogger<HomeController> _logger;
	private readonly DbLabsContext _context;

	public HomeController(ILogger<HomeController> logger, DbLabsContext context)
	{
		_logger = logger;
		_context = context;
	}

	public async Task<IActionResult> Index()
	{
		var products = await _context.Products.FromSql($"SELECT product_id, name, price FROM product").Select(p => new
		{
			p.ProductId,
			p.Name,
			p.Price
		}).ToListAsync();
		return View(products);
	}

	public IActionResult Privacy()
	{
		return View();
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}
}
