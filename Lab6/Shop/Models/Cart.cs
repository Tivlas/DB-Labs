namespace Shop.Models;

public partial class Cart
{
	public int CartId { get; set; }

	public int ClientId { get; set; }

	public decimal Price { get; set; }

	public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

	public virtual Client Client { get; set; } = null!;
}
