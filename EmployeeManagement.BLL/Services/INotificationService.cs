using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagement.DAL.Models;

namespace EmployeeManagement.BLL.Services
{
    public interface INotificationService
    {
        // Gửi thông báo cho tất cả (TargetDepartmentID = NULL)
        void SendNotificationToAll(Notification notification, int senderAdminId);

        // Gửi thông báo cho 1 phòng ban
        void SendNotificationToDepartment(Notification notification, int departmentId, int senderAdminId);

        void DeleteNotification(int notificationId);

        // Lấy thông báo cho 1 nhân viên (bao gồm cả trạng thái đã đọc/chưa đọc)
        IEnumerable<NotificationReadStatus> GetNotificationsForEmployee(int employeeId);

        int GetUnreadNotificationCount(int employeeId);

        // Đánh dấu đã đọc
        void MarkAsRead(int notificationId, int employeeId);
    }
}
