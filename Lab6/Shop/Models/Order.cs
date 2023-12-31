﻿namespace Shop.Models;

public partial class Order
{
	public int OrderId { get; set; }

	public int ClientId { get; set; }

	public DateTime Date { get; set; }

	public decimal Price { get; set; }

	public virtual Client? Client { get; set; }

	public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
