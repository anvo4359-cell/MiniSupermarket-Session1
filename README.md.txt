# Môn học: Lập trình Ứng dụng .NET Core
## Buổi 1: Xây dựng Web API quản lý danh mục và kết nối WinForms Client (CRUD)

### 1. Mô tả Kiến trúc Hệ thống (Client - Server)
* **Backend (`MiniSupermarket.API`)**: Sử dụng ASP.NET Core Web API để xử lý nghiệp vụ, cung cấp các RESTful API chuẩn cho việc quản lý danh mục sản phẩm (CRUD).
* **Frontend (`MiniSupermarket.WinForms`)**: Ứng dụng Windows Forms đóng vai trò là máy trạm POS, sử dụng `HttpClient` để gọi dữ liệu từ API và hiển thị lên `DataGridView`.

### 2. Công nghệ Sử dụng
* Ngôn ngữ: C# (.NET 10.0) -> Vs 2026
* Backend: ASP.NET Core Web API, Controllers, LINQ, In-Memory Data.
* Frontend: Windows Forms, `System.Net.Http.Json`.
* Công cụ kiểm thử: Swagger UI.

### 3. Cấu trúc Solution
MiniSupermarketSystem/
│
├── MiniSupermarket.API/         # Dự án Backend Web API
└── MiniSupermarket.WinForms/    # Dự án Frontend WinForms
### 4. Hướng dẫn Chạy và Kiểm thử Dự án
* **Bước 1 (Chạy Backend):** Đặt `MiniSupermarket.API` làm Startup Project, nhấn `F5` để chạy và kiểm thử qua giao diện Swagger UI.
* **Bước 2 (Chạy Frontend):** Cấu hình cổng port của API trong dự án WinForms, sau đó chạy ứng dụng để thao tác dữ liệu.

### 5. Thông tin Tác giả
* **Họ tên sinh viên:** Võ Thị Thúy An
* **Mã sinh viên:** 2124110097
* **Lớp học phần:** CCQ2411C