using EmployeeManagement.BLL.Services;
using EmployeeManagement.DAL.Models;
using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace EmployeeManagement.UI
{
    public partial class EmployeeDetailDialog : Window
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;
        private readonly IPositionService _positionService;
        private int? _employeeId;
        private bool _isViewOnly;
        private string? _profilePicturePath;

        public EmployeeDetailDialog(IEmployeeService employeeService, IDepartmentService departmentService, IPositionService positionService)
        {
            InitializeComponent();
            _employeeService = employeeService;
            _departmentService = departmentService;
            _positionService = positionService;

            LoadComboBoxData();
            SetDefaultValues();

            // Set initial placeholder visibility
            PlaceholderText.Visibility = Visibility.Visible;
            ProfilePictureImage.Visibility = Visibility.Collapsed;
        }

        private void LoadComboBoxData()
        {
            try
            {
                var departments = _departmentService.GetAllDepartments().ToList();
                DepartmentComboBox.ItemsSource = departments;

                var positions = _positionService.GetAllPositions().ToList();
                PositionComboBox.ItemsSource = positions;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}",
                    "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SetDefaultValues()
        {
            HireDatePicker.SelectedDate = DateTime.Now;
            GenderComboBox.SelectedIndex = 0;
            EmploymentStatusComboBox.SelectedIndex = 0;
            BaseSalaryTextBox.Text = "0"; // Gán giá trị mặc định cho Lương
        }

        public void LoadEmployee(int employeeId, bool isViewOnly = false)
        {
            _employeeId = employeeId;
            _isViewOnly = isViewOnly;

            try
            {
                var employee = _employeeService.GetEmployeeById(employeeId);
                if (employee == null)
                {
                    MessageBox.Show("Không tìm thấy nhân viên!",
                        "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    this.Close();
                    return;
                }

                // Load data to form
                FullNameTextBox.Text = employee.FullName;
                DateOfBirthPicker.SelectedDate = employee.DateOfBirth.HasValue
                    ? employee.DateOfBirth.Value.ToDateTime(TimeOnly.MinValue)
                    : null;

                // Set ComboBox Text
                SetComboBoxSelectedValue(GenderComboBox, employee.Gender);
                SetComboBoxSelectedValue(EmploymentStatusComboBox, employee.EmploymentStatus);

                AddressTextBox.Text = employee.Address;
                PhoneNumberTextBox.Text = employee.PhoneNumber;
                EmailTextBox.Text = employee.Email;

                // Use SelectedValue for data-bound ComboBoxes
                DepartmentComboBox.SelectedValue = employee.DepartmentId;
                PositionComboBox.SelectedValue = employee.PositionId;

                BaseSalaryTextBox.Text = employee.BaseSalary?.ToString("N0") ?? "0"; // Format lương
                HireDatePicker.SelectedDate = employee.HireDate.ToDateTime(TimeOnly.MinValue);
                _profilePicturePath = employee.ProfilePicturePath;

                // Load profile picture
                if (!string.IsNullOrEmpty(employee.ProfilePicturePath) && File.Exists(employee.ProfilePicturePath))
                {
                    try
                    {
                        // Use FileStream to prevent file locking issue
                        using (var stream = new FileStream(employee.ProfilePicturePath, FileMode.Open, FileAccess.Read))
                        {
                            BitmapImage bitmap = new BitmapImage();
                            bitmap.BeginInit();
                            bitmap.StreamSource = stream;
                            bitmap.CacheOption = BitmapCacheOption.OnLoad;
                            bitmap.EndInit();
                            ProfilePictureImage.Source = bitmap;
                            ProfilePictureImage.Visibility = Visibility.Visible;
                            PlaceholderText.Visibility = Visibility.Collapsed;
                        }
                    }
                    catch { /* Ignore error, keep placeholder visible */ }
                }

                // Hide password panel when editing
                PasswordPanel.Visibility = Visibility.Collapsed;

                // Update header
                HeaderTextBlock.Text = isViewOnly ? "XEM CHI TIẾT NHÂN VIÊN" : "SỬA THÔNG TIN NHÂN VIÊN";

                // Disable all controls if view only
                if (isViewOnly)
                {
                    DisableAllControls();
                    SaveButton.Visibility = Visibility.Collapsed;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải thông tin nhân viên: {ex.Message}",
                    "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Helper function to set ComboBox text based on Content (for non-data bound ComboBoxes)
        private void SetComboBoxSelectedValue(ComboBox comboBox, string value)
        {
            if (string.IsNullOrEmpty(value)) return;

            foreach (ComboBoxItem item in comboBox.Items)
            {
                if (item.Content.ToString() == value)
                {
                    comboBox.SelectedItem = item;
                    return;
                }
            }
        }

        private void DisableAllControls()
        {
            FullNameTextBox.IsReadOnly = true;
            DateOfBirthPicker.IsEnabled = false;
            GenderComboBox.IsEnabled = false;
            AddressTextBox.IsReadOnly = true;
            PhoneNumberTextBox.IsReadOnly = true;
            EmailTextBox.IsReadOnly = true;
            DepartmentComboBox.IsEnabled = false;
            PositionComboBox.IsEnabled = false;
            BaseSalaryTextBox.IsReadOnly = true;
            HireDatePicker.IsEnabled = false;
            EmploymentStatusComboBox.IsEnabled = false;
            UploadPhotoButton.IsEnabled = false;
            RemovePhotoButton.IsEnabled = false;
            PasswordBox.IsEnabled = false;
        }

        private void UploadPhotoButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Filter = "Image files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png",
                    Title = "Chọn ảnh đại diện"
                };

                if (openFileDialog.ShowDialog() == true)
                {
                    string sourceFile = openFileDialog.FileName;

                    // Create ProfilePictures folder if not exists
                    string profilePicturesFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ProfilePictures");
                    if (!Directory.Exists(profilePicturesFolder))
                    {
                        Directory.CreateDirectory(profilePicturesFolder);
                    }

                    // Generate unique filename
                    string fileName = $"{Guid.NewGuid()}{Path.GetExtension(sourceFile)}";
                    string destinationFile = Path.Combine(profilePicturesFolder, fileName);

                    // Copy file (better to handle file stream to avoid locking if needed)
                    File.Copy(sourceFile, destinationFile, true);
                    _profilePicturePath = destinationFile;

                    // Display image using FileStream
                    using (var stream = new FileStream(destinationFile, FileMode.Open, FileAccess.Read))
                    {
                        BitmapImage bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.StreamSource = stream;
                        bitmap.CacheOption = BitmapCacheOption.OnLoad;
                        bitmap.EndInit();
                        ProfilePictureImage.Source = bitmap;
                        ProfilePictureImage.Visibility = Visibility.Visible;
                        PlaceholderText.Visibility = Visibility.Collapsed;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải ảnh: {ex.Message}",
                    "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RemovePhotoButton_Click(object sender, RoutedEventArgs e)
        {
            _profilePicturePath = null;
            ProfilePictureImage.Source = null;
            ProfilePictureImage.Visibility = Visibility.Collapsed;
            PlaceholderText.Visibility = Visibility.Visible;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Validate required fields
                if (string.IsNullOrWhiteSpace(FullNameTextBox.Text))
                {
                    MessageBox.Show("Vui lòng nhập họ tên!",
                        "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    FullNameTextBox.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(EmailTextBox.Text))
                {
                    MessageBox.Show("Vui lòng nhập email!",
                        "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    EmailTextBox.Focus();
                    return;
                }

                if (!HireDatePicker.SelectedDate.HasValue)
                {
                    MessageBox.Show("Vui lòng chọn ngày bắt đầu làm việc!",
                        "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!_employeeId.HasValue && string.IsNullOrWhiteSpace(PasswordBox.Password))
                {
                    MessageBox.Show("Vui lòng nhập mật khẩu cho nhân viên mới!",
                        "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    PasswordBox.Focus();
                    return;
                }
                // ===== VALIDATION START =====

                // 1️⃣ Họ tên
                if (string.IsNullOrWhiteSpace(FullNameTextBox.Text))
                {
                    MessageBox.Show("Vui lòng nhập họ tên!", "Cảnh báo",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    FullNameTextBox.Focus();
                    return;
                }

                // 2️⃣ Email
                if (string.IsNullOrWhiteSpace(EmailTextBox.Text))
                {
                    MessageBox.Show("Vui lòng nhập email!", "Cảnh báo",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    EmailTextBox.Focus();
                    return;
                }

                // Email định dạng hợp lệ
                if (!System.Text.RegularExpressions.Regex.IsMatch(
                        EmailTextBox.Text,
                        @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                {
                    MessageBox.Show("Email không hợp lệ!", "Cảnh báo",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    EmailTextBox.Focus();
                    return;
                }

                // 3️⃣ Số điện thoại (10 số, bắt đầu bằng 0)
                if (!string.IsNullOrWhiteSpace(PhoneNumberTextBox.Text))
                {
                    if (!System.Text.RegularExpressions.Regex.IsMatch(
                            PhoneNumberTextBox.Text,
                            @"^(0\d{9})$"))
                    {
                        MessageBox.Show("Số điện thoại phải gồm 10 số và bắt đầu bằng 0!",
                            "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        PhoneNumberTextBox.Focus();
                        return;
                    }
                }

                // 4️⃣ Ngày sinh không được > ngày hiện tại
                if (DateOfBirthPicker.SelectedDate.HasValue)
                {
                    if (DateOfBirthPicker.SelectedDate.Value > DateTime.Now)
                    {
                        MessageBox.Show("Ngày sinh không thể lớn hơn ngày hiện tại!",
                            "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }

                // 5️⃣ Ngày bắt đầu làm việc
                if (!HireDatePicker.SelectedDate.HasValue)
                {
                    MessageBox.Show("Vui lòng chọn ngày bắt đầu làm việc!",
                        "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Ngày bắt đầu phải >= ngày sinh (nếu có)
                if (DateOfBirthPicker.SelectedDate.HasValue)
                {
                    if (HireDatePicker.SelectedDate.Value <= DateOfBirthPicker.SelectedDate.Value)
                    {
                        MessageBox.Show("Ngày bắt đầu làm việc phải lớn hơn ngày sinh!",
                            "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }

                // 6️⃣ Chọn phòng ban
                if (DepartmentComboBox.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn phòng ban!",
                        "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // 7️⃣ Chọn chức vụ
                if (PositionComboBox.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn chức vụ!",
                        "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // 8️⃣ Lương
                string salaryText = BaseSalaryTextBox.Text.Replace(",", "").Replace(".", "");
                if (!decimal.TryParse(salaryText, out decimal salaryValue) || salaryValue < 0)
                {
                    MessageBox.Show("Mức lương phải là số và không âm!",
                        "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    BaseSalaryTextBox.Focus();
                    return;
                }

                // 9️⃣ Mật khẩu khi tạo mới
                if (!_employeeId.HasValue) // chế độ add
                {
                    if (string.IsNullOrWhiteSpace(PasswordBox.Password))
                    {
                        MessageBox.Show("Vui lòng nhập mật khẩu!", "Cảnh báo",
                            MessageBoxButton.OK, MessageBoxImage.Warning);
                        PasswordBox.Focus();
                        return;
                    }

                    if (PasswordBox.Password.Length < 6)
                    {
                        MessageBox.Show("Mật khẩu phải có ít nhất 6 ký tự!",
                            "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }

                // 1️⃣0️⃣ Trạng thái làm việc
                if (EmploymentStatusComboBox.SelectedItem == null)
                {
                    MessageBox.Show("Vui lòng chọn trạng thái làm việc!",
                        "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // ===== VALIDATION END =====


                // Create or update employee
                Employee employee;
                if (_employeeId.HasValue)
                {
                    employee = _employeeService.GetEmployeeById(_employeeId.Value);
                    if (employee == null)
                    {
                        MessageBox.Show("Không tìm thấy nhân viên!",
                            "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
                else
                {
                    employee = new Employee();

                    // Set password for new employee
                    string defaultPassword = PasswordBox.Password;
                    employee.PasswordHash = BCrypt.Net.BCrypt.HashPassword(defaultPassword);
                }

                // Update employee data
                employee.FullName = FullNameTextBox.Text.Trim();
                employee.DateOfBirth = DateOfBirthPicker.SelectedDate.HasValue
                    ? DateOnly.FromDateTime(DateOfBirthPicker.SelectedDate.Value)
                    : null;

                // Get ComboBox content/value
                employee.Gender = (GenderComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? "Other";
                employee.EmploymentStatus = (EmploymentStatusComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? "Active";

                employee.Address = AddressTextBox.Text?.Trim();
                employee.PhoneNumber = PhoneNumberTextBox.Text?.Trim();
                employee.Email = EmailTextBox.Text.Trim();

                // Check and set foreign keys
                employee.DepartmentId = DepartmentComboBox.SelectedValue is int deptId ? deptId : null;
                employee.PositionId = PositionComboBox.SelectedValue is int posId ? posId : null;

                // Parse BaseSalary
                // Remove formatting (like commas/periods for thousands separator) before parsing
                string cleanSalaryText = BaseSalaryTextBox.Text.Replace(",", "").Replace(".", "").Trim();

                if (decimal.TryParse(cleanSalaryText, out decimal salary))
                {
                    employee.BaseSalary = salary;
                }
                else
                {
                    employee.BaseSalary = 0;
                }

                employee.HireDate = DateOnly.FromDateTime(HireDatePicker.SelectedDate.Value);

                employee.ProfilePicturePath = _profilePicturePath;

                // Save to database
                if (_employeeId.HasValue)
                {
                    _employeeService.UpdateEmployee(employee);
                    MessageBox.Show("Cập nhật thông tin nhân viên thành công!",
                        "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    // For new employee, also initialize remaining leave days (as set in DB default)
                    _employeeService.AddEmployee(employee);
                    MessageBox.Show("Thêm nhân viên mới thành công!",
                        "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu thông tin: {ex.Message}",
                    "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}