using Microsoft.AspNetCore.Mvc;
using Shop.Models;
using Shop.Services.DbServices;

namespace Shop.Controllers;
public class SignupController : Controller
{
	private readonly IClientService _clientService;
	private readonly IEmployeeService _employeeService;
	private readonly IRoleService _roleService;
	private List<EmployeeRole>? _employeeRoles = null;

	public SignupController(IClientService clientService, IEmployeeService employeeService, IRoleService roleService)
	{
		_clientService = clientService;
		_employeeService = employeeService;
		_roleService = roleService;
	}

	private async Task LoadRolesAsync()
	{
		if (_employeeRoles == null)
		{
			_employeeRoles = await _roleService.GetRolesListAsync();
		}
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
			TempData["ErrorMessage"] = "Invalid form data!";
			return View(client);
		}
		return RedirectToAction("Index", "Home");
	}

	public async Task<IActionResult> SignupEmployee(Employee employee)
	{
		await LoadRolesAsync();
		if (ModelState.IsValid)
		{
			var success = await _employeeService.AddEmployeeAsync(employee);
			if (!success)
			{
				TempData["Roles"] = _employeeRoles;
				TempData["ErrorMessage"] = "Invalid form data!";
				return View(employee);
			}
		}
		else
		{
			TempData["Roles"] = _employeeRoles;
			TempData["ErrorMessage"] = "Invalid form data!";
			return View(employee);
		}
		return RedirectToAction("Index", "Home");
	}
}
