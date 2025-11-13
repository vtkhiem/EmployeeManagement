# Hướng dẫn sử dụng Quản lý Nhân viên

## Tính năng đã triển khai

### 1. Quản lý thông tin nhân viên
- ✅ **Thêm nhân viên mới**: Nhập đầy đủ thông tin nhân viên
- ✅ **Sửa thông tin**: Cập nhật thông tin nhân viên đã có
- ✅ **Xóa nhân viên**: Xóa nhân viên khỏi hệ thống (có xác nhận)
- ✅ **Xem chi tiết**: Xem đầy đủ thông tin nhân viên (chế độ chỉ đọc)

### 2. Thông tin quản lý
Các trường thông tin bao gồm:
- Họ tên (bắt buộc)
- Ngày sinh
- Giới tính (Nam/Nữ/Khác)
- Địa chỉ
- Số điện thoại
- Email (bắt buộc)
- Phòng ban
- Chức vụ
- Mức lương (VNĐ)
- Ngày bắt đầu làm việc (bắt buộc)
- Trạng thái (Active/Inactive/On Leave)
- Ảnh đại diện

### 3. Tìm kiếm nhân viên
- 🔍 **Tìm kiếm theo tên**: Nhập tên nhân viên vào ô tìm kiếm và nhấn nút "Tìm kiếm"
- Kết quả hiển thị tất cả nhân viên có tên chứa từ khóa tìm kiếm

### 4. Lọc nhân viên nâng cao
Bộ lọc hỗ trợ các tiêu chí:
- **Phòng ban**: Lọc theo phòng ban cụ thể
- **Giới tính**: Lọc theo Nam/Nữ/Khác
- **Mức lương**: Lọc theo khoảng lương (từ - đến)
- **Ngày làm việc**: Lọc theo khoảng thời gian bắt đầu làm việc

### 5. Quản lý ảnh đại diện
- 📷 **Tải ảnh lên**: Chọn file ảnh từ máy tính (hỗ trợ .jpg, .jpeg, .png)
- 🗑️ **Xóa ảnh**: Xóa ảnh đại diện hiện tại
- Ảnh được lưu trong thư mục `ProfilePictures` tại thư mục gốc ứng dụng

## Cách sử dụng

### Truy cập chức năng
1. Đăng nhập với tài khoản Admin
2. Tại Dashboard, nhấn vào nút **"Quản lý nhân viên"**

### Thêm nhân viên mới
1. Nhấn nút **"➕ Thêm nhân viên"**
2. Điền đầy đủ thông tin (các trường có dấu * là bắt buộc)
3. Tải ảnh đại diện nếu muốn
4. Nhấn **"💾 Lưu"** để hoàn tất

**Lưu ý**: Mật khẩu mặc định cho nhân viên mới là `123456`

### Sửa thông tin nhân viên
1. Chọn nhân viên trong danh sách
2. Nhấn nút **"✏️ Sửa thông tin"**
3. Cập nhật thông tin cần thiết
4. Nhấn **"💾 Lưu"** để hoàn tất

### Xem chi tiết nhân viên
- **Cách 1**: Chọn nhân viên và nhấn **"👁️ Xem chi tiết"**
- **Cách 2**: Double-click vào dòng nhân viên trong danh sách

### Xóa nhân viên
1. Chọn nhân viên cần xóa
2. Nhấn nút **"🗑️ Xóa nhân viên"**
3. Xác nhận xóa trong hộp thoại

### Tìm kiếm nhân viên
1. Nhập tên nhân viên vào ô tìm kiếm
2. Nhấn nút **"Tìm kiếm"**
3. Để xem lại toàn bộ danh sách, xóa nội dung tìm kiếm và tìm lại

### Lọc nhân viên
1. Nhấn vào **"🔽 Bộ lọc nâng cao"** để mở rộng
2. Chọn các tiêu chí lọc mong muốn
3. Nhấn **"Áp dụng lọc"**
4. Để xóa bộ lọc, nhấn **"Xóa lọc"**

## Cấu trúc dữ liệu

### Employee Model
```csharp
- EmployeeId: int (Primary Key)
- FullName: string (Required)
- DateOfBirth: DateOnly?
- Gender: string?
- Address: string?
- PhoneNumber: string?
- Email: string (Required)
- ProfilePicturePath: string?
- HireDate: DateOnly (Required)
- EmploymentStatus: string?
- BaseSalary: decimal?
- DepartmentId: int?
- PositionId: int?
- PasswordHash: string?
```

## Các service đã triển khai

### IEmployeeService
```csharp
- GetAllEmployees(): Lấy danh sách tất cả nhân viên
- GetEmployeeById(int id): Lấy thông tin nhân viên theo ID
- AddEmployee(Employee): Thêm nhân viên mới
- UpdateEmployee(Employee): Cập nhật thông tin nhân viên
- DeleteEmployee(int id): Xóa nhân viên
- SearchEmployeesByName(string): Tìm kiếm nhân viên theo tên
- FilterEmployees(...): Lọc nhân viên theo nhiều tiêu chí
```

## Ghi chú kỹ thuật

### Validation
- Email phải là duy nhất trong hệ thống
- Các trường bắt buộc: Họ tên, Email, Ngày bắt đầu làm việc
- Mật khẩu được mã hóa bằng BCrypt

### Upload ảnh
- Ảnh được lưu trong thư mục `ProfilePictures`
- Tên file được tạo tự động bằng GUID để tránh trùng lặp
- Hỗ trợ định dạng: .jpg, .jpeg, .png

### Hiệu suất
- Sử dụng LINQ để lọc dữ liệu
- DataGrid hỗ trợ sắp xếp và cuộn mượt mà
- Lazy loading cho các quan hệ (Department, Position)
