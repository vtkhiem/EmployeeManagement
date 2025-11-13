using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Threading;

namespace EmployeeManagement.UI
{
    public partial class AttendanceWindow : Window
    {
        private DispatcherTimer timer;
        private ObservableCollection<AttendanceRecord> attendanceRecords;

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
            attendanceRecords = new ObservableCollection<AttendanceRecord>
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
            if (dgAttendanceHistory.ItemsSource == null)
            {
                dgAttendanceHistory.ItemsSource = attendanceRecords;
            }
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
                // Tạo bản ghi chấm công mới
                var newRecord = new AttendanceRecord
                {
                    Date = DateTime.Now.Date,
                    CheckInTime = DateTime.Now.TimeOfDay,
                    CheckOutTime = null,
                    TotalHours = "-",
                    BreakTime = "0m",
                    Status = "Đang làm việc",
                    Note = ""
                };

                // Thêm vào danh sách
                attendanceRecords.Insert(0, newRecord);
                
                // Cập nhật giao diện
                LoadAttendanceHistory();
                
                MessageBox.Show("Chấm công vào thành công!", "Thông báo", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
                
                // Vô hiệu hóa nút chấm công vào, kích hoạt nút chấm công ra
                btnCheckIn.IsEnabled = false;
                btnCheckOut.IsEnabled = true;
            }
        }

        private void BtnCheckOut_Click(object sender, RoutedEventArgs e)
        {
            // Kiểm tra xem đã chấm công vào chưa
            var todayRecord = attendanceRecords.FirstOrDefault(r => r.Date.Date == DateTime.Now.Date && r.CheckOutTime == null);
            
            if (todayRecord == null)
            {
                MessageBox.Show("Bạn chưa chấm công vào hôm nay!\nVui lòng chấm công vào trước khi chấm công ra.", 
                    "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show(
                $"Xác nhận chấm công ra lúc {DateTime.Now:HH:mm:ss}?",
                "Chấm công ra",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                // Cập nhật giờ ra
                todayRecord.CheckOutTime = DateTime.Now.TimeOfDay;
                
                // Tính tổng giờ làm
                var workHours = todayRecord.CheckOutTime.Value - todayRecord.CheckInTime.Value;
                int hours = (int)workHours.TotalHours;
                int minutes = workHours.Minutes;
                todayRecord.TotalHours = $"{hours}h {minutes}m";
                
                // Cập nhật trạng thái
                if (todayRecord.CheckInTime > new TimeSpan(8, 30, 0))
                {
                    todayRecord.Status = "Đi muộn";
                    var lateMinutes = (int)(todayRecord.CheckInTime.Value - new TimeSpan(8, 30, 0)).TotalMinutes;
                    todayRecord.Note = $"Đi muộn {lateMinutes} phút";
                }
                else if (todayRecord.CheckOutTime < new TimeSpan(17, 30, 0))
                {
                    todayRecord.Status = "Về sớm";
                    var earlyMinutes = (int)(new TimeSpan(17, 30, 0) - todayRecord.CheckOutTime.Value).TotalMinutes;
                    todayRecord.Note = $"Về sớm {earlyMinutes} phút";
                }
                else
                {
                    todayRecord.Status = "Đầy đủ";
                    todayRecord.Note = "";
                }
                
                // Cập nhật giao diện
                LoadAttendanceHistory();
                
                MessageBox.Show("Chấm công ra thành công!", "Thông báo", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
                
                // Vô hiệu hóa nút chấm công ra, kích hoạt nút chấm công vào
                btnCheckOut.IsEnabled = false;
                btnCheckIn.IsEnabled = true;
            }
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            // Làm mới dữ liệu
            LoadAttendanceHistory();
            MessageBox.Show("Đã làm mới dữ liệu!", "Thông báo", 
                MessageBoxButton.OK, MessageBoxImage.Information);
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
    public class AttendanceRecord : INotifyPropertyChanged
    {
        private TimeSpan? _checkInTime;
        private TimeSpan? _checkOutTime;
        private string _totalHours = string.Empty;
        private string _status = string.Empty;
        private string _note = string.Empty;

        public DateTime Date { get; set; }
        
        public TimeSpan? CheckInTime 
        { 
            get => _checkInTime;
            set
            {
                _checkInTime = value;
                OnPropertyChanged(nameof(CheckInTime));
                OnPropertyChanged(nameof(CheckInTimeDisplay));
            }
        }
        
        public TimeSpan? CheckOutTime 
        { 
            get => _checkOutTime;
            set
            {
                _checkOutTime = value;
                OnPropertyChanged(nameof(CheckOutTime));
                OnPropertyChanged(nameof(CheckOutTimeDisplay));
            }
        }
        
        public string TotalHours 
        { 
            get => _totalHours;
            set
            {
                _totalHours = value;
                OnPropertyChanged(nameof(TotalHours));
            }
        }
        
        public string BreakTime { get; set; } = string.Empty;
        
        public string Status 
        { 
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
            }
        }
        
        public string Note 
        { 
            get => _note;
            set
            {
                _note = value;
                OnPropertyChanged(nameof(Note));
            }
        }

        // Properties để hiển thị thời gian
        public string CheckInTimeDisplay => CheckInTime.HasValue 
            ? CheckInTime.Value.ToString(@"hh\:mm\:ss") 
            : "-";
        
        public string CheckOutTimeDisplay => CheckOutTime.HasValue 
            ? CheckOutTime.Value.ToString(@"hh\:mm\:ss") 
            : "-";

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}