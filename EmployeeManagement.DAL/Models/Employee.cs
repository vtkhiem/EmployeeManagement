using System;
using System.Collections.Generic;

namespace EmployeeManagement.DAL.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string FullName { get; set; } = null!;

    public DateOnly? DateOfBirth { get; set; }

    public string? Gender { get; set; }

    public string? Address { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public string? ProfilePicturePath { get; set; }

    public DateOnly HireDate { get; set; }

    public string? EmploymentStatus { get; set; }

    public decimal? AnnualLeaveDaysRemaining { get; set; }

    public decimal? SickLeaveDaysRemaining { get; set; }

    public decimal? BaseSalary { get; set; }

    public int? DepartmentId { get; set; }

    public int? PositionId { get; set; }

    public string? PasswordHash { get; set; }

    public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

    public virtual Department? Department { get; set; }

    public virtual ICollection<LeaveRequest> LeaveRequests { get; set; } = new List<LeaveRequest>();

    public virtual ICollection<NotificationReadStatus> NotificationReadStatuses { get; set; } = new List<NotificationReadStatus>();

    public virtual ICollection<PayrollHistory> PayrollHistories { get; set; } = new List<PayrollHistory>();

    public virtual Position? Position { get; set; }
}
