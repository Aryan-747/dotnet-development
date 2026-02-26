using System;
using System.Collections.Generic;

namespace DbFirstDemo.Models;

public partial class Employees
{
    public int EmployeeId { get; set; }

    public string FullName { get; set; } = null!;

    public string Department { get; set; } = null!;

    public decimal Salary { get; set; }
}
