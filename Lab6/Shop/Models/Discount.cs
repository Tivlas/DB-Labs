using System;
using System.Collections.Generic;

namespace Shop.Models;

public partial class Discount
{
    public int DiscountId { get; set; }

    public string Description { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int Percent { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
