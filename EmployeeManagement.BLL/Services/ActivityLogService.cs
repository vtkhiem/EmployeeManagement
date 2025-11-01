using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagement.DAL.Models;

namespace EmployeeManagement.BLL.Services
{
    public class ActivityLogService : IActivityLogService
    {
        public IEnumerable<ActivityLog> GetActivityLogs(DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ActivityLog> GetLogsByAdmin(int adminId)
        {
            throw new NotImplementedException();
        }

        public void LogActivity(int adminId, string actionType, string details)
        {
            throw new NotImplementedException();
        }
    }
}
