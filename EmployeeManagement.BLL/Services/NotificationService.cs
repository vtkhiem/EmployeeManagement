using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagement.DAL.Models;

namespace EmployeeManagement.BLL.Services
{
    public class NotificationService : INotificationService
    {
        public void DeleteNotification(int notificationId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<NotificationReadStatus> GetNotificationsForEmployee(int employeeId)
        {
            throw new NotImplementedException();
        }

        public int GetUnreadNotificationCount(int employeeId)
        {
            throw new NotImplementedException();
        }

        public void MarkAsRead(int notificationId, int employeeId)
        {
            throw new NotImplementedException();
        }

        public void SendNotificationToAll(Notification notification, int senderAdminId)
        {
            throw new NotImplementedException();
        }

        public void SendNotificationToDepartment(Notification notification, int departmentId, int senderAdminId)
        {
            throw new NotImplementedException();
        }
    }
}
