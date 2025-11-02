using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagement.DAL.Models;
using EmployeeManagement.DAL.Repositories;

namespace EmployeeManagement.BLL.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void AddEmployee(Employee employee)
        {
            var existing = _unitOfWork.EmployeeRepository
                                    .Find(c => c.Email == employee.Email)
                                    .FirstOrDefault();

            if (existing != null)
            {
                throw new Exception("Email đã tồn tại. Vui lòng sử dụng email khác.");
            }
            _unitOfWork.EmployeeRepository.Add(employee);
            _unitOfWork.Save();
        }

        public void DeleteEmployee(int id)
        {
            var employee = _unitOfWork.EmployeeRepository.GetById(id);
            if (employee != null)
            {
                _unitOfWork.EmployeeRepository.Delete(id);
                _unitOfWork.Save();
            }
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _unitOfWork.EmployeeRepository.GetAll();

        }

        public Employee? GetEmployeeById(int id)
        {
            return _unitOfWork.EmployeeRepository.GetById(id);

        }

        public Employee? LoginAsEmployee(string email, string password)
        {
            // Bước 1: Tìm admin bằng username
            var employee = _unitOfWork.EmployeeRepository
                                   .Find(a => a.Email == email && a.EmploymentStatus == "Active")
                                   .FirstOrDefault();

            // Nếu không tìm thấy admin
            if (employee == null)
            {
                return null;
            }

            // Bước 2: Xác thực mật khẩu
            bool isPasswordValid;
            try
            {
                isPasswordValid = BCrypt.Net.BCrypt.Verify(password, employee.PasswordHash);
            }
            catch (Exception)
            {
                // Lỗi nếu hash trong DB bị hỏng
                return null;
            }

            if (isPasswordValid)
            {
                // Mật khẩu đúng
                return employee;
            }

            // Mật khẩu sai
            return null;
        }

        public IEnumerable<Employee> SearchEmployeesByName(string name)
        {
          
            return _unitOfWork.EmployeeRepository
                        .Find(e => e.FullName.Contains(name))
                        .OrderBy(e => e.FullName); 

        }

        public void UpdateEmployee(Employee employee)
        {

            _unitOfWork.EmployeeRepository.Update(employee);
            _unitOfWork.Save();

        }
    }
}
