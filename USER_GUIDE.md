# HƯỚNG DẪN SỬ DỤNG

## 📋 Mục lục
1. [Đăng nhập](#1-đăng-nhập)
2. [Dashboard](#2-dashboard)
3. [Chấm công](#3-chấm-công)
4. [Thông báo](#4-thông-báo)
5. [Quản lý Nhân viên](#5-quản-lý-nhân-viên)
6. [Tips & Tricks](#6-tips--tricks)

---

## 1. Đăng nhập

### Tài khoản test:

**Admin:**
- Username: `admin`
- Password: `admin123`

**Nhân viên:**
- Username: `employee1` | Password: `emp123`
- Username: `employee2` | Password: `emp123`

### Các bước đăng nhập:

1. Khởi động ứng dụng
2. Nhập username và password
3. Click **Đăng nhập**
4. Hệ thống sẽ chuyển đến Dashboard

---

## 2. Dashboard

### Màn hình chính hiển thị:

- **Header:** "HỆ THỐNG QUẢN LÝ NHÂN VIÊN"
- **7 Cards chức năng:**
  1. 👥 Quản lý Nhân viên
  2. 🏢 Quản lý Phòng ban
  3. 💼 Quản lý Chức vụ
  4. ⏰ Chấm công
  5. 📅 Nghỉ phép
  6. 📊 Báo cáo
  7. 🔔 Thông báo
- **Status bar:** Hiển thị thời gian thực

### Cách sử dụng:

- **Click vào card** để mở chức năng tương ứng
- **Hoặc sử dụng menu bar** phía trên
- **Thời gian** tự động cập nhật mỗi giây

---

## 3. Chấm công

### Mở cửa sổ Chấm công:

- Click card **"⏰ Chấm công"**
- Hoặc menu **"Chấm công" → "Chấm công hàng ngày"**

### 3.1. Chấm công vào

**Các bước:**
1. Click button **"Chấm công vào"** (màu xanh lá)
2. Xác nhận thời gian hiện tại
3. Click **"Yes"** để xác nhận
4. Thông báo "Chấm công vào thành công!"
5. Bản ghi mới xuất hiện ở đầu lịch sử

**Lưu ý:**
- Chỉ chấm công vào được 1 lần/ngày
- Sau khi chấm công vào, button sẽ bị vô hiệu hóa
- Button "Chấm công ra" sẽ được kích hoạt

### 3.2. Chấm công ra

**Các bước:**
1. Click button **"Chấm công ra"** (màu đỏ)
2. Xác nhận thời gian hiện tại
3. Click **"Yes"** để xác nhận
4. Hệ thống tự động:
   - Tính tổng giờ làm việc
   - Phát hiện đi muộn (sau 8:30)
   - Phát hiện về sớm (trước 17:30)
   - Cập nhật trạng thái và ghi chú

**Trạng thái:**
- **Đầy đủ:** Vào đúng giờ (trước 8:30) và ra đúng giờ (sau 17:30)
- **Đi muộn:** Vào sau 8:30 (hiển thị số phút đi muộn)
- **Về sớm:** Ra trước 17:30 (hiển thị số phút về sớm)
- **Đang làm việc:** Đã chấm công vào nhưng chưa chấm công ra

**Lưu ý:**
- Phải chấm công vào trước khi chấm công ra
- Nếu chưa chấm công vào → Hiển thị cảnh báo

### 3.3. Lịch sử chấm công

**Bảng hiển thị:**
- **Ngày:** Ngày chấm công
- **Giờ vào:** Thời gian chấm công vào (HH:mm:ss)
- **Giờ ra:** Thời gian chấm công ra (HH:mm:ss)
- **Tổng giờ làm:** Tổng thời gian làm việc (Xh Ym)
- **Trạng thái:** Đầy đủ/Đi muộn/Về sớm
- **Ghi chú:** Chi tiết về đi muộn/về sớm

**Thống kê:**
- **Tổng ngày công:** Số ngày đã chấm công
- **Tổng giờ làm:** Tổng thời gian làm việc

### 3.4. Lọc dữ liệu

**Các bước:**
1. Chọn **"Từ ngày"** và **"Đến ngày"**
2. Click button **"Lọc"**
3. Dữ liệu sẽ được lọc theo khoảng thời gian

### 3.5. Làm mới

- Click button **"Làm mới"** (màu xanh lá) để cập nhật dữ liệu mới nhất

### 3.6. Xuất báo cáo

- Click button **"Xuất báo cáo"** để export dữ liệu ra Excel (Đang phát triển)

---

## 4. Thông báo

### Mở cửa sổ Thông báo:

- Click card **"🔔 Thông báo"**
- Hoặc menu **"Thông báo" → "Gửi thông báo"**

### 4.1. Tab: Gửi thông báo mới

#### Thông tin người gửi:
- Hiển thị tên và phòng ban của người gửi

#### Thông tin thông báo:

**Tiêu đề:**
- Nhập tiêu đề thông báo (bắt buộc)

**Mức độ:**
- Thông thường
- Quan trọng
- Khẩn cấp

**Người nhận:**
- **Tất cả nhân viên:** Gửi cho toàn bộ nhân viên
- **Theo phòng ban:** Chọn phòng ban cụ thể
- **Chọn cụ thể:** Chọn từng nhân viên (có thể chọn nhiều)

**Nội dung:**
- Nhập nội dung thông báo (bắt buộc)
- Hỗ trợ nhiều dòng

**File đính kèm:**
- Click **"Chọn file"** để đính kèm file
- Có thể đính kèm nhiều file
- Click **"Xóa file"** để xóa file đã chọn

#### Các nút chức năng:

**Gửi thông báo:**
1. Điền đầy đủ thông tin
2. Click **"Gửi thông báo"** (màu xanh lá)
3. Xác nhận gửi
4. Thông báo sẽ được gửi đến người nhận

**Lưu nháp:**
- Click **"Lưu nháp"** (màu vàng) để lưu thông báo chưa gửi
- Có thể chỉnh sửa và gửi sau

**Xóa form:**
- Click **"Xóa form"** (màu xám) để xóa toàn bộ nội dung đã nhập

### 4.2. Tab: Danh sách thông báo

**Bảng hiển thị:**
- **ID:** Mã thông báo
- **Tiêu đề:** Tiêu đề thông báo
- **Người gửi:** Tên người gửi
- **Người nhận:** Đối tượng nhận
- **Mức độ:** Thông thường/Quan trọng/Khẩn cấp
- **Ngày gửi:** Thời gian gửi (dd/MM/yyyy HH:mm)
- **Trạng thái:** Đã gửi/Nháp
- **Đã đọc:** Số người đã đọc/Tổng số người nhận

**Các chức năng:**

**Xem chi tiết:**
1. Click chọn thông báo
2. Click **"Xem chi tiết"** (màu xanh dương)
3. Hoặc **double-click** vào thông báo
4. Hiển thị đầy đủ thông tin

**Xóa thông báo:**
1. Click chọn thông báo
2. Click **"Xóa thông báo"** (màu đỏ)
3. Xác nhận xóa

**Làm mới:**
- Click **"Làm mới"** (màu xanh lá) để cập nhật danh sách

---

## 5. Quản lý Nhân viên

### Đang phát triển

Các chức năng sẽ có:
- Thêm nhân viên mới
- Sửa thông tin nhân viên
- Xóa nhân viên
- Tìm kiếm và lọc
- Quản lý phòng ban
- Quản lý chức vụ

---

## 6. Tips & Tricks

### Phím tắt:

- **F5:** Refresh/Làm mới
- **Ctrl + Q:** Thoát ứng dụng
- **Alt + F4:** Đóng cửa sổ hiện tại

### Lưu ý quan trọng:

1. **Chấm công:**
   - Phải chấm công vào trước khi chấm công ra
   - Chỉ chấm công được 1 lần/ngày
   - Giờ chuẩn: Vào 8:30, Ra 17:30

2. **Thông báo:**
   - Kiểm tra kỹ người nhận trước khi gửi
   - Thông báo đã gửi không thể chỉnh sửa
   - Sử dụng "Lưu nháp" nếu chưa chắc chắn

3. **Hiệu suất:**
   - Đóng các cửa sổ không sử dụng
   - Sử dụng chức năng lọc để giảm dữ liệu hiển thị
   - Làm mới định kỳ để cập nhật dữ liệu mới

### Xử lý lỗi:

**Nếu ứng dụng không phản hồi:**
1. Đợi vài giây
2. Kiểm tra kết nối database
3. Khởi động lại ứng dụng

**Nếu dữ liệu không cập nhật:**
1. Click button "Làm mới"
2. Đóng và mở lại cửa sổ
3. Kiểm tra kết nối mạng

**Nếu không thể chấm công:**
1. Kiểm tra đã chấm công vào chưa
2. Kiểm tra đã chấm công hôm nay chưa
3. Liên hệ admin nếu vẫn lỗi

---

## 📞 Hỗ trợ

Nếu cần hỗ trợ:
1. Đọc lại hướng dẫn
2. Kiểm tra phần "Xử lý lỗi"
3. Liên hệ IT Support
4. Tạo ticket trên hệ thống

---

## 🔄 Cập nhật

Tài liệu này được cập nhật lần cuối: **13/11/2025**

Phiên bản ứng dụng: **1.0.0**
