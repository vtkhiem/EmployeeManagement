using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagement.DAL.Models;
using EmployeeManagement.DAL.Repositories;

namespace EmployeeManagement.BLL.Services
{
    public class AdminService : IAdminService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdminService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Admin? Authenticate(string username, string password)
        {
            throw new NotImplementedException();
        }

        public bool ChangePassword(int adminId, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public void CreateAdmin(Admin admin, string password)
        {
            throw new NotImplementedException();
        }

        public void DeleteAdmin(int id)
        {
            throw new NotImplementedException();
        }

        public Admin? GetAdminById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Admin> GetAllAdmins()
        {
            throw new NotImplementedException();
        }

        public void ToggleAdminStatus(int adminId, bool isActive)
        {
            throw new NotImplementedException();
        }

        public void UpdateAdmin(Admin admin)
        {
            throw new NotImplementedException();
        }
    }
}
