using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagement.DAL.Models;
using EmployeeManagement.DAL.Repositories;

namespace EmployeeManagement.BLL.Services
{
    public class PayrollHistoryService : IPayrollHistoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PayrollHistoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void GenerateMonthlyPayroll(int month, int year)
        {
            throw new NotImplementedException();
        }

        public void GeneratePayrollForEmployee(int employeeId, int month, int year)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PayrollHistory> GetPayrollByMonth(int month, int year)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PayrollHistory> GetPayrollHistoryForEmployee(int employeeId)
        {
            throw new NotImplementedException();
        }

        public PayrollHistory? GetPayrollRecordById(int payrollId)
        {
            throw new NotImplementedException();
        }

        public void UpdatePayrollRecord(PayrollHistory payrollRecord)
        {
            throw new NotImplementedException();
        }
    }
}
