using System;
using System.Collections.Generic;

namespace StudentSystem.Models;

public partial class brands
{
    public int brand_id { get; set; }

    public string brand_name { get; set; } = null!;

    public virtual ICollection<products> products { get; set; } = new List<products>();
}
