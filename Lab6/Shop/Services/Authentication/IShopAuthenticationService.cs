using Shop.Models;

namespace Shop.Services.Authentication;

public interface IShopAuthenticationService
{
	Task<Client?> AuthenticateClient(string email, string password);
	Task<Employee?> AuthenticateEmployee(string email, string password);
}