using System;
using System.Collections.Generic;

namespace StudentSystem.Models;

public partial class products
{
    public int product_id { get; set; }

    public string product_name { get; set; } = null!;

    public int brand_id { get; set; }

    public int category_id { get; set; }

    public short model_year { get; set; }

    public decimal list_price { get; set; }

    public virtual brands brand { get; set; } = null!;

    public virtual categories category { get; set; } = null!;

    public virtual ICollection<order_items> order_items { get; set; } = new List<order_items>();

    public virtual ICollection<stocks> stocks { get; set; } = new List<stocks>();
}
