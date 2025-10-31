using System;
using System.Collections.Generic;

namespace EmployeeManagement.DAL.Models;

public partial class Attendance
{
    public long AttendanceId { get; set; }

    public int EmployeeId { get; set; }

    public DateOnly AttendanceDate { get; set; }

    public TimeOnly? ClockInTime { get; set; }

    public TimeOnly? ClockOutTime { get; set; }

    public decimal? OvertimeHours { get; set; }

    public string? Status { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
