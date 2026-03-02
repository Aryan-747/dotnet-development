using System;
using System.Collections.Generic;

namespace StudentSystem.Models;

public partial class staffs
{
    public int staff_id { get; set; }

    public string first_name { get; set; } = null!;

    public string last_name { get; set; } = null!;

    public string email { get; set; } = null!;

    public string? phone { get; set; }

    public byte active { get; set; }

    public int store_id { get; set; }

    public int? manager_id { get; set; }

    public virtual ICollection<staffs> Inversemanager { get; set; } = new List<staffs>();

    public virtual staffs? manager { get; set; }

    public virtual ICollection<orders> orders { get; set; } = new List<orders>();

    public virtual stores store { get; set; } = null!;
}
