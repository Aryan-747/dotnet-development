using System;
using System.Collections.Generic;

namespace StudentSystem.Models;

public partial class stocks
{
    public int store_id { get; set; }

    public int product_id { get; set; }

    public int? quantity { get; set; }

    public virtual products product { get; set; } = null!;

    public virtual stores store { get; set; } = null!;
}
