using System;
using System.Collections.Generic;

namespace Shop.Models;

public partial class ProductsActiveDiscount
{
    public string? Category { get; set; }

    public string? Name { get; set; }

    public decimal? Price { get; set; }

    public DateOnly? ProductionDate { get; set; }

    public int? Quantity { get; set; }

    public string? Brand { get; set; }

    public string? Description { get; set; }

    public string? Discount { get; set; }

    public string? Profit { get; set; }

    public int? Percent { get; set; }
}
