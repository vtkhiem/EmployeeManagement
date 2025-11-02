using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagement.DAL.Models;
using EmployeeManagement.DAL.Repositories;

namespace EmployeeManagement.BLL.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        public DepartmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void AddDepartment(Department department)
        {
            throw new NotImplementedException();
        }

        public void DeleteDepartment(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Department> GetAllDepartments()
        {
            throw new NotImplementedException();
        }

        public Department? GetDepartmentById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Employee> GetEmployeesInDepartment(int departmentId)
        {
            throw new NotImplementedException();
        }

        public void UpdateDepartment(Department department)
        {
            throw new NotImplementedException();
        }
    }
}
