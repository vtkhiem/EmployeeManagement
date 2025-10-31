using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagement.DAL.Models;

namespace EmployeeManagement.DAL.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Admin> AdminReposotpry { get; }
        IRepository<Employee> EmployeeRepository { get; }
        IRepository<LeaveRequest> LeaveRequestRepository { get; }
        IRepository<Notification> NotificationRepository { get; }
        IRepository<ActivityLog> ActivityLogRepository { get; }

        IRepository<Department> DepartmentRepository { get; }
        IRepository<Position> PositionRepository { get; }
        IRepository<Attendance> AttendanceRepository { get; }
        IRepository<LeaveType> LeaveTypeRepository { get; }
        IRepository<NotificationReadStatus> NotificationReadStatusRepository { get; }
        IRepository<PayrollHistory> PayrollHistoryRepository { get; }
    
        void Save();

    }
}
