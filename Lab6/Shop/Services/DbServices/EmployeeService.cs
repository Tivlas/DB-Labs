using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
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
			emp = await _context.Employees.FromSqlInterpolated($"SELECT e.employee_id, e.employee_role_id, e.first_name, e.last_name, e.salary, e.phone, e.position, e.password, e.email, r.name FROM employee AS e INNER JOIN employee_role AS r ON e.employee_role_id = r.employee_role_id WHERE e.email = {email} AND e.password = {password}")
			  .Include(e => e.EmployeeRole).FirstOrDefaultAsync();
		}
		catch (Exception e)
		{
			_logger.LogError(e.StackTrace + ": " + e.Message);
			return null;
		}
		return emp;
	}
}
