using System;
using System.Collections.Generic;

namespace EmployeeManagement.DAL.Models;

public partial class Position
{
    public int PositionId { get; set; }

    public string PositionTitle { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
