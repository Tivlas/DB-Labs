using Microsoft.EntityFrameworkCore;
using Shop.Models;

namespace Shop.Services.DbServices;

public class RoleService : IRoleService
{
	private readonly DbLabsContext _context;

	public RoleService(DbLabsContext context)
	{
		_context = context;
	}

	public async Task<List<EmployeeRole>> GetRolesListAsync()
	{
		return await _context.EmployeeRoles.FromSqlRaw("SELECT * from employee_role").ToListAsync();
	}
}
