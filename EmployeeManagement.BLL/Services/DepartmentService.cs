using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagement.DAL.Models;
using EmployeeManagement.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.BLL.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Prn212Context _context;
        
        public DepartmentService(IUnitOfWork unitOfWork, Prn212Context context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public IEnumerable<Department> GetAllDepartments()
        {
            // Eager load Employees collection để hiển thị số nhân viên
            return _context.Departments
                .Include(d => d.Employees)
                .ToList();
        }

        public Department? GetDepartmentById(int id)
        {
            return _unitOfWork.DepartmentRepository.GetById(id);
        }

        public void AddDepartment(Department department)
        {
            if (string.IsNullOrWhiteSpace(department.DepartmentName))
                throw new ArgumentException("Tên phòng ban không được để trống");

            _unitOfWork.DepartmentRepository.Add(department);
            _unitOfWork.Save();
        }

        public void UpdateDepartment(Department department)
        {
            if (string.IsNullOrWhiteSpace(department.DepartmentName))
                throw new ArgumentException("Tên phòng ban không được để trống");

            _unitOfWork.DepartmentRepository.Update(department);
            _unitOfWork.Save();
        }

        public void DeleteDepartment(int id)
        {
            var department = _unitOfWork.DepartmentRepository.GetById(id);
            if (department == null)
                throw new ArgumentException("Không tìm thấy phòng ban");

            // Kiểm tra xem có nhân viên nào trong phòng ban không
            var employees = GetEmployeesInDepartment(id);
            if (employees.Any())
                throw new InvalidOperationException("Không thể xóa phòng ban có nhân viên");

            _unitOfWork.DepartmentRepository.Delete(id);
            _unitOfWork.Save();
        }

        public IEnumerable<Employee> GetEmployeesInDepartment(int departmentId)
        {
            return _unitOfWork.EmployeeRepository
                .Find(e => e.DepartmentId == departmentId);
        }
    }
}
