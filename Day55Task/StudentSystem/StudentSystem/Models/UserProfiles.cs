using System;
using System.Collections.Generic;

namespace StudentSystem.Models;

public partial class UserProfiles
{
    public Guid UserID { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public DateTime? CreatedAt { get; set; }
}
