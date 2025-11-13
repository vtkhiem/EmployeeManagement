using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagement.DAL.Models;

namespace EmployeeManagement.BLL.Services
{
    public interface IEmployeeService
    {
        IEnumerable<Employee> GetAllEmployees();

        Employee? GetEmployeeById(int id);

        void AddEmployee(Employee employee);

        void UpdateEmployee(Employee employee);

        void DeleteEmployee(int id);

        IEnumerable<Employee> SearchEmployeesByName(string keyword);

        Employee? LoginAsEmployee(string email, string password);

        IEnumerable<Employee> FilterEmployees(int? departmentId, string? gender, decimal? minSalary, decimal? maxSalary, DateOnly? fromDate, DateOnly? toDate);
    }
}
