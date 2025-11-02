using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagement.DAL.Models;
using EmployeeManagement.DAL.Repositories;

namespace EmployeeManagement.BLL.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AttendanceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void AddOrUpdateManualAttendance(Attendance attendance)
        {
            throw new NotImplementedException();
        }

        public void ClockIn(int employeeId)
        {
            throw new NotImplementedException();
        }

        public void ClockOut(int employeeId)
        {
            throw new NotImplementedException();
        }

        public Attendance? GetAttendanceByEmployeeAndDate(int employeeId, DateTime date)
        {
            throw new NotImplementedException();
        }

        public Attendance? GetAttendanceRecord(long attendanceId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Attendance> GetAttendanceReport(int employeeId, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Attendance> GetDailyAttendanceSheet(DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}
