namespace Shop.Models;

public partial class ClientsOrder
{
	public string? ClientName { get; set; }

	public string? Email { get; set; }

	public DateTime? OrderDate { get; set; }

	public decimal? Total { get; set; }

	public string? Name { get; set; }

	public int? ProductQuantity { get; set; }
}
