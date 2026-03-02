using System;
using System.Collections.Generic;

namespace StudentSystem.Models;

public partial class orders
{
    public int order_id { get; set; }

    public int? customer_id { get; set; }

    public byte order_status { get; set; }

    public DateOnly order_date { get; set; }

    public DateOnly required_date { get; set; }

    public DateOnly? shipped_date { get; set; }

    public int store_id { get; set; }

    public int staff_id { get; set; }

    public virtual customers? customer { get; set; }

    public virtual ICollection<order_items> order_items { get; set; } = new List<order_items>();

    public virtual staffs staff { get; set; } = null!;

    public virtual stores store { get; set; } = null!;
}
