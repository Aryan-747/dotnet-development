using System;
using System.Collections.Generic;

namespace StudentSystem.Models;

public partial class tblLog
{
    public int StudentId { get; set; }

    public int LogId { get; set; }

    public string? Info { get; set; }
}
