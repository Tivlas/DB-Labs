using Shop.Models;

namespace Shop.Services.DbServices;
public interface IClientService
{
	Task<bool> AddClientAsync(Client client);
	Task<Client?> FindAsync(string email);
	Task<Client?> VerifyAsync(string email, string password);
}