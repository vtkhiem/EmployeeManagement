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
using EmployeeManagement.BLL.Services;

namespace EmployeeManagement.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IAdminService _adminService;
        // (Nếu bạn cần các service khác, hãy thêm chúng ở đây)
        // private readonly IRoomService _roomService;

        // 2. Yêu cầu (Inject) service qua hàm khởi tạo
        public MainWindow(IAdminService adminService) // <-- DI sẽ tự động "tiêm" vào đây
        {
            InitializeComponent();

            // 3. Gán service được tiêm vào biến của bạn
            _adminService = adminService;

            // (Bạn cũng có thể gán các service khác)
            // _roomService = roomService;
        }
    }
}