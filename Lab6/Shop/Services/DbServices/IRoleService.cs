using Shop.Models;

namespace Shop.Services.DbServices;
public interface IRoleService
{
	Task<List<EmployeeRole>> GetRolesListAsync();
}