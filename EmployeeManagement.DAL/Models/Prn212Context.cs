using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EmployeeManagement.DAL.Models;

public partial class Prn212Context : DbContext
{
    public Prn212Context()
    {
    }

    public Prn212Context(DbContextOptions<Prn212Context> options)
        : base(options)
    {
    }

    public virtual DbSet<ActivityLog> ActivityLogs { get; set; }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Attendance> Attendances { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<LeaveRequest> LeaveRequests { get; set; }

    public virtual DbSet<LeaveType> LeaveTypes { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<NotificationReadStatus> NotificationReadStatuses { get; set; }

    public virtual DbSet<PayrollHistory> PayrollHistories { get; set; }

    public virtual DbSet<Position> Positions { get; set; }

  



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActivityLog>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__Activity__5E5499A82337C851");

            entity.ToTable("ActivityLog");

            entity.Property(e => e.LogId).HasColumnName("LogID");
            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.ActionType).HasMaxLength(200);
            entity.Property(e => e.Details).HasMaxLength(1000);
            entity.Property(e => e.Timestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Account).WithMany(p => p.ActivityLogs)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ActivityL__Accou__6B24EA82");
        });

        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.AdminId).HasName("PK__Admins__719FE4E83C9E01CD");

            entity.HasIndex(e => e.Username, "UQ__Admins__536C85E4C159D605").IsUnique();

            entity.Property(e => e.AdminId).HasColumnName("AdminID");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Attendance>(entity =>
        {
            entity.HasKey(e => e.AttendanceId).HasName("PK__Attendan__8B69263CE7923945");

            entity.ToTable("Attendance");

            entity.HasIndex(e => new { e.EmployeeId, e.AttendanceDate }, "UQ__Attendan__77AAB78F69177023").IsUnique();

            entity.Property(e => e.AttendanceId).HasColumnName("AttendanceID");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.OvertimeHours)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(4, 2)");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Present");

            entity.HasOne(d => d.Employee).WithMany(p => p.Attendances)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__Attendanc__Emplo__534D60F1");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK__Departme__B2079BCD0816CEE0");

            entity.HasIndex(e => e.DepartmentName, "UQ__Departme__D949CC340DC07CCB").IsUnique();

            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.DepartmentName).HasMaxLength(100);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04FF1F45E18B9");

            entity.HasIndex(e => e.PhoneNumber, "UQ__Employee__85FB4E38112E358F").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Employee__A9D10534C7EB3AE5").IsUnique();

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.AnnualLeaveDaysRemaining)
                .HasDefaultValue(12.0m)
                .HasColumnType("decimal(4, 1)");
            entity.Property(e => e.BaseSalary)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.EmploymentStatus)
                .HasMaxLength(50)
                .HasDefaultValue("Active");
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.HireDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.PositionId).HasColumnName("PositionID");
            entity.Property(e => e.SickLeaveDaysRemaining)
                .HasDefaultValue(10.0m)
                .HasColumnType("decimal(4, 1)");

            entity.HasOne(d => d.Department).WithMany(p => p.Employees)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Employees__Depar__4316F928");

            entity.HasOne(d => d.Position).WithMany(p => p.Employees)
                .HasForeignKey(d => d.PositionId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Employees__Posit__440B1D61");
        });

        modelBuilder.Entity<LeaveRequest>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("PK__LeaveReq__33A8519A235EFCE1");

            entity.Property(e => e.RequestId).HasColumnName("RequestID");
            entity.Property(e => e.ApprovedByAdminId).HasColumnName("ApprovedByAdminID");
            entity.Property(e => e.ApprovedDate).HasColumnType("datetime");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.LeaveTypeId).HasColumnName("LeaveTypeID");
            entity.Property(e => e.NumberOfDays).HasColumnType("decimal(4, 1)");
            entity.Property(e => e.Reason).HasMaxLength(500);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Pending");

            entity.HasOne(d => d.ApprovedByAdmin).WithMany(p => p.LeaveRequests)
                .HasForeignKey(d => d.ApprovedByAdminId)
                .HasConstraintName("FK__LeaveRequ__Appro__5EBF139D");

            entity.HasOne(d => d.Employee).WithMany(p => p.LeaveRequests)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LeaveRequ__Emplo__5BE2A6F2");

            entity.HasOne(d => d.LeaveType).WithMany(p => p.LeaveRequests)
                .HasForeignKey(d => d.LeaveTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LeaveRequ__Leave__5CD6CB2B");
        });

        modelBuilder.Entity<LeaveType>(entity =>
        {
            entity.HasKey(e => e.LeaveTypeId).HasName("PK__LeaveTyp__43BE8FF4FB90AA86");

            entity.HasIndex(e => e.LeaveTypeName, "UQ__LeaveTyp__E6D8DFAB8F206CE4").IsUnique();

            entity.Property(e => e.LeaveTypeId).HasColumnName("LeaveTypeID");
            entity.Property(e => e.IsPaid).HasDefaultValue(true);
            entity.Property(e => e.LeaveTypeName).HasMaxLength(100);
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__Notifica__20CF2E32CBE6E18D");

            entity.Property(e => e.NotificationId).HasColumnName("NotificationID");
            entity.Property(e => e.SenderAdminId).HasColumnName("SenderAdminID");
            entity.Property(e => e.SentDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TargetDepartmentId).HasColumnName("TargetDepartmentID");
            entity.Property(e => e.Title).HasMaxLength(200);

            entity.HasOne(d => d.SenderAdmin).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.SenderAdminId)
                .HasConstraintName("FK__Notificat__Sende__628FA481");

            entity.HasOne(d => d.TargetDepartment).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.TargetDepartmentId)
                .HasConstraintName("FK__Notificat__Targe__6383C8BA");
        });

        modelBuilder.Entity<NotificationReadStatus>(entity =>
        {
            entity.HasKey(e => new { e.NotificationId, e.EmployeeId }).HasName("PK__Notifica__27622ACD962495ED");

            entity.ToTable("NotificationReadStatus");

            entity.Property(e => e.NotificationId).HasColumnName("NotificationID");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.IsRead).HasDefaultValue(false);

            entity.HasOne(d => d.Employee).WithMany(p => p.NotificationReadStatuses)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__Notificat__Emplo__6754599E");

            entity.HasOne(d => d.Notification).WithMany(p => p.NotificationReadStatuses)
                .HasForeignKey(d => d.NotificationId)
                .HasConstraintName("FK__Notificat__Notif__66603565");
        });

        modelBuilder.Entity<PayrollHistory>(entity =>
        {
            entity.HasKey(e => e.PayrollId).HasName("PK__PayrollH__99DFC69238523DDE");

            entity.ToTable("PayrollHistory");

            entity.HasIndex(e => new { e.EmployeeId, e.PayrollMonth, e.PayrollYear }, "UQ__PayrollH__0F2ABD5C62B8BB59").IsUnique();

            entity.Property(e => e.PayrollId).HasColumnName("PayrollID");
            entity.Property(e => e.Allowances)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.BaseSalaryAtTime).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Bonuses)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Deductions)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.Notes).HasMaxLength(500);
            entity.Property(e => e.PaymentDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.TotalPay).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Employee).WithMany(p => p.PayrollHistories)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__PayrollHi__Emplo__4BAC3F29");
        });

        modelBuilder.Entity<Position>(entity =>
        {
            entity.HasKey(e => e.PositionId).HasName("PK__Position__60BB9A59FEC93CDC");

            entity.HasIndex(e => e.PositionTitle, "UQ__Position__B27DF5060F13400D").IsUnique();

            entity.Property(e => e.PositionId).HasColumnName("PositionID");
            entity.Property(e => e.PositionTitle).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
