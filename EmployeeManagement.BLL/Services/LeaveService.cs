using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagement.DAL.Models;

namespace EmployeeManagement.BLL.Services
{
    public class LeaveService : ILeaveService
    {
        public void AddLeaveType(LeaveType leaveType)
        {
            throw new NotImplementedException();
        }

        public void ApproveLeaveRequest(int requestId, int approvedByAdminId)
        {
            throw new NotImplementedException();
        }

        public void DeleteLeaveType(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LeaveRequest> GetAllLeaveRequests()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LeaveType> GetAllLeaveTypes()
        {
            throw new NotImplementedException();
        }

        public LeaveRequest? GetLeaveRequestById(int requestId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LeaveRequest> GetLeaveRequestsByEmployee(int employeeId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LeaveRequest> GetLeaveRequestsByStatus(string status)
        {
            throw new NotImplementedException();
        }

        public LeaveType? GetLeaveTypeById(int id)
        {
            throw new NotImplementedException();
        }

        public void RejectLeaveRequest(int requestId, int rejectedByAdminId)
        {
            throw new NotImplementedException();
        }

        public void SubmitLeaveRequest(LeaveRequest request)
        {
            throw new NotImplementedException();
        }

        public void UpdateLeaveType(LeaveType leaveType)
        {
            throw new NotImplementedException();
        }
    }
}
