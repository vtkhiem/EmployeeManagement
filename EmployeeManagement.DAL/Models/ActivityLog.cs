using System;
using System.Collections.Generic;

namespace EmployeeManagement.DAL.Models;

public partial class ActivityLog
{
    public long LogId { get; set; }

    public int AccountId { get; set; }

    public DateTime? Timestamp { get; set; }

    public string? ActionType { get; set; }

    public string? Details { get; set; }

    public virtual Admin Account { get; set; } = null!;
}
