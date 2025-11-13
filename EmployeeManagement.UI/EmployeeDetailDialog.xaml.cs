using EmployeeManagement.BLL.Services;
using EmployeeManagement.DAL.Models;
using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Windows;
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
                GenderComboBox.Text = employee.Gender;
                AddressTextBox.Text = employee.Address;
                PhoneNumberTextBox.Text = employee.PhoneNumber;
                EmailTextBox.Text = employee.Email;
                DepartmentComboBox.SelectedValue = employee.DepartmentId;
                PositionComboBox.SelectedValue = employee.PositionId;
                BaseSalaryTextBox.Text = employee.BaseSalary?.ToString();
                HireDatePicker.SelectedDate = employee.HireDate.ToDateTime(TimeOnly.MinValue);
                EmploymentStatusComboBox.Text = employee.EmploymentStatus;
                _profilePicturePath = employee.ProfilePicturePath;

                // Load profile picture
                if (!string.IsNullOrEmpty(employee.ProfilePicturePath) && File.Exists(employee.ProfilePicturePath))
                {
                    try
                    {
                        BitmapImage bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.UriSource = new Uri(employee.ProfilePicturePath, UriKind.Absolute);
                        bitmap.CacheOption = BitmapCacheOption.OnLoad;
                        bitmap.EndInit();
                        ProfilePictureImage.Source = bitmap;
                        ProfilePictureImage.Visibility = Visibility.Visible;
                        PlaceholderText.Visibility = Visibility.Collapsed;
                    }
                    catch { }
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

                    // Copy file
                    File.Copy(sourceFile, destinationFile, true);
                    _profilePicturePath = destinationFile;

                    // Display image
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(destinationFile, UriKind.Absolute);
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.EndInit();
                    ProfilePictureImage.Source = bitmap;
                    ProfilePictureImage.Visibility = Visibility.Visible;
                    PlaceholderText.Visibility = Visibility.Collapsed;
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
                    
                    // Set default password for new employee
                    string defaultPassword = string.IsNullOrWhiteSpace(PasswordBox.Password) 
                        ? "123456" 
                        : PasswordBox.Password;
                    employee.PasswordHash = BCrypt.Net.BCrypt.HashPassword(defaultPassword);
                }

                // Update employee data
                employee.FullName = FullNameTextBox.Text.Trim();
                employee.DateOfBirth = DateOfBirthPicker.SelectedDate.HasValue 
                    ? DateOnly.FromDateTime(DateOfBirthPicker.SelectedDate.Value) 
                    : null;
                employee.Gender = GenderComboBox.Text;
                employee.Address = AddressTextBox.Text?.Trim();
                employee.PhoneNumber = PhoneNumberTextBox.Text?.Trim();
                employee.Email = EmailTextBox.Text.Trim();
                employee.DepartmentId = DepartmentComboBox.SelectedValue as int?;
                employee.PositionId = PositionComboBox.SelectedValue as int?;
                
                if (decimal.TryParse(BaseSalaryTextBox.Text, out decimal salary))
                {
                    employee.BaseSalary = salary;
                }
                
                employee.HireDate = DateOnly.FromDateTime(HireDatePicker.SelectedDate.Value);
                employee.EmploymentStatus = EmploymentStatusComboBox.Text;
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
