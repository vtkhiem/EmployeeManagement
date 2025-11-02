using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagement.DAL.Models;
using EmployeeManagement.DAL.Repositories;
// 1. Thêm thư viện BCrypt.Net
using BCrypt.Net;

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
            // Bước 1: Tìm admin bằng username
            var admin = _unitOfWork.AdminRepository
                                   .Find(a => a.Username == username && a.IsActive == true)
                                   .FirstOrDefault();

            // Nếu không tìm thấy admin
            if (admin == null)
            {
                return null;
            }

            // Bước 2: Xác thực mật khẩu
            bool isPasswordValid;
            try
            {
                isPasswordValid = BCrypt.Net.BCrypt.Verify(password, admin.PasswordHash);
            }
            catch (Exception)
            {
                // Lỗi nếu hash trong DB bị hỏng
                return null;
            }

            if (isPasswordValid)
            {
                // Mật khẩu đúng
                return admin;
            }

            // Mật khẩu sai
            return null;
        }

        public bool ChangePassword(int adminId, string oldPassword, string newPassword)
        {
            var admin = _unitOfWork.AdminRepository.GetById(adminId);
            if (admin == null)
            {
                return false; // Không tìm thấy
            }

            try
            {
                // 1. Kiểm tra mật khẩu cũ
                if (BCrypt.Net.BCrypt.Verify(oldPassword, admin.PasswordHash))
                {
                    // 2. Hash mật khẩu mới
                    admin.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);

                    // 3. Cập nhật
                    _unitOfWork.AdminRepository.Update(admin);
                    _unitOfWork.Save();
                    return true;
                }
            }
            catch (Exception)
            {
                return false; // Lỗi (ví dụ: hash hỏng)
            }

            // Mật khẩu cũ sai
            return false;
        }

        public void CreateAdmin(Admin admin, string password)
        {
            if (admin == null)
            {
                throw new ArgumentNullException(nameof(admin));
            }

            // Kiểm tra username trùng
            if (_unitOfWork.AdminRepository.Find(a => a.Username == admin.Username).FirstOrDefault() != null)
            {
                throw new InvalidOperationException("Username đã tồn tại.");
            }

            // Hash mật khẩu
            admin.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);

            _unitOfWork.AdminRepository.Add(admin);
            _unitOfWork.Save();
        }

        public void DeleteAdmin(int id)
        {
            var admin = _unitOfWork.AdminRepository.GetById(id);
            if (admin != null)
            {
                // Giả sử IRepository của bạn có hàm Delete(int id)
                _unitOfWork.AdminRepository.Delete(id);
                _unitOfWork.Save();
            }
        }

        public Admin? GetAdminById(int id)
        {
            return _unitOfWork.AdminRepository.GetById(id);
        }

        public IEnumerable<Admin> GetAllAdmins()
        {
            return _unitOfWork.AdminRepository.GetAll();
        }

        public void ToggleAdminStatus(int adminId, bool isActive)
        {
            var admin = _unitOfWork.AdminRepository.GetById(adminId);
            if (admin != null)
            {
                admin.IsActive = isActive;
                _unitOfWork.AdminRepository.Update(admin);
                _unitOfWork.Save();
            }
        }

        public void UpdateAdmin(Admin admin)
        {
            if (admin == null)
            {
                throw new ArgumentNullException(nameof(admin));
            }

            // Kiểm tra username trùng (trừ chính nó)
            if (_unitOfWork.AdminRepository.Find(a => a.Username == admin.Username && a.AdminId != admin.AdminId).FirstOrDefault() != null)
            {
                throw new InvalidOperationException("Username đã tồn tại.");
            }

            _unitOfWork.AdminRepository.Update(admin);
            _unitOfWork.Save();
        }
    }
}

