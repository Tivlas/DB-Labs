﻿namespace Shop.Models;

public partial class EmployeeRole
{
	public int EmployeeRoleId { get; set; }

	public string Name { get; set; } = null!;

	public virtual ICollection<Employee>? Employees { get; set; } = null;
}
