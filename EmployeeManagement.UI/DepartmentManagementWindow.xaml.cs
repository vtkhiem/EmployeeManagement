using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using EmployeeManagement.BLL.Services;
using EmployeeManagement.DAL.Models;

namespace EmployeeManagement.UI
{
    public partial class DepartmentManagementWindow : Window
    {
        // 1. Khai báo service cần dùng (DI sẽ inject)
        private readonly IDepartmentService _departmentService;
        private readonly IEmployeeService _employeeService;
        private readonly IServiceProvider _serviceProvider;
        private Department? _selectedDepartment;

        // 2. Constructor nhận service qua Dependency Injection
        public DepartmentManagementWindow(
            IDepartmentService departmentService, 
            IEmployeeService employeeService,
            IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _departmentService = departmentService;
            _employeeService = employeeService;
            _serviceProvider = serviceProvider;

            // 3. Load dữ liệu ban đầu
            LoadDepartments();
        }

        // Load danh sách phòng ban
        private void LoadDepartments()
        {
            try
            {
                var departments = _departmentService.GetAllDepartments().ToList();
                dgDepartments.ItemsSource = departments;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách phòng ban: {ex.Message}",
                    "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Khi chọn một phòng ban trong DataGrid
        private void DgDepartments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgDepartments.SelectedItem is Department department)
            {
                _selectedDepartment = department;
                txtDepartmentId.Text = department.DepartmentId.ToString();
                txtDepartmentName.Text = department.DepartmentName;
                txtEmployeeCount.Text = department.Employees.Count.ToString();

                // Load danh sách nhân viên trong phòng ban
                LoadEmployeesInDepartment(department.DepartmentId);
            }
        }

        // Load danh sách nhân viên trong phòng ban
        private void LoadEmployeesInDepartment(int departmentId)
        {
            try
            {
                var employees = _departmentService.GetEmployeesInDepartment(departmentId).ToList();
                dgEmployeesInDepartment.ItemsSource = employees;
                LoadDepartments(); // Cập nhật lại danh sách phòng ban để hiển thị số lượng nhân viên đúng
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách nhân viên: {ex.Message}",
                    "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Thêm phòng ban mới
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtDepartmentName.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên phòng ban!",
                        "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var newDepartment = new Department
                {
                    DepartmentName = txtDepartmentName.Text.Trim()
                };

                _departmentService.AddDepartment(newDepartment);

                MessageBox.Show("Thêm phòng ban thành công!",
                    "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);

                LoadDepartments();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm phòng ban: {ex.Message}",
                    "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Cập nhật phòng ban
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_selectedDepartment == null)
                {
                    MessageBox.Show("Vui lòng chọn phòng ban cần cập nhật!",
                        "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtDepartmentName.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên phòng ban!",
                        "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                _selectedDepartment.DepartmentName = txtDepartmentName.Text.Trim();
                _departmentService.UpdateDepartment(_selectedDepartment);

                MessageBox.Show("Cập nhật phòng ban thành công!",
                    "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);

                LoadDepartments();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật phòng ban: {ex.Message}",
                    "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Xóa phòng ban
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_selectedDepartment == null)
                {
                    MessageBox.Show("Vui lòng chọn phòng ban cần xóa!",
                        "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var result = MessageBox.Show(
                    $"Bạn có chắc chắn muốn xóa phòng ban '{_selectedDepartment.DepartmentName}'?",
                    "Xác nhận xóa",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    _departmentService.DeleteDepartment(_selectedDepartment.DepartmentId);

                    MessageBox.Show("Xóa phòng ban thành công!",
                        "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);

                    LoadDepartments();
                    ClearForm();
                }
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message,
                    "Không thể xóa", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa phòng ban: {ex.Message}",
                    "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Làm mới form
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            txtDepartmentId.Clear();
            txtDepartmentName.Clear();
            txtEmployeeCount.Clear();
            _selectedDepartment = null;
            dgDepartments.SelectedItem = null;
        }

        // Làm mới danh sách
        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            LoadDepartments();
            ClearForm();
        }

        // Quay lại Dashboard
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // Mở dialog gán nhân viên
        private void AssignEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_selectedDepartment == null)
                {
                    MessageBox.Show("Vui lòng chọn phòng ban trước!",
                        "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var dialog = new AssignEmployeeDialog(_employeeService, _selectedDepartment);
                var result = dialog.ShowDialog();

                if (result == true)
                {
                    // Reload dữ liệu sau khi gán thành công
                    LoadDepartments();
                    if (_selectedDepartment != null)
                    {
                        LoadEmployeesInDepartment(_selectedDepartment.DepartmentId);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}",
                    "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
