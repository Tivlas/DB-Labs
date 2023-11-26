using Microsoft.EntityFrameworkCore;
using Shop.Models;

namespace Shop.Services.DbServices;

public class ClientService : IClientService
{
	private readonly DbLabsContext _context;
	private readonly ILogger<IClientService> _logger;

	public ClientService(DbLabsContext context, ILogger<IClientService> logger)
	{
		_context = context;
		_logger = logger;
	}

	public async Task<bool> AddClientAsync(Client client)
	{
		if (client == null)
		{
			throw new ArgumentNullException(nameof(client));
		}
		try
		{
			if (await FindAsync(client.Email) is null)
				await _context.Database.ExecuteSqlInterpolatedAsync($"CALL insert_client({client.FirstName}, {client.LastName}, {client.Phone}, {client.Password}, {client.Email});");
			return true;
		}
		catch (Exception e)
		{
			_logger.LogError(e.StackTrace + ": " + e.Message);
		}
		return false;
	}

	public async Task<Client?> FindAsync(string email)
	{
		Client? c = null;
		try
		{
			c = await _context.Clients.FromSqlInterpolated($"SELECT * FROM client WHERE email = {email}")
			   .AsNoTracking().FirstOrDefaultAsync();
		}
		catch (Exception e)
		{
			_logger.LogError(e.StackTrace + ": " + e.Message);
			return null;
		}
		return c;
	}

	public async Task<Client?> LoginAsync(string email, string password)
	{
		Client? c = null;
		try
		{
			c = await _context.Clients.FromSqlInterpolated($"SELECT * FROM client WHERE email = {email} AND password = {password}")
			   .AsNoTracking().FirstOrDefaultAsync();
		}
		catch (Exception e)
		{
			_logger.LogError(e.StackTrace + ": " + e.Message);
			return null;
		}
		return c;
	}

	public async Task<int?> GetIdByEmailAsync(string email)
	{
		var client = await _context.Clients.FromSqlInterpolated($"select * from client where email = {email}").FirstOrDefaultAsync();
		return client?.ClientId;
	}
}
