using System.Configuration;
using System.Data;
using System.Windows;
using Microsoft.Extensions.Hosting;

using EmployeeManagement.DAL.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using EmployeeManagement.DAL.Models;
using Microsoft.EntityFrameworkCore;
using EmployeeManagement.BLL.Services;

namespace EmployeeManagement.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IHost? AppHost { get; private set; }
        public static IConfiguration? Configuration { get; private set; } // Thêm dòng này
      

        public App()
        {
            AppHost = Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    // === Nạp Configuration từ appsettings.json ===
                    // Dòng này phải chạy trước khi đăng ký DbContext
                    var builder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

                    Configuration = builder.Build(); // Xây dựng Configuration

                    // === Đăng ký DBContext ===
                    services.AddDbContext<Prn212Context>(options => // Sửa tên DbContext
                    {
                        // Đọc chuỗi kết nối từ Configuration
                        string connectionString = Configuration.GetConnectionString("PrnDb");
                        options.UseSqlServer(connectionString);
                    });

                    // === Đăng ký Repository (DAL) ===
                    services.AddScoped<IUnitOfWork, UnitOfWork>();

                    // === Đăng ký Services (BLL) ===
                    // (Sẽ thêm sau)
                    services.AddScoped<IEmployeeService, EmployeeService>();
                    services.AddScoped<IAdminService, AdminService>();
                    services.AddScoped<IDepartmentService, DepartmentService>();
                    services.AddScoped<IAttendanceService, AttendanceService>();
                    services.AddScoped<IActivityLogService, ActivityLogService>();
                    services.AddScoped<ILeaveService, LeaveService>();
                    services.AddScoped<IPayrollHistoryService, PayrollHistoryService>();
                    services.AddScoped<IPositionService, PositionService>();
                    services.AddScoped<INotificationService, NotificationService>();
                    

                    // === Đăng ký Cửa sổ (UI) ===
                    services.AddTransient<MainWindow>();
                    services.AddTransient<LoginWindow>();
                })
                .Build();
        }
        // Ghi đè phương thức OnStartup
        protected override async void OnStartup(StartupEventArgs e)
        {
            // Bắt đầu chạy Host
            await AppHost!.StartAsync();

            // Lấy MainWindow TỪ BỘ CHỨA DI
            // (DI sẽ tự động inject các service nếu MainWindow cần)
            var startupForm = AppHost.Services.GetRequiredService<LoginWindow>();

            // Hiển thị cửa sổ
            startupForm.Show();

            base.OnStartup(e);
        }

        // Ghi đè phương thức OnExit
        protected override async void OnExit(ExitEventArgs e)
        {
            await AppHost!.StopAsync();
            base.OnExit(e);
        }
    }

}
