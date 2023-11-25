using System;
using System.Collections.Generic;

namespace Shop.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public int ProductCategoryId { get; set; }

    public decimal Price { get; set; }

    public string Name { get; set; } = null!;

    public DateOnly ProductionDate { get; set; }

    public int Quantity { get; set; }

    public string Brand { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual ProductCategory? ProductCategory { get; set; }

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<Discount> Discounts { get; set; } = new List<Discount>();
}
