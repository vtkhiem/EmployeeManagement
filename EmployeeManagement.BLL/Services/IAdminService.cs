using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagement.DAL.Models;

namespace EmployeeManagement.BLL.Services
{
    public interface IAdminService
    {
        // Trả về Admin nếu đăng nhập thành công, null nếu thất bại
        Admin? Authenticate(string username, string password);

        IEnumerable<Admin> GetAllAdmins();
        Admin? GetAdminById(int id);

        // Cần mật khẩu khi tạo mới
        void CreateAdmin(Admin admin, string password);

        void UpdateAdmin(Admin admin);

        // Logic đổi mật khẩu riêng
        bool ChangePassword(int adminId, string oldPassword, string newPassword);

        void ToggleAdminStatus(int adminId, bool isActive);
        void DeleteAdmin(int id);



    }
}
