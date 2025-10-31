using System;
using System.Collections.Generic;

namespace EmployeeManagement.DAL.Models;

public partial class PayrollHistory
{
    public int PayrollId { get; set; }

    public int EmployeeId { get; set; }

    public int PayrollMonth { get; set; }

    public int PayrollYear { get; set; }

    public decimal? BaseSalaryAtTime { get; set; }

    public decimal? Allowances { get; set; }

    public decimal? Bonuses { get; set; }

    public decimal? Deductions { get; set; }

    public decimal? TotalPay { get; set; }

    public DateOnly? PaymentDate { get; set; }

    public string? Notes { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
