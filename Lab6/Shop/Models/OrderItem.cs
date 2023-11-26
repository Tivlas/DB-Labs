using System;
using System.Collections.Generic;

namespace Shop.Models;

public partial class OrderItem
{
    public int OrderItemId { get; set; }

    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public int ProductQuantity { get; set; }

    public decimal ProductPrice { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Product? Product { get; set; }
}
