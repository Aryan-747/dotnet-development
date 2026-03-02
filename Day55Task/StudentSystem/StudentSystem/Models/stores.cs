using System;
using System.Collections.Generic;

namespace StudentSystem.Models;

public partial class stores
{
    public int store_id { get; set; }

    public string store_name { get; set; } = null!;

    public string? phone { get; set; }

    public string? email { get; set; }

    public string? street { get; set; }

    public string? city { get; set; }

    public string? state { get; set; }

    public string? zip_code { get; set; }

    public virtual ICollection<orders> orders { get; set; } = new List<orders>();

    public virtual ICollection<staffs> staffs { get; set; } = new List<staffs>();

    public virtual ICollection<stocks> stocks { get; set; } = new List<stocks>();
}
