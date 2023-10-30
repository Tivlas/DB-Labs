using System;
using System.Collections.Generic;

namespace Shop.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int ClientId { get; set; }

    public DateTime Date { get; set; }

    public decimal Price { get; set; }

    public virtual Client Client { get; set; } = null!;

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
