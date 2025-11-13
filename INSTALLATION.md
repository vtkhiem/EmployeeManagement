# HƯỚNG DẪN CÀI ĐẶT CHI TIẾT

## 📋 Mục lục
1. [Cài đặt .NET SDK](#1-cài-đặt-net-sdk)
2. [Cài đặt SQL Server](#2-cài-đặt-sql-server)
3. [Cài đặt Visual Studio (Tùy chọn)](#3-cài-đặt-visual-studio)
4. [Clone và Setup Project](#4-clone-và-setup-project)
5. [Cấu hình Database](#5-cấu-hình-database)
6. [Cài đặt Dependencies](#6-cài-đặt-dependencies)

---

## 1. Cài đặt .NET SDK

### Windows:

1. Truy cập: https://dotnet.microsoft.com/download/dotnet/8.0
2. Tải **".NET 8.0 SDK"** (x64)
3. Chạy file cài đặt và làm theo hướng dẫn
4. Khởi động lại máy tính

### Kiểm tra cài đặt:

Mở Command Prompt hoặc PowerShell:
```bash
dotnet --version
```

Kết quả mong đợi: `8.0.xxx`

---

## 2. Cài đặt SQL Server

### Tùy chọn 1: SQL Server Express (Miễn phí, Khuyến nghị)

1. Truy cập: https://www.microsoft.com/sql-server/sql-server-downloads
2. Tải **SQL Server 2022 Express**
3. Chạy file cài đặt
4. Chọn **"Basic"** installation
5. Chấp nhận license và chọn thư mục cài đặt
6. Đợi quá trình cài đặt hoàn tất

### Tùy chọn 2: SQL Server Developer Edition (Miễn phí, Đầy đủ tính năng)

1. Tải SQL Server Developer Edition
2. Chọn **"Custom"** installation
3. Cài đặt với các tùy chọn mặc định

### Cài đặt SQL Server Management Studio (SSMS):

1. Truy cập: https://aka.ms/ssmsfullsetup
2. Tải và cài đặt SSMS
3. Khởi động lại máy tính sau khi cài đặt

### Kiểm tra SQL Server:

1. Mở **SQL Server Management Studio (SSMS)**
2. Server name: `localhost` hoặc `.\SQLEXPRESS`
3. Authentication: **Windows Authentication**
4. Click **Connect**

---

## 3. Cài đặt Visual Studio (Tùy chọn)

### Visual Studio 2022 Community (Miễn phí):

1. Truy cập: https://visualstudio.microsoft.com/downloads/
2. Tải **Visual Studio 2022 Community**
3. Chạy installer
4. Chọn workloads:
   - ✅ **.NET desktop development**
   - ✅ **Data storage and processing** (cho SQL Server tools)
5. Click **Install**

### Hoặc sử dụng Visual Studio Code:

1. Truy cập: https://code.visualstudio.com/
2. Tải và cài đặt VS Code
3. Cài đặt extensions:
   - C# Dev Kit
   - .NET Extension Pack

---

## 4. Clone và Setup Project

### Cách 1: Sử dụng Git

```bash
# Clone repository
git clone <repository-url>

# Di chuyển vào thư mục project
cd EmployeeManagement
```

### Cách 2: Tải ZIP

1. Tải file ZIP từ repository
2. Giải nén vào thư mục mong muốn
3. Mở Command Prompt tại thư mục đó

---

## 5. Cấu hình Database

### Bước 1: Tạo Database

Mở **SQL Server Management Studio (SSMS)** và chạy:

```sql
-- Tạo database
CREATE DATABASE EmployeeManagementDB;
GO

-- Sử dụng database
USE EmployeeManagementDB;
GO
```

### Bước 2: Cấu hình Connection String

1. Mở file `EmployeeManagement.UI/appsettings.json`
2. Tìm section `ConnectionStrings`
3. Cập nhật theo SQL Server của bạn:

**Với SQL Server Express:**
```json
{
  "ConnectionStrings": {
    "PrnDb": "Server=.\\SQLEXPRESS;Database=EmployeeManagementDB;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

**Với LocalDB:**
```json
{
  "ConnectionStrings": {
    "PrnDb": "Server=(localdb)\\MSSQLLocalDB;Database=EmployeeManagementDB;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

**Với SQL Server đầy đủ:**
```json
{
  "ConnectionStrings": {
    "PrnDb": "Server=localhost;Database=EmployeeManagementDB;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

**Với SQL Authentication:**
```json
{
  "ConnectionStrings": {
    "PrnDb": "Server=localhost;Database=EmployeeManagementDB;User Id=sa;Password=YourPassword;TrustServerCertificate=True;"
  }
}
```

### Bước 3: Chạy Migration (Nếu có)

```bash
# Di chuyển vào thư mục DAL
cd EmployeeManagement.DAL

# Cài đặt EF Core tools (nếu chưa có)
dotnet tool install --global dotnet-ef

# Chạy migration
dotnet ef database update

# Quay lại thư mục gốc
cd ..
```

---

## 6. Cài đặt Dependencies

### Restore tất cả NuGet packages:

```bash
# Từ thư mục gốc của project
dotnet restore
```

### Kiểm tra packages đã cài đặt:

```bash
dotnet list package
```

### Nếu thiếu package, cài đặt thủ công:

```bash
# Entity Framework Core
dotnet add EmployeeManagement.DAL package Microsoft.EntityFrameworkCore --version 8.0.10
dotnet add EmployeeManagement.DAL package Microsoft.EntityFrameworkCore.SqlServer --version 8.0.10
dotnet add EmployeeManagement.DAL package Microsoft.EntityFrameworkCore.Tools --version 8.0.10

# BCrypt for password hashing
dotnet add EmployeeManagement.BLL package BCrypt.Net-Next --version 4.0.3

# Dependency Injection
dotnet add EmployeeManagement.UI package Microsoft.Extensions.DependencyInjection --version 9.0.0
dotnet add EmployeeManagement.UI package Microsoft.Extensions.Hosting --version 9.0.0
```

---

## 7. Build Project

### Build toàn bộ solution:

```bash
dotnet build
```

### Build với configuration Release:

```bash
dotnet build --configuration Release
```

### Kiểm tra lỗi:

Nếu có lỗi, đọc thông báo và:
1. Kiểm tra connection string
2. Kiểm tra SQL Server đã chạy
3. Kiểm tra tất cả packages đã cài đặt
4. Xóa thư mục `bin` và `obj`, sau đó build lại

---

## 8. Chạy ứng dụng lần đầu

```bash
# Từ thư mục gốc
dotnet run --project EmployeeManagement.UI
```

Nếu thành công, cửa sổ ứng dụng sẽ hiển thị!

---

## 🐛 Xử lý lỗi

### Lỗi: "dotnet command not found"
- Cài đặt lại .NET SDK
- Khởi động lại Command Prompt/PowerShell
- Kiểm tra biến môi trường PATH

### Lỗi: "Cannot connect to SQL Server"
- Kiểm tra SQL Server Service đã chạy:
  - Mở **Services** (services.msc)
  - Tìm **SQL Server (SQLEXPRESS)** hoặc **SQL Server (MSSQLSERVER)**
  - Đảm bảo status là **Running**
- Kiểm tra connection string
- Thử ping SQL Server: `sqlcmd -S localhost -E`

### Lỗi: "Login failed for user"
- Kiểm tra username/password trong connection string
- Đảm bảo user có quyền truy cập database
- Thử dùng Windows Authentication

### Lỗi: "Package restore failed"
- Kiểm tra kết nối internet
- Xóa thư mục `%USERPROFILE%\.nuget\packages`
- Chạy lại: `dotnet restore --force`

### Lỗi: "The type initializer threw an exception"
- Cài đặt Visual C++ Redistributable:
  - https://aka.ms/vs/17/release/vc_redist.x64.exe
- Khởi động lại máy tính

---

## ✅ Checklist hoàn thành

- [ ] .NET 8.0 SDK đã cài đặt
- [ ] SQL Server đã cài đặt và chạy
- [ ] SSMS đã cài đặt
- [ ] Database đã tạo
- [ ] Connection string đã cấu hình
- [ ] Packages đã restore
- [ ] Project build thành công
- [ ] Ứng dụng chạy được

---

## 📞 Hỗ trợ

Nếu gặp vấn đề, vui lòng:
1. Kiểm tra lại từng bước
2. Đọc thông báo lỗi cẩn thận
3. Tìm kiếm lỗi trên Google/Stack Overflow
4. Tạo Issue trên GitHub với thông tin chi tiết
