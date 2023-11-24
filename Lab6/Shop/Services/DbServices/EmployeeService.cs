using Microsoft.EntityFrameworkCore;
using Shop.Models;

namespace Shop.Services.DbServices;

public class EmployeeService
{
	private readonly DbLabsContext _context;
	private readonly ILogger<EmployeeService> _logger;

	public EmployeeService(DbLabsContext context, ILogger<EmployeeService> logger)
	{
		_context = context;
		_logger = logger;
	}

	public async Task<bool> AddEmployeeAsync(Employee Employee)
	{
		if (Employee == null)
		{
			throw new ArgumentNullException(nameof(Employee));
		}
		try
		{
			if (await FindAsync(Employee.Email) is null)
				await _context.Database.ExecuteSqlInterpolatedAsync($@"CALL insert_employee({Employee.EmployeeRoleId},{Employee.FirstName},{Employee.LastName},{Employee.Salary},{Employee.Phone},{Employee.Position},{Employee.Password},{Employee.Email});");
			return true;
		}
		catch (Exception e)
		{
			_logger.LogError(e.StackTrace + ": " + e.Message);
		}
		return false;
	}

	public async Task<Employee?> FindAsync(string email)
	{
		Employee? c = null;
		try
		{
			c = await _context.Employees.FromSqlInterpolated($"SELECT * FROM Employee WHERE email = {email}")
			   .AsNoTracking().FirstOrDefaultAsync();
		}
		catch (Exception e)
		{
			_logger.LogError(e.StackTrace + ": " + e.Message);
			return null;
		}
		return c;
	}

	public async Task<Employee?> VerifyAsync(string email, string password)
	{
		Employee? c = null;
		try
		{
			c = await _context.Employees.FromSqlInterpolated($"SELECT * FROM Employee WHERE email = {email} AND password = {password}")
			   .AsNoTracking().FirstOrDefaultAsync();
		}
		catch (Exception e)
		{
			_logger.LogError(e.StackTrace + ": " + e.Message);
			return null;
		}
		return c;
	}
}
