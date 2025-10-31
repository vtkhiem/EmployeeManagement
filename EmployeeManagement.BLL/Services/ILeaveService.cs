using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagement.DAL.Models;

namespace EmployeeManagement.BLL.Services
{
    public interface ILeaveService
    {
        // --- Quản lý Đơn xin nghỉ (LeaveRequests) ---

        // Nhân viên tạo đơn
        void SubmitLeaveRequest(LeaveRequest request);

        // Admin duyệt đơn
        void ApproveLeaveRequest(int requestId, int approvedByAdminId);

        // Admin từ chối đơn
        void RejectLeaveRequest(int requestId, int rejectedByAdminId);

        LeaveRequest? GetLeaveRequestById(int requestId);
        IEnumerable<LeaveRequest> GetLeaveRequestsByEmployee(int employeeId);
        IEnumerable<LeaveRequest> GetLeaveRequestsByStatus(string status); // 'Pending', 'Approved', 'Rejected'
        IEnumerable<LeaveRequest> GetAllLeaveRequests();


        // --- Quản lý Loại nghỉ phép (LeaveTypes) ---
        IEnumerable<LeaveType> GetAllLeaveTypes();
        LeaveType? GetLeaveTypeById(int id);
        void AddLeaveType(LeaveType leaveType);
        void UpdateLeaveType(LeaveType leaveType);
        void DeleteLeaveType(int id);
    }
}
