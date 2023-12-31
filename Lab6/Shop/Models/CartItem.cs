﻿namespace Shop.Models;

public partial class CartItem
{
	public int CartItemId { get; set; }

	public int CartId { get; set; }

	public int ProductId { get; set; }

	public int ProductQuantity { get; set; }

	public decimal ProductPrice { get; set; }

	public virtual Cart? Cart { get; set; }

	public virtual Product? Product { get; set; }
}
