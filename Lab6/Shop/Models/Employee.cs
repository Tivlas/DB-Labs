namespace Shop.Models;

public partial class Employee
{
	public int EmployeeId { get; set; }

	public int EmployeeRoleId { get; set; }

	public string FirstName { get; set; } = null!;

	public string LastName { get; set; } = null!;

	public decimal Salary { get; set; }

	public string Phone { get; set; } = null!;

	public string Position { get; set; } = null!;

	public string Password { get; set; } = null!;

	public string Email { get; set; } = null!;

	public virtual EmployeeRole? EmployeeRole { get; set; }
}
