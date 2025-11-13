using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using EmployeeManagement.BLL.Services;
using EmployeeManagement.DAL.Models;

namespace EmployeeManagement.UI
{
    public partial class AssignEmployeeDialog : Window
    {
        private readonly IEmployeeService _employeeService;
        private readonly Department _department;
        private List<Employee> _allEmployees;

        public AssignEmployeeDialog(IEmployeeService employeeService, Department department)
        {
            InitializeComponent();
            _employeeService = employeeService;
            _department = department;

            txtDepartmentInfo.Text = $"{_department.DepartmentName} (ID: {_department.DepartmentId})";
            LoadEmployees();
        }

        private void LoadEmployees()
        {
            try
            {
                _allEmployees = _employeeService.GetAllEmployees().ToList();
                dgEmployees.ItemsSource = _allEmployees;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách nhân viên: {ex.Message}",
                    "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterEmployees();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            FilterEmployees();
        }

        private void FilterEmployees()
        {
            if (_allEmployees == null) return;

            string searchText = txtSearch.Text.ToLower().Trim();

            if (string.IsNullOrEmpty(searchText))
            {
                dgEmployees.ItemsSource = _allEmployees;
            }
            else
            {
                var filtered = _allEmployees.Where(emp =>
                    emp.FullName.ToLower().Contains(searchText) ||
                    emp.Email.ToLower().Contains(searchText) ||
                    emp.EmployeeId.ToString().Contains(searchText)
                ).ToList();

                dgEmployees.ItemsSource = filtered;
            }
        }

        private void AssignButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgEmployees.SelectedItem is Employee selectedEmployee)
                {
                    var result = MessageBox.Show(
                        $"Bạn có chắc chắn muốn gán nhân viên '{selectedEmployee.FullName}' vào phòng ban '{_department.DepartmentName}'?",
                        "Xác nhận",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        // Cập nhật phòng ban cho nhân viên
                        selectedEmployee.DepartmentId = _department.DepartmentId;
                        _employeeService.UpdateEmployee(selectedEmployee);

                        MessageBox.Show("Gán nhân viên vào phòng ban thành công!",
                            "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);

                        this.DialogResult = true;
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn nhân viên cần gán!",
                        "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi gán nhân viên: {ex.Message}",
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
