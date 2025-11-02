using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using EmployeeManagement.BLL.Services;
using EmployeeManagement.DAL.Models;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeManagement.UI
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly IAdminService _adminService;
        private readonly IEmployeeService _employeeService;
        private readonly IServiceProvider _serviceProvider; // Dùng để mở các cửa sổ khác

        // Sửa constructor để nhận IServiceProvider
        public LoginWindow(IAdminService adminService, IEmployeeService employeeService, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _adminService = adminService;
            _employeeService = employeeService;
            _serviceProvider = serviceProvider;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = pbPassword.Password; // SỬA LỖI: Dùng .Password

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Tên đăng nhập và mật khẩu không được để trống.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Bước 1: Thử đăng nhập với tư cách Admin
            Admin? admin = _adminService.Authenticate(username, password);

            if (admin != null)
            {
                // Đăng nhập Admin thành công
                MessageBox.Show($"Chào mừng Admin: {admin.Username}!", "Đăng nhập thành công");

                // Mở cửa sổ chính (Dành cho Admin)
                var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
                mainWindow.Show();
                this.Close();
                return; // Kết thúc sự kiện click
            }

            // Bước 2: Nếu Admin thất bại, thử đăng nhập với tư cách Employee
            // (Giả sử bạn đã thêm hàm LoginAsEmployee vào IEmployeeService và lớp triển khai của nó)
            Employee? employee = _employeeService.LoginAsEmployee(username, password);

            if (employee != null)
            {
                // Đăng nhập Employee thành công
                MessageBox.Show($"Chào mừng nhân viên: {employee.FullName}!", "Đăng nhập thành công");

                // TODO: Mở cửa sổ dành cho nhân viên (ví dụ: EmployeeDashboardWindow)
                // var empWindow = _serviceProvider.GetRequiredService<EmployeeDashboardWindow>();
                // empWindow.Show();

                // Hiện tại, ta cứ mở MainWindow (bạn nên thay đổi điều này)
                var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
                // Bạn có thể muốn truyền thông tin nhân viên này cho MainWindow
                // mainWindow.SetCurrentUser(employee);
                mainWindow.Show();

                this.Close();
                return; // Kết thúc sự kiện click
            }

            // Bước 3: Nếu cả hai đều thất bại
            MessageBox.Show("Đăng nhập thất bại. Vui lòng kiểm tra lại tên đăng nhập hoặc mật khẩu.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
