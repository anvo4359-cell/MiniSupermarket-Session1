# HAPPY MART — Buổi 2: Bảo mật và phân quyền JWT cho Web API

## 1. Giới thiệu

Buổi 2 tập trung xây dựng cơ chế xác thực và phân quyền cho hệ thống quản lý siêu thị mini HAPPY MART. Dự án sử dụng ASP.NET Core Web API ở phía Backend và C# Windows Forms ở phía Client.

Mục tiêu là bảo vệ các API bằng JWT (JSON Web Token — mã thông báo xác thực), quản lý phiên đăng nhập và giới hạn quyền truy cập theo vai trò người dùng.

## 2. Mục tiêu thực hành

- Tìm hiểu cơ chế xác thực không trạng thái (Stateless Authentication).
- Phân biệt Authentication (xác thực danh tính) và Authorization (phân quyền truy cập).
- Xây dựng AuthController xử lý đăng nhập và cấp JWT.
- Cấu hình xác thực JWT Bearer trong Program.cs.
- Sử dụng các thuộc tính [Authorize] và [Authorize(Roles = "...")] để bảo vệ API.
- Xây dựng FormLogin trên Windows Forms.
- Quản lý JWT Token và vai trò người dùng bằng SessionManager.
- Gửi Bearer Token trong Header khi WinForms gọi API.

## 3. Công nghệ sử dụng

- C#: ngôn ngữ lập trình chính.
- Windows Forms (WinForms): xây dựng giao diện ứng dụng máy tính.
- ASP.NET Core Web API: xây dựng các điểm cuối API.
- JWT Bearer Authentication: xác thực yêu cầu HTTP.
- HttpClient: gửi yêu cầu từ WinForms đến Web API.
- Swagger UI: kiểm thử API.
- NuGet: quản lý các gói thư viện.

## 4. Nội dung thực hiện

### 4.1. Xây dựng chức năng đăng nhập

Tạo AuthController với endpoint POST `/api/auth/login` để kiểm tra thông tin đăng nhập và cấp JWT Token cho người dùng hợp lệ.

Tài khoản mẫu theo đề bài:

| Tài khoản | Mật khẩu | Vai trò |
|---|---|---|
| admin | 123456 | Admin |
| cashier | 123456 | Cashier |

*Lưu ý: Đây là tài khoản minh họa theo đề bài, không nên sử dụng làm thông tin đăng nhập thực tế.*

### 4.2. Cấu hình bảo mật API

- Đăng ký dịch vụ xác thực JWT Bearer.
- Cấu hình middleware xác thực và phân quyền.
- Bảo vệ CategoriesController bằng [Authorize].
- Giới hạn endpoint quản trị cho Admin.
- Cho phép Admin và Cashier truy cập các endpoint dành cho nhân viên.

### 4.3. Tích hợp WinForms Client

- Thiết kế FormLogin để nhập tài khoản và mật khẩu.
- Tạo SessionManager lưu JWT Token và vai trò người dùng trong bộ nhớ ứng dụng.
- Cập nhật ApiClientService hoặc HttpClient để đính kèm Header `Authorization: Bearer <token>`.
- Điều hướng đến màn hình quản lý sau khi đăng nhập thành công.

### 4.4. Kiểm thử bảo mật

- Gọi API không có token để kiểm tra phản hồi 401 Unauthorized.
- Đăng nhập và sử dụng token để truy cập API.
- Kiểm tra quyền Admin và Cashier.
- Kiểm tra phản hồi 403 Forbidden khi người dùng đã xác thực nhưng không đủ quyền.

## 5. Kết quả mong đợi

- Xây dựng được quy trình đăng nhập và cấp JWT Token.
- Hiểu cách bảo vệ API bằng xác thực và phân quyền.
- Biết cách lưu token ở phía WinForms và gửi token khi gọi API.
- Kiểm thử được các trường hợp truy cập hợp lệ và không hợp lệ bằng Swagger UI.

## 6. Cách chạy dự án

1. Mở solution MiniSupermarket.API và MiniSupermarket.WinForms bằng Visual Studio.
2. Cấu hình khóa bí mật JWT trong appsettings.json hoặc cấu hình tương ứng của dự án.
3. Chạy Web API và kiểm tra địa chỉ, cổng kết nối.
4. Mở Swagger UI để thử endpoint đăng nhập.
5. Chạy WinForms Client và kiểm tra giao diện đăng nhập.
6. Đăng nhập bằng tài khoản mẫu và kiểm thử quyền truy cập API.

## 7. Lưu ý

- Cổng API phải được cập nhật theo cấu hình thực tế trên máy.
- Không đưa khóa bí mật JWT hoặc mật khẩu thật lên GitHub.
- Cần kiểm tra cấu hình hết hạn token và các điều kiện xác thực phù hợp trước khi triển khai thực tế.

## 8. Tổng kết

Buổi 2 cung cấp nền tảng bảo mật cho hệ thống HAPPY MART thông qua JWT, xác thực người dùng và phân quyền theo vai trò. Đây là bước chuẩn bị quan trọng trước khi mở rộng các chức năng quản lý dữ liệu và nghiệp vụ bán hàng.

