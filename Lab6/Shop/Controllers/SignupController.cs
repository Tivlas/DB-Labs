using Microsoft.AspNetCore.Mvc;
using Shop.Models;
using Shop.Services.DbServices;

namespace Shop.Controllers;
public class SignupController : Controller
{
    private readonly IClientService _clientService;

    public SignupController(IClientService clientService)
    {
        _clientService = clientService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> Signup(Client client)
    {
        if (ModelState.IsValid)
        {
           var success = await _clientService.AddClientAsync(client);
            if (!success)
            {
                TempData["ErrorMessage"] = "Invalid form data!";
                return View(client);
            }
        }
        else
        {
            return View(client);
        }
        return RedirectToAction("Index", "Home");
    }
}
