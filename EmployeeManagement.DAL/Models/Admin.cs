using System;
using System.Collections.Generic;

namespace EmployeeManagement.DAL.Models;

public partial class Admin
{
    public int AdminId { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public bool? IsActive { get; set; }

    public virtual ICollection<ActivityLog> ActivityLogs { get; set; } = new List<ActivityLog>();

    public virtual ICollection<LeaveRequest> LeaveRequests { get; set; } = new List<LeaveRequest>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}
