using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagement.DAL.Models;

namespace EmployeeManagement.BLL.Services
{
    public interface IAttendanceService
    {
        // Nghiệp vụ chính
        void ClockIn(int employeeId);
        void ClockOut(int employeeId);

        // Thêm/Sửa chấm công thủ công (khi quên chấm, hoặc sửa)
        void AddOrUpdateManualAttendance(Attendance attendance);

        Attendance? GetAttendanceRecord(long attendanceId);
        Attendance? GetAttendanceByEmployeeAndDate(int employeeId, DateTime date);

        // Báo cáo chấm công
        IEnumerable<Attendance> GetAttendanceReport(int employeeId, DateTime startDate, DateTime endDate);
        IEnumerable<Attendance> GetDailyAttendanceSheet(DateTime date);
    }
}
