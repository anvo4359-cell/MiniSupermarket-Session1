# Môn học: Lập trình Ứng dụng .NET Core
## Buổi 1: Xây dựng Web API quản lý danh mục và kết nối WinForms Client (CRUD)

### 1. Mô tả Kiến trúc Hệ thống (Client - Server)
* **Backend (`MiniSupermarket.API`)**: Sử dụng ASP.NET Core Web API để xử lý nghiệp vụ và cung cấp RESTful API quản lý danh mục sản phẩm.
* **Frontend (`MiniSupermarket.WinForms`)**: Ứng dụng Windows Forms sử dụng `HttpClient` để gọi dữ liệu từ API và hiển thị lên giao diện `DataGridView`.

### 2. Công nghệ Sử dụng
* Ngôn ngữ: C# (.NET 8.0)
* Backend: ASP.NET Core Web API, Controllers, LINQ, In-Memory Data.
* Frontend: Windows Forms, `System.Net.Http.Json`.
* Công cụ kiểm thử: Swagger UI.

### 3. Hướng dẫn Chạy và Kiểm thử Dự án
* **Bước 1 (Chạy Backend):** Đặt `MiniSupermarket.API` làm Startup Project, nhấn `F5` chạy API và kiểm thử trên Swagger UI.
* **Bước 2 (Chạy Frontend):** Cấu hình cổng port khớp với API trong dự án WinForms và chạy ứng dụng để thao tác dữ liệu.

### 4. Thông tin Tác giả
* **Họ tên sinh viên:** Võ Thị Thùy An
* **Mã sinh viên:** 2124110097
* **Lớp học phần:** CCQ2411C
