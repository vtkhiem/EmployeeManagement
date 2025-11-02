using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagement.DAL.Models;

namespace EmployeeManagement.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Prn212Context _context;
        public IRepository<Admin> AdminRepository { get; private set; }
        public IRepository<Employee> EmployeeRepository { get; private set; }
        public IRepository<LeaveRequest> LeaveRequestRepository { get; private set; }
        public IRepository<Notification> NotificationRepository { get; private set; }
        public IRepository<ActivityLog> ActivityLogRepository { get; private set; }
        public IRepository<Department> DepartmentRepository { get; private set; }
        public IRepository<Position> PositionRepository { get; private set; }
        public IRepository<Attendance> AttendanceRepository { get; private set; }
        public IRepository<LeaveType> LeaveTypeRepository { get; private set; }
        public IRepository<NotificationReadStatus> NotificationReadStatusRepository { get; private set; }
        public IRepository<PayrollHistory> PayrollHistoryRepository { get; private set; }

        public UnitOfWork(Prn212Context context)
        {
            _context = context;

            AdminRepository = new GenericRepository<Admin>(_context);
            EmployeeRepository = new GenericRepository<Employee>(_context);
            LeaveRequestRepository = new GenericRepository<LeaveRequest>(_context);
            NotificationRepository = new GenericRepository<Notification>(_context);
            ActivityLogRepository = new GenericRepository<ActivityLog>(_context);
            DepartmentRepository = new GenericRepository<Department>(_context);
            PositionRepository = new GenericRepository<Position>(_context);
            AttendanceRepository = new GenericRepository<Attendance>(_context);
            LeaveTypeRepository = new GenericRepository<LeaveType>(_context);
            NotificationReadStatusRepository = new GenericRepository<NotificationReadStatus>(_context);
            PayrollHistoryRepository = new GenericRepository<PayrollHistory>(_context);


        }
        public void Save()
        {
            _context.SaveChanges();
        }
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
