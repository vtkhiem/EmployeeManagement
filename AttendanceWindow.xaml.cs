using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Threading;

namespace EmployeeManagement.UI
{
    public partial class AttendanceWindow : Window
    {
        private DispatcherTimer timer;
        private List<AttendanceRecord> attendanceRecords;

        public AttendanceWindow()
        {
            InitializeComponent();
            InitializeWindow();
        }

        private void InitializeWindow()
        {
            // Khởi tạo timer để cập nhật thời gian
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();

            // Thiết lập ngày hiện tại
            txtCurrentDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            
            // Thiết lập tên nhân viên (có thể lấy từ session hoặc database)
            txtEmployeeName.Text = "Nguyễn Văn A"; // Placeholder

            // Thiết lập ngày mặc định cho bộ lọc
            dpFromDate.SelectedDate = DateTime.Now.AddDays(-30);
            dpToDate.SelectedDate = DateTime.Now;

            // Khởi tạo dữ liệu mẫu
            LoadSampleData();
            LoadAttendanceHistory();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            txtCurrentTime.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void LoadSampleData()
        {
            // Dữ liệu mẫu cho lịch sử chấm công
            attendanceRecords = new List<AttendanceRecord>
            {
                new AttendanceRecord
                {
                    Date = DateTime.Now.AddDays(-1),
                    CheckInTime = new TimeSpan(8, 30, 0),
                    CheckOutTime = new TimeSpan(17, 30, 0),
                    TotalHours = "8h 30m",
                    BreakTime = "30m",
                    Status = "Đầy đủ",
                    Note = ""
                },
                new AttendanceRecord
                {
                    Date = DateTime.Now.AddDays(-2),
                    CheckInTime = new TimeSpan(8, 45, 0),
                    CheckOutTime = new TimeSpan(17, 15, 0),
                    TotalHours = "8h 0m",
                    BreakTime = "30m",
                    Status = "Đi muộn",
                    Note = "Đi muộn 15 phút"
                },
                new AttendanceRecord
                {
                    Date = DateTime.Now.AddDays(-3),
                    CheckInTime = new TimeSpan(8, 30, 0),
                    CheckOutTime = new TimeSpan(16, 30, 0),
                    TotalHours = "7h 30m",
                    BreakTime = "30m",
                    Status = "Về sớm",
                    Note = "Về sớm 1 tiếng"
                }
            };
        }

        private void LoadAttendanceHistory()
        {
            dgAttendanceHistory.ItemsSource = attendanceRecords;
            UpdateStatistics();
        }

        private void UpdateStatistics()
        {
            txtTotalDays.Text = attendanceRecords.Count.ToString();
            txtTotalHours.Text = "24h 0m"; // Tính toán thực tế từ dữ liệu
        }

        private void BtnCheckIn_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show(
                $"Xác nhận chấm công vào lúc {DateTime.Now:HH:mm:ss}?",
                "Chấm công vào",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                // TODO: Lưu thông tin chấm công vào database
                MessageBox.Show("Chấm công vào thành công!", "Thông báo", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
                
                // Vô hiệu hóa nút chấm công vào
                btnCheckIn.IsEnabled = false;
                btnCheckOut.IsEnabled = true;
            }
        }

        private void BtnCheckOut_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show(
                $"Xác nhận chấm công ra lúc {DateTime.Now:HH:mm:ss}?",
                "Chấm công ra",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                // TODO: Lưu thông tin chấm công ra database
                MessageBox.Show("Chấm công ra thành công!", "Thông báo", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
                
                // Vô hiệu hóa nút chấm công ra
                btnCheckOut.IsEnabled = false;
                btnCheckIn.IsEnabled = true;
            }
        }

        private void BtnBreak_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show(
                "Bạn muốn bắt đầu hay kết thúc giờ nghỉ?",
                "Nghỉ giải lao",
                MessageBoxButton.YesNoCancel,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                // TODO: Bắt đầu giờ nghỉ
                MessageBox.Show("Bắt đầu giờ nghỉ!", "Thông báo", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (result == MessageBoxResult.No)
            {
                // TODO: Kết thúc giờ nghỉ
                MessageBox.Show("Kết thúc giờ nghỉ!", "Thông báo", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void BtnFilter_Click(object sender, RoutedEventArgs e)
        {
            if (dpFromDate.SelectedDate.HasValue && dpToDate.SelectedDate.HasValue)
            {
                // TODO: Lọc dữ liệu theo khoảng thời gian
                MessageBox.Show($"Lọc dữ liệu từ {dpFromDate.SelectedDate:dd/MM/yyyy} đến {dpToDate.SelectedDate:dd/MM/yyyy}",
                    "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn khoảng thời gian để lọc!", "Cảnh báo", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnExport_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Xuất báo cáo Excel
            MessageBox.Show("Chức năng xuất báo cáo đang được phát triển!", "Thông báo", 
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        protected override void OnClosed(EventArgs e)
        {
            timer?.Stop();
            base.OnClosed(e);
        }
    }

    // Model class cho dữ liệu chấm công
    public class AttendanceRecord
    {
        public DateTime Date { get; set; }
        public TimeSpan? CheckInTime { get; set; }
        public TimeSpan? CheckOutTime { get; set; }
        public string TotalHours { get; set; }
        public string BreakTime { get; set; }
        public string Status { get; set; }
        public string Note { get; set; }
    }
}