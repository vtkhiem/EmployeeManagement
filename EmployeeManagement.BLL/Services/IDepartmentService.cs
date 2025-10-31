using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagement.DAL.Models;

namespace EmployeeManagement.BLL.Services
{
    public interface IDepartmentService
    {
        IEnumerable<Department> GetAllDepartments();
        Department? GetDepartmentById(int id);
        void AddDepartment(Department department);
        void UpdateDepartment(Department department);
        void DeleteDepartment(int id);

        // Nghiệp vụ bổ sung: Lấy tất cả nhân viên trong một phòng ban
        IEnumerable<Employee> GetEmployeesInDepartment(int departmentId);
    }
}
