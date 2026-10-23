# HAPPY MART — Buổi 3: Tích hợp SQL Server và Entity Framework Core

## 1. Giới thiệu

Buổi 3 tập trung chuyển đổi hệ thống quản lý siêu thị mini từ lưu trữ dữ liệu tạm thời trong bộ nhớ sang cơ sở dữ liệu SQL Server. Dự án sử dụng Entity Framework Core (EF Core — công cụ ánh xạ đối tượng C# với cơ sở dữ liệu quan hệ) để quản lý cấu trúc bảng và thao tác dữ liệu.

Thông qua mô hình Code-First, các lớp thực thể được xây dựng bằng C# trước, sau đó sử dụng Migration để tạo và cập nhật cơ sở dữ liệu.

## 2. Mục tiêu thực hành

- Tích hợp SQL Server vào ASP.NET Core Web API.
- Xây dựng các lớp thực thể và DbContext.
- Cấu hình quan hệ giữa danh mục và sản phẩm.
- Sử dụng EF Core Code-First và Migrations.
- Truy vấn dữ liệu bằng LINQ và các phương thức bất đồng bộ.
- Thực hiện các thao tác CRUD (Create, Read, Update, Delete — thêm, đọc, cập nhật và xóa).
- Kết nối WinForms Client với dữ liệu được lưu trữ trong SQL Server.

## 3. Công nghệ sử dụng

- C#: ngôn ngữ lập trình.
- ASP.NET Core Web API: xây dựng Backend.
- Microsoft SQL Server: hệ quản trị cơ sở dữ liệu.
- Entity Framework Core: ánh xạ và truy vấn dữ liệu.
- EF Core Migrations: quản lý thay đổi cấu trúc cơ sở dữ liệu.
- LINQ: truy vấn dữ liệu.
- async/await: xử lý các thao tác bất đồng bộ.
- Windows Forms: xây dựng giao diện Client.
- Swagger UI: kiểm thử API.
- SQL Server Management Studio (SSMS): quản lý và kiểm tra cơ sở dữ liệu.

## 4. Nội dung thực hiện

### 4.1. Xây dựng các thực thể

Theo nội dung chính của đề bài, xây dựng các lớp:

- Category: lưu thông tin danh mục sản phẩm.
- Product: lưu mã vạch, tên sản phẩm, giá bán, số lượng tồn kho và mã danh mục.
- Customer: thực thể dành cho bài tập mở rộng quản lý khách hàng.

### 4.2. Xây dựng SupermarketDbContext

Tạo SupermarketDbContext để quản lý các tập dữ liệu thông qua DbSet và cấu hình quan hệ giữa các thực thể.

Sử dụng Data Annotations để khai báo khóa chính, trường bắt buộc, độ dài dữ liệu và kiểu dữ liệu của các thuộc tính.

Đề bài cũng hướng dẫn sử dụng Data Seeding để thêm dữ liệu mẫu ban đầu cho danh mục và khách hàng.

### 4.3. Cấu hình kết nối SQL Server

- Khai báo Connection String trong appsettings.json.
- Đăng ký SupermarketDbContext bằng Dependency Injection (DI — cơ chế cung cấp các đối tượng phụ thuộc).
- Cấu hình EF Core sử dụng SQL Server.
- Kiểm tra kết nối giữa Web API và cơ sở dữ liệu.

### 4.4. Sử dụng EF Core Migrations

Các lệnh thực hành được đề bài hướng dẫn:

```powershell
Add-Migration InitialCreateDatabase
Update-Database
```

Đối với bài tập mở rộng quản lý khách hàng:

```powershell
Add-Migration AddCustomersTable
Update-Database
```

Các lệnh cần được thực hiện theo đúng trạng thái Migration hiện có của dự án, tránh tạo Migration trùng hoặc áp dụng sai cấu trúc cơ sở dữ liệu.

### 4.5. Xây dựng API quản lý danh mục

Tái cấu trúc CategoriesController để sử dụng SupermarketDbContext thay cho danh sách dữ liệu tạm thời.

Các endpoint theo đề bài:

| Phương thức | Endpoint | Chức năng |
|---|---|---|
| GET | `/api/categories` | Lấy danh sách danh mục |
| GET | `/api/categories/{id}` | Lấy danh mục theo ID |
| GET | `/api/categories/search?keyword=...` | Tìm kiếm danh mục |
| POST | `/api/categories` | Thêm danh mục |
| PUT | `/api/categories/{id}` | Cập nhật danh mục |
| DELETE | `/api/categories/{id}` | Xóa danh mục |

Sử dụng các phương thức như ToListAsync, FindAsync và SaveChangesAsync để thao tác dữ liệu bất đồng bộ.

### 4.6. Bài tập mở rộng: Quản lý khách hàng

Đề bài bổ sung yêu cầu xây dựng phân hệ khách hàng với các thông tin:

- Mã khách hàng.
- Tên khách hàng.
- Số điện thoại.
- Địa chỉ.
- Điểm tích lũy.
- Hạng thành viên.

Các endpoint dự kiến gồm lấy danh sách, lấy chi tiết, tìm kiếm, thêm, cập nhật và xóa khách hàng thông qua CustomersController.

Phía WinForms, đề bài hướng dẫn xây dựng FormCustomerManagement để hiển thị danh sách và thực hiện thao tác với API.

## 5. Kiểm thử và đánh giá

- Chạy Web API và kiểm thử endpoint bằng Swagger UI.
- Kiểm tra dữ liệu trong SQL Server bằng SSMS.
- Thử thêm, sửa và xóa danh mục.
- Kiểm tra dữ liệu sau khi khởi động lại ứng dụng.
- Kiểm tra khả năng trao đổi dữ liệu giữa WinForms và Web API.
- Với bài tập mở rộng, kiểm thử thêm các chức năng quản lý khách hàng nếu đã triển khai.

## 6. Cách chạy dự án

1. Cài đặt SQL Server và bảo đảm dịch vụ cơ sở dữ liệu đang hoạt động.
2. Cấu hình Connection String trong appsettings.json theo môi trường thực tế.
3. Kiểm tra các gói NuGet của EF Core phù hợp với phiên bản .NET đang sử dụng.
4. Chọn MiniSupermarket.API làm project khởi động.
5. Thực hiện Migration nếu cơ sở dữ liệu chưa được tạo hoặc có thay đổi cần áp dụng.
6. Chạy API và kiểm thử bằng Swagger UI.
7. Chạy MiniSupermarket.WinForms để kiểm tra chức năng kết nối API.

## 7. Lưu ý

- Không đưa mật khẩu SQL Server hoặc thông tin kết nối nhạy cảm lên GitHub.
- Tên cơ sở dữ liệu và Connection String cần thống nhất với cấu hình thực tế.
- Các bảng, Migration và chức năng trong README cần được đối chiếu với code trước khi xác nhận hoàn thành.

## 8. Tổng kết

Buổi 3 giúp xây dựng nền tảng lưu trữ dữ liệu lâu dài cho hệ thống HAPPY MART. Việc kết hợp SQL Server với EF Core, LINQ và các thao tác bất đồng bộ tạo tiền đề cho các phân hệ quản lý sản phẩm, khách hàng, đơn hàng và nghiệp vụ bán hàng ở những buổi tiếp theo.

