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
    public class OrdersController : Controller
    {
		private readonly IOrderService _orderService;

		public OrdersController(IOrderService orderService)
        {
			_orderService = orderService;
		}

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var orders = await _orderService.GetListAsync();
            return View(orders);
        }
    }
}
