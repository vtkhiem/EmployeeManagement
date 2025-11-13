using EmployeeManagement.BLL.Services;
using EmployeeManagement.DAL.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace EmployeeManagement.UI
{
    public partial class EmployeeManagementWindow : Window
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;
        private readonly IServiceProvider _serviceProvider;

        public EmployeeManagementWindow(IEmployeeService employeeService, IDepartmentService departmentService, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _employeeService = employeeService;
            _departmentService = departmentService;
            _serviceProvider = serviceProvider;
            
            LoadData();
        }

        private void LoadData()
        {
            LoadEmployees();
            LoadDepartments();
        }

        private void LoadEmployees()
        {
            try
            {
                var employees = _employeeService.GetAllEmployees().ToList();
                EmployeeDataGrid.ItemsSource = employees;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách nhân viên: {ex.Message}", 
                    "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadDepartments()
        {
            try
            {
                var departments = _departmentService.GetAllDepartments().ToList();
                departments.Insert(0, new Department { DepartmentId = 0, DepartmentName = "Tất cả" });
                DepartmentFilterComboBox.ItemsSource = departments;
                DepartmentFilterComboBox.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách phòng ban: {ex.Message}", 
                    "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string searchText = SearchTextBox.Text.Trim();
                
                if (string.IsNullOrEmpty(searchText))
                {
                    LoadEmployees();
                    return;
                }

                var employees = _employeeService.SearchEmployeesByName(searchText).ToList();
                EmployeeDataGrid.ItemsSource = employees;
                
                if (employees.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy nhân viên nào phù hợp.", 
                        "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm: {ex.Message}", 
                    "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ApplyFilterButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int? departmentId = null;
                if (DepartmentFilterComboBox.SelectedValue != null && (int)DepartmentFilterComboBox.SelectedValue > 0)
                {
                    departmentId = (int)DepartmentFilterComboBox.SelectedValue;
                }

                string? gender = null;
                if (GenderFilterComboBox.SelectedIndex > 0)
                {
                    gender = ((ComboBoxItem)GenderFilterComboBox.SelectedItem).Content.ToString();
                }

                decimal? minSalary = null;
                if (!string.IsNullOrEmpty(MinSalaryTextBox.Text) && decimal.TryParse(MinSalaryTextBox.Text, out decimal min))
                {
                    minSalary = min;
                }

                decimal? maxSalary = null;
                if (!string.IsNullOrEmpty(MaxSalaryTextBox.Text) && decimal.TryParse(MaxSalaryTextBox.Text, out decimal max))
                {
                    maxSalary = max;
                }

                DateOnly? fromDate = null;
                if (FromDatePicker.SelectedDate.HasValue)
                {
                    fromDate = DateOnly.FromDateTime(FromDatePicker.SelectedDate.Value);
                }

                DateOnly? toDate = null;
                if (ToDatePicker.SelectedDate.HasValue)
                {
                    toDate = DateOnly.FromDateTime(ToDatePicker.SelectedDate.Value);
                }

                var employees = _employeeService.FilterEmployees(departmentId, gender, minSalary, maxSalary, fromDate, toDate).ToList();
                EmployeeDataGrid.ItemsSource = employees;

                if (employees.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy nhân viên nào phù hợp với bộ lọc.", 
                        "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lọc dữ liệu: {ex.Message}", 
                    "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearFilterButton_Click(object sender, RoutedEventArgs e)
        {
            DepartmentFilterComboBox.SelectedIndex = 0;
            GenderFilterComboBox.SelectedIndex = 0;
            MinSalaryTextBox.Clear();
            MaxSalaryTextBox.Clear();
            FromDatePicker.SelectedDate = null;
            ToDatePicker.SelectedDate = null;
            SearchTextBox.Clear();
            LoadEmployees();
        }

        private void AddEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = _serviceProvider.GetRequiredService<EmployeeDetailDialog>();
            dialog.Owner = this;
            if (dialog.ShowDialog() == true)
            {
                LoadEmployees();
            }
        }

        private void EditEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeeDataGrid.SelectedItem is Employee selectedEmployee)
            {
                var dialog = _serviceProvider.GetRequiredService<EmployeeDetailDialog>();
                dialog.LoadEmployee(selectedEmployee.EmployeeId);
                dialog.Owner = this;
                if (dialog.ShowDialog() == true)
                {
                    LoadEmployees();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần sửa.", 
                    "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void ViewEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeeDataGrid.SelectedItem is Employee selectedEmployee)
            {
                var dialog = _serviceProvider.GetRequiredService<EmployeeDetailDialog>();
                dialog.LoadEmployee(selectedEmployee.EmployeeId, isViewOnly: true);
                dialog.Owner = this;
                dialog.ShowDialog();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần xem.", 
                    "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DeleteEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeeDataGrid.SelectedItem is Employee selectedEmployee)
            {
                var result = MessageBox.Show(
                    $"Bạn có chắc chắn muốn xóa nhân viên '{selectedEmployee.FullName}'?\n\nLưu ý: Thao tác này không thể hoàn tác!", 
                    "Xác nhận xóa", 
                    MessageBoxButton.YesNo, 
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        _employeeService.DeleteEmployee(selectedEmployee.EmployeeId);
                        MessageBox.Show("Xóa nhân viên thành công!", 
                            "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadEmployees();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi xóa nhân viên: {ex.Message}", 
                            "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần xóa.", 
                    "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void EmployeeDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (EmployeeDataGrid.SelectedItem is Employee selectedEmployee)
            {
                var dialog = _serviceProvider.GetRequiredService<EmployeeDetailDialog>();
                dialog.LoadEmployee(selectedEmployee.EmployeeId, isViewOnly: true);
                dialog.Owner = this;
                dialog.ShowDialog();
            }
        }
    }
}
