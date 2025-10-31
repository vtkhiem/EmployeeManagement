using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagement.DAL.Models;

namespace EmployeeManagement.BLL.Services
{
    public interface IActivityLogService
    {
        // Nghiệp vụ chính: Ghi lại một hành động
        void LogActivity(int adminId, string actionType, string details);

        // Lấy log để xem
        IEnumerable<ActivityLog> GetActivityLogs(DateTime startDate, DateTime endDate);
        IEnumerable<ActivityLog> GetLogsByAdmin(int adminId);
    }
}
