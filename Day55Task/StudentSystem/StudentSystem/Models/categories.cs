using System;
using System.Collections.Generic;

namespace StudentSystem.Models;

public partial class categories
{
    public int category_id { get; set; }

    public string category_name { get; set; } = null!;

    public virtual ICollection<products> products { get; set; } = new List<products>();
}
