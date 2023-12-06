using Shop.Models;

namespace Shop.Services.DbServices;
public interface IRoleService
{
	Task<IEnumerable<EmployeeRole>?> GetRolesListAsync();
}