using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace EmployeeManagement.UI
{
    public partial class NotificationWindow : Window
    {
        private List<NotificationRecord> notifications;
        private List<string> attachedFiles;

        public NotificationWindow()
        {
            InitializeComponent();
            InitializeWindow();
        }

        private void InitializeWindow()
        {
            // Thiết lập thông tin người gửi
            txtSenderName.Text = "Nguyễn Văn Admin"; // Placeholder
            txtSenderDepartment.Text = "Phòng Nhân sự"; // Placeholder

            // Thiết lập ngày mặc định cho bộ lọc
            dpFromDateNotif.SelectedDate = DateTime.Now.AddDays(-30);
            dpToDateNotif.SelectedDate = DateTime.Now;

            // Khởi tạo danh sách file đính kèm
            attachedFiles = new List<string>();

            // Đăng ký sự kiện cho radio buttons
            rbAllEmployees.Checked += RbRecipient_Checked;
            rbDepartment.Checked += RbRecipient_Checked;
            rbSpecific.Checked += RbRecipient_Checked;

            // Load dữ liệu mẫu
            LoadSampleNotifications();
            LoadNotificationList();
        }

        private void RbRecipient_Checked(object sender, RoutedEventArgs e)
        {
            if (rbAllEmployees.IsChecked == true)
            {
                pnlDepartment.Visibility = Visibility.Collapsed;
                pnlSpecific.Visibility = Visibility.Collapsed;
            }
            else if (rbDepartment.IsChecked == true)
            {
                pnlDepartment.Visibility = Visibility.Visible;
                pnlSpecific.Visibility = Visibility.Collapsed;
            }
            else if (rbSpecific.IsChecked == true)
            {
                pnlDepartment.Visibility = Visibility.Collapsed;
                pnlSpecific.Visibility = Visibility.Visible;
            }
        }

        private void LoadSampleNotifications()
        {
            notifications = new List<NotificationRecord>
            {
                new NotificationRecord
                {
                    Id = 1,
                    Title = "Thông báo nghỉ lễ Quốc khánh 2/9",
                    Sender = "Phòng Nhân sự",
                    Recipients = "Tất cả nhân viên",
                    Priority = "Quan trọng",
                    SentDate = DateTime.Now.AddDays(-2),
                    Status = "Đã gửi",
                    ReadCount = "45/50",
                    Content = "Công ty thông báo lịch nghỉ lễ Quốc khánh 2/9..."
                },
                new NotificationRecord
                {
                    Id = 2,
                    Title = "Họp phòng IT định kỳ tháng 9",
                    Sender = "Trưởng phòng IT",
                    Recipients = "Phòng IT",
                    Priority = "Thông thường",
                    SentDate = DateTime.Now.AddDays(-1),
                    Status = "Đã gửi",
                    ReadCount = "8/10",
                    Content = "Cuộc họp định kỳ tháng 9 sẽ diễn ra vào..."
                },
                new NotificationRecord
                {
                    Id = 3,
                    Title = "Cập nhật quy định mới về chấm công",
                    Sender = "Phòng Nhân sự",
                    Recipients = "Tất cả nhân viên",
                    Priority = "Khẩn cấp",
                    SentDate = DateTime.Now,
                    Status = "Nháp",
                    ReadCount = "0/50",
                    Content = "Từ ngày 1/10, công ty sẽ áp dụng quy định mới..."
                }
            };
        }

        private void LoadNotificationList()
        {
            dgNotifications.ItemsSource = notifications;
        }

        private void BtnAttachFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "All files (*.*)|*.*|Documents (*.pdf;*.doc;*.docx)|*.pdf;*.doc;*.docx|Images (*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif";

            if (openFileDialog.ShowDialog() == true)
            {
                foreach (string filename in openFileDialog.FileNames)
                {
                    if (!attachedFiles.Contains(filename))
                    {
                        attachedFiles.Add(filename);
                        lbAttachedFiles.Items.Add(Path.GetFileName(filename));
                    }
                }
            }
        }

        private void BtnRemoveFile_Click(object sender, RoutedEventArgs e)
        {
            if (lbAttachedFiles.SelectedItem != null)
            {
                int selectedIndex = lbAttachedFiles.SelectedIndex;
                lbAttachedFiles.Items.RemoveAt(selectedIndex);
                attachedFiles.RemoveAt(selectedIndex);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn file cần xóa!", "Cảnh báo", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateNotificationForm())
            {
                var result = MessageBox.Show(
                    "Bạn có chắc chắn muốn gửi thông báo này?",
                    "Xác nhận gửi thông báo",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    // TODO: Lưu thông báo vào database và gửi
                    MessageBox.Show("Gửi thông báo thành công!", "Thông báo", 
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    
                    ClearForm();
                    LoadNotificationList(); // Refresh danh sách
                }
            }
        }

        private void BtnSaveDraft_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                // TODO: Lưu thông báo dưới dạng nháp
                MessageBox.Show("Lưu nháp thành công!", "Thông báo", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Vui lòng nhập tiêu đề thông báo!", "Cảnh báo", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa toàn bộ nội dung đã nhập?",
                "Xác nhận xóa",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                ClearForm();
            }
        }

        private void BtnFilterNotif_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Lọc thông báo theo điều kiện
            MessageBox.Show("Chức năng lọc đang được phát triển!", "Thông báo", 
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BtnViewDetail_Click(object sender, RoutedEventArgs e)
        {
            if (dgNotifications.SelectedItem is NotificationRecord selectedNotification)
            {
                ShowNotificationDetail(selectedNotification);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn thông báo cần xem!", "Cảnh báo", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnDeleteNotif_Click(object sender, RoutedEventArgs e)
        {
            if (dgNotifications.SelectedItem is NotificationRecord selectedNotification)
            {
                var result = MessageBox.Show(
                    $"Bạn có chắc chắn muốn xóa thông báo '{selectedNotification.Title}'?",
                    "Xác nhận xóa",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    // TODO: Xóa thông báo khỏi database
                    notifications.Remove(selectedNotification);
                    LoadNotificationList();
                    MessageBox.Show("Xóa thông báo thành công!", "Thông báo", 
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn thông báo cần xóa!", "Cảnh báo", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadNotificationList();
            MessageBox.Show("Làm mới danh sách thành công!", "Thông báo", 
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void DgNotifications_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (dgNotifications.SelectedItem is NotificationRecord selectedNotification)
            {
                ShowNotificationDetail(selectedNotification);
            }
        }

        private bool ValidateNotificationForm()
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("Vui lòng nhập tiêu đề thông báo!", "Cảnh báo", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                txtTitle.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtContent.Text))
            {
                MessageBox.Show("Vui lòng nhập nội dung thông báo!", "Cảnh báo", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                txtContent.Focus();
                return false;
            }

            if (rbDepartment.IsChecked == true && cbDepartment.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn phòng ban!", "Cảnh báo", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (rbSpecific.IsChecked == true && lbEmployees.SelectedItems.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một nhân viên!", "Cảnh báo", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        private void ClearForm()
        {
            txtTitle.Clear();
            txtContent.Clear();
            cbPriority.SelectedIndex = 0;
            rbAllEmployees.IsChecked = true;
            cbDepartment.SelectedIndex = -1;
            lbEmployees.UnselectAll();
            lbAttachedFiles.Items.Clear();
            attachedFiles.Clear();
        }

        private void ShowNotificationDetail(NotificationRecord notification)
        {
            string detail = $"ID: {notification.Id}\n" +
                           $"Tiêu đề: {notification.Title}\n" +
                           $"Người gửi: {notification.Sender}\n" +
                           $"Người nhận: {notification.Recipients}\n" +
                           $"Mức độ: {notification.Priority}\n" +
                           $"Ngày gửi: {notification.SentDate:dd/MM/yyyy HH:mm}\n" +
                           $"Trạng thái: {notification.Status}\n" +
                           $"Đã đọc: {notification.ReadCount}\n\n" +
                           $"Nội dung:\n{notification.Content}";

            MessageBox.Show(detail, "Chi tiết thông báo", 
                MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }

    // Model class cho dữ liệu thông báo
    public class NotificationRecord
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Sender { get; set; }
        public string Recipients { get; set; }
        public string Priority { get; set; }
        public DateTime SentDate { get; set; }
        public string Status { get; set; }
        public string ReadCount { get; set; }
        public string Content { get; set; }
    }
}