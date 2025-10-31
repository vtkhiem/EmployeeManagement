using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagement.DAL.Models;

namespace EmployeeManagement.BLL.Services
{
    public interface IPayrollHistoryService
    {
        // Nghiệp vụ quan trọng: Tạo bảng lương cho tất cả nhân viên trong tháng
        void GenerateMonthlyPayroll(int month, int year);

        // Tạo bảng lương cho một nhân viên cụ thể
        void GeneratePayrollForEmployee(int employeeId, int month, int year);

        PayrollHistory? GetPayrollRecordById(int payrollId);

        // Lấy lịch sử lương của một nhân viên
        IEnumerable<PayrollHistory> GetPayrollHistoryForEmployee(int employeeId);

        // Lấy toàn bộ bảng lương của một tháng
        IEnumerable<PayrollHistory> GetPayrollByMonth(int month, int year);

        // Cập nhật thủ công (điều chỉnh bonus, deductions)
        void UpdatePayrollRecord(PayrollHistory payrollRecord);
    }
}
