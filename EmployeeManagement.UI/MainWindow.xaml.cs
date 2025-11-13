using EmployeeManagement.BLL.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EmployeeManagement.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IAdminService _adminService;
        private readonly IServiceProvider _serviceProvider;

        public MainWindow(IAdminService adminService, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _adminService = adminService;
            _serviceProvider = serviceProvider;
        }

        // Xử lý sự kiện đăng xuất
        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show(
                "Bạn có chắc chắn muốn đăng xuất?",
                "Xác nhận đăng xuất",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                // Mở lại màn hình đăng nhập
                var loginWindow = _serviceProvider.GetRequiredService<LoginWindow>();
                loginWindow.Show();
                this.Close();
            }
        }

        // Các sự kiện cho các nút quản lý
        private void EmployeeManagementButton_Click(object sender, RoutedEventArgs e)
        {
            var employeeWindow = _serviceProvider.GetRequiredService<EmployeeManagementWindow>();
            employeeWindow.Show();
        }

        private void DepartmentManagementButton_Click(object sender, RoutedEventArgs e)
        {
            var departmentWindow = _serviceProvider.GetRequiredService<DepartmentManagementWindow>();
            departmentWindow.Show();
        }

        private void AttendanceManagementButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Chức năng Quản lý chấm công đang được phát triển...", 
                "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void SalaryManagementButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Chức năng Quản lý lương đang được phát triển...", 
                "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ReportsButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Chức năng Báo cáo thống kê đang được phát triển...", 
                "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Chức năng Cài đặt hệ thống đang được phát triển...", 
                "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}