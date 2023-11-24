using Shop.Models;
using Shop.Services.DbServices;

namespace Shop.Services.Authentication;

public class ShopAuthenticationService : IShopAuthenticationService
{
	private readonly IClientService _clientService;

	public ShopAuthenticationService(IClientService clientService)
	{
		_clientService = clientService;
	}

	public async Task<Employee?> AuthenticateEmployee(string email, string password)
	{
		throw new NotImplementedException();
	}

	public Task<Client?> AuthenticateClient(string email, string password)
	{
		return _clientService.VerifyAsync(email, password);
	}
}
