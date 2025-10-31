using System;
using System.Collections.Generic;

namespace EmployeeManagement.DAL.Models;

public partial class Notification
{
    public int NotificationId { get; set; }

    public string Title { get; set; } = null!;

    public string MessageBody { get; set; } = null!;

    public DateTime? SentDate { get; set; }

    public int? SenderAdminId { get; set; }

    public int? TargetDepartmentId { get; set; }

    public virtual ICollection<NotificationReadStatus> NotificationReadStatuses { get; set; } = new List<NotificationReadStatus>();

    public virtual Admin? SenderAdmin { get; set; }

    public virtual Department? TargetDepartment { get; set; }
}
