using System;
using System.Collections.Generic;

namespace EmployeeManagement.DAL.Models;

public partial class NotificationReadStatus
{
    public int NotificationId { get; set; }

    public int EmployeeId { get; set; }

    public bool? IsRead { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual Notification Notification { get; set; } = null!;
}
