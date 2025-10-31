using System;
using System.Collections.Generic;

namespace EmployeeManagement.DAL.Models;

public partial class LeaveRequest
{
    public int RequestId { get; set; }

    public int EmployeeId { get; set; }

    public int LeaveTypeId { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public decimal? NumberOfDays { get; set; }

    public string? Reason { get; set; }

    public string? Status { get; set; }

    public int? ApprovedByAdminId { get; set; }

    public DateTime? ApprovedDate { get; set; }

    public virtual Admin? ApprovedByAdmin { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual LeaveType LeaveType { get; set; } = null!;
}
