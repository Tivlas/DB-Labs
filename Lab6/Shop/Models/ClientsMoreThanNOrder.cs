using System;
using System.Collections.Generic;

namespace Shop.Models;

public partial class ClientsMoreThanNOrder
{
    public string? Name { get; set; }

    public string? Email { get; set; }

    public long? NumberOfOrders { get; set; }

    public decimal? Total { get; set; }
}
