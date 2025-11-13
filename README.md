# HỆ THỐNG QUẢN LÝ NHÂN VIÊN

Ứng dụng quản lý nhân viên được xây dựng bằng WPF (.NET 8.0) với kiến trúc 3 lớp.

## 📋 Mục lục
- [Yêu cầu hệ thống](#yêu-cầu-hệ-thống)
- [Cài đặt](#cài-đặt)
- [Cấu hình Database](#cấu-hình-database)
- [Chạy ứng dụng](#chạy-ứng-dụng)
- [Tài khoản test](#tài-khoản-test)
- [Tính năng](#tính-năng)

## 🖥️ Yêu cầu hệ thống

### Phần mềm cần thiết:
- **Windows 10/11** (64-bit)
- **.NET 8.0 SDK** - [Tải tại đây](https://dotnet.microsoft.com/download/dotnet/8.0)
- **SQL Server 2019** trở lên hoặc **SQL Server Express** - [Tải tại đây](https://www.microsoft.com/sql-server/sql-server-downloads)
- **Visual Studio 2022** (khuyến nghị) hoặc **Visual Studio Code**

### Kiểm tra .NET đã cài đặt:
```bash
dotnet --version
```
Kết quả phải là `8.0.x` trở lên.

## 📦 Cài đặt

### 1. Clone hoặc tải project về máy

```bash
git clone <repository-url>
cd EmployeeManagement
```

### 2. Cài đặt các NuGet packages

Project sử dụng các thư viện sau:

**EmployeeManagement.DAL:**
- Microsoft.EntityFrameworkCore (8.0.10)
- Microsoft.EntityFrameworkCore.SqlServer (8.0.10)
- Microsoft.EntityFrameworkCore.Tools (8.0.10)

**EmployeeManagement.BLL:**
- BCrypt.Net-Next (4.0.3)

**EmployeeManagement.UI:**
- Microsoft.Extensions.DependencyInjection (9.0.0)
- Microsoft.Extensions.Hosting (9.0.0)

Cài đặt tất cả packages:
```bash
dotnet restore
```

## 🗄️ Cấu hình Database

### 1. Tạo Database

Mở SQL Server Management Studio (SSMS) và chạy script sau:

```sql
CREATE DATABASE EmployeeManagementDB;
GO
```

### 2. Cấu hình Connection String

Mở file `EmployeeManagement.UI/appsettings.json` và cập nhật connection string:

```json
{
  "ConnectionStrings": {
    "PrnDb": "Server=YOUR_SERVER_NAME;Database=EmployeeManagementDB;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

**Lưu ý:** 
- Thay `YOUR_SERVER_NAME` bằng tên SQL Server của bạn
- Ví dụ: `localhost`, `.\SQLEXPRESS`, hoặc `(localdb)\MSSQLLocalDB`

### 3. Chạy Migration (nếu có)

```bash
cd EmployeeManagement.DAL
dotnet ef database update
```

## 🚀 Chạy ứng dụng

### Cách 1: Sử dụng Command Line

```bash
# Từ thư mục gốc của project
dotnet run --project EmployeeManagement.UI
```

### Cách 2: Sử dụng Visual Studio

1. Mở file `EmployeeManagement.sln`
2. Set `EmployeeManagement.UI` làm Startup Project (chuột phải → Set as Startup Project)
3. Nhấn `F5` hoặc click nút **Start**

### Cách 3: Build và chạy file .exe

```bash
# Build project
dotnet build --configuration Release

# Chạy file exe
cd EmployeeManagement.UI/bin/Release/net8.0-windows
./EmployeeManagement.UI.exe
```

## 👤 Tài khoản test

### Admin Account:
- **Username:** `admin`
- **Password:** `admin123`

### Employee Accounts:
- **Username:** `employee1` | **Password:** `emp123`
- **Username:** `employee2` | **Password:** `emp123`

**Lưu ý:** Đây là dữ liệu mẫu. Trong môi trường production, cần thay đổi mật khẩu mạnh hơn.

## ✨ Tính năng

### 1. Dashboard
- Hiển thị tổng quan hệ thống
- 7 cards chức năng chính
- Thời gian thực

### 2. Quản lý Nhân viên
- Thêm, sửa, xóa thông tin nhân viên
- Tìm kiếm và lọc
- Quản lý phòng ban, chức vụ

### 3. Chấm công
- Chấm công vào/ra
- Lịch sử chấm công
- Tính toán tự động giờ làm việc
- Phát hiện đi muộn/về sớm
- Xuất báo cáo

### 4. Thông báo nội bộ
- Gửi thông báo cho nhân viên
- Chọn người nhận (tất cả/theo phòng ban/cụ thể)
- Đính kèm file
- Quản lý danh sách thông báo
- Lưu nháp

### 5. Nghỉ phép (Đang phát triển)
- Đơn xin nghỉ phép
- Duyệt đơn nghỉ phép

### 6. Báo cáo (Đang phát triển)
- Báo cáo nhân viên
- Báo cáo lương
- Báo cáo chấm công

## 🏗️ Kiến trúc

```
EmployeeManagement/
├── EmployeeManagement.DAL/      # Data Access Layer
│   ├── Models/                  # Entity models
│   ├── Repositories/            # Repository pattern
│   └── Prn212Context.cs        # DbContext
├── EmployeeManagement.BLL/      # Business Logic Layer
│   └── Services/                # Business services
├── EmployeeManagement.UI/       # Presentation Layer (WPF)
│   ├── MainWindow.xaml         # Màn hình chính
│   ├── AttendanceWindow.xaml   # Chấm công
│   └── NotificationWindow.xaml # Thông báo
└── PasswordHasherUtility/       # Utility for password hashing
```

## 🐛 Xử lý lỗi thường gặp

### Lỗi: "Cannot connect to SQL Server"
- Kiểm tra SQL Server đã chạy chưa
- Kiểm tra connection string trong `appsettings.json`
- Kiểm tra firewall

### Lỗi: "The type initializer for 'Microsoft.Data.SqlClient.SNI.SNILoadHandle' threw an exception"
- Cài đặt Visual C++ Redistributable
- Tải tại: https://aka.ms/vs/17/release/vc_redist.x64.exe

### Lỗi: "Could not load file or assembly"
- Chạy lại: `dotnet restore`
- Xóa thư mục `bin` và `obj`, sau đó build lại

## 📝 License

Dự án này được phát triển cho mục đích học tập.

## 👥 Đóng góp

Mọi đóng góp đều được chào đón. Vui lòng tạo Pull Request hoặc Issue.

## 📞 Liên hệ

Nếu có vấn đề, vui lòng tạo Issue trên GitHub.
