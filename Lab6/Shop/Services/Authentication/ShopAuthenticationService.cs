using Shop.Models;
using Shop.Services.DbServices;

namespace Shop.Services.Authentication;

public class ShopAuthenticationService : IShopAuthenticationService
{
	private readonly IClientService _clientService;
	private readonly IEmployeeService _employeeService;

	public ShopAuthenticationService(IClientService clientService, IEmployeeService employeeService)
	{
		_clientService = clientService;
		_employeeService = employeeService;
	}

	public Task<Employee?> AuthenticateEmployee(string email, string password)
	{
		return _employeeService.LoginAsync(email, password);
	}

	public Task<Client?> AuthenticateClient(string email, string password)
	{
		return _clientService.LoginAsync(email, password);
	}
}
