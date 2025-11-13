using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagement.DAL.Models;
using EmployeeManagement.DAL.Repositories;

namespace EmployeeManagement.BLL.Services
{
    public class ActivityLogService : IActivityLogService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ActivityLogService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<ActivityLog> GetActivityLogs(DateTime startDate, DateTime endDate)
        {
            return _unitOfWork.ActivityLogRepository
                .Find(log => log.Timestamp >= startDate && log.Timestamp <= endDate)
                .ToList();
        }

        public IEnumerable<ActivityLog> GetLogsByAdmin(int adminId)
        {
            return _unitOfWork.ActivityLogRepository
                .Find(log => log.AccountId == adminId)
                .ToList();

        }

        public void LogActivity(int adminId, string actionType, string details)
        {
            _unitOfWork.ActivityLogRepository.Add(new ActivityLog
            {
                AccountId = adminId,
                ActionType = actionType,
                Details = details,
                Timestamp = DateTime.UtcNow
            });
        }
    }
}
