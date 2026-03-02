using System;
using System.Collections.Generic;

namespace StudentSystem.Models;

public partial class order_items
{
    public int order_id { get; set; }

    public int item_id { get; set; }

    public int product_id { get; set; }

    public int quantity { get; set; }

    public decimal list_price { get; set; }

    public decimal discount { get; set; }

    public virtual orders order { get; set; } = null!;

    public virtual products product { get; set; } = null!;
}
