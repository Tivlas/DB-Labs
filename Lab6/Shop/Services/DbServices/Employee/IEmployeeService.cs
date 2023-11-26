using Shop.Models;

namespace Shop.Services.DbServices;
public interface IEmployeeService
{
	Task<bool> AddEmployeeAsync(Employee Employee);
	Task<Employee?> FindAsync(string email);
	Task<Employee?> LoginAsync(string email, string password);
}