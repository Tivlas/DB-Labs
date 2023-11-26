using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Shop.Models;

namespace Shop.Services.DbServices;

public class EmployeeService : IEmployeeService
{
	private readonly DbLabsContext _context;
	private readonly ILogger<IEmployeeService> _logger;

	public EmployeeService(DbLabsContext context, ILogger<IEmployeeService> logger)
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

	public async Task<Employee?> LoginAsync(string email, string password)
	{
		Employee? emp = null;
		try
		{
			var emailParam = new NpgsqlParameter("p_email", email);
			var passwordParam = new NpgsqlParameter("p_password", password);

			var query = "SELECT * FROM get_employee_with_role(@p_email, @p_password)";

			emp = await _context.Employees
				.FromSqlRaw(query, emailParam, passwordParam)
				.Select(e => new Employee
				{
					EmployeeId = e.EmployeeId,
					FirstName = e.FirstName,
					LastName = e.LastName,
					Email = e.Email,
					Password = e.Password,
					Salary = e.Salary,
					Phone = e.Phone,
					EmployeeRole = e.EmployeeRole,
					EmployeeRoleId = e.EmployeeRoleId,
				})
				.FirstOrDefaultAsync();
		}
		catch (Exception e)
		{
			_logger.LogError(e.StackTrace + ": " + e.Message);
			return null;
		}
		return emp;
	}
}
