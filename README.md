# Happy Mart POS

Hệ thống POS thu ngân tích hợp quản lý kho cho cửa hàng tiện lợi Happy Mart, gồm ASP.NET Core Web API và ứng dụng Windows Forms. Database kỹ thuật hiện tại là `MiniSupermarketDb` trên SQL Server LocalDB.

## Công nghệ

- .NET 10
- ASP.NET Core Web API, JWT Bearer, Swagger
- Entity Framework Core 10 và SQL Server provider
- ADO.NET (`SqlConnection`, `SqlCommand`, `SqlDataReader`, `SqlTransaction`)
- Windows Forms
- SQL Server LocalDB

## Cấu trúc

- `MiniSupermarket.API`: API, EF Core, migrations, authentication, checkout và reports.
- `MiniSupermarket.WinForms`: client đăng nhập, shell, quản lý dữ liệu, POS và báo cáo.
- `ThucHanhAdoNet_Tiet*`: các bài thực hành ADO.NET độc lập đang có trong solution; không phải project ứng dụng Happy Mart.

## Cấu hình an toàn

Không commit JWT secret hoặc mật khẩu thật. Tạo secret cho môi trường phát triển bằng User Secrets:

```powershell
dotnet user-secrets init --project MiniSupermarket.API/MiniSupermarket.API.csproj
dotnet user-secrets set "JwtSettings:Secret" "thay-bang-secret-it-nhat-32-ky-tu" --project MiniSupermarket.API/MiniSupermarket.API.csproj
```

`appsettings.Development.json` dùng LocalDB:

```text
Server=(localdb)\\MSSQLLocalDB;Database=MiniSupermarketDb;Trusted_Connection=True;TrustServerCertificate=True;
```

## Chạy API

```powershell
dotnet restore
dotnet build MiniSupermarket.API/MiniSupermarket.API.csproj
dotnet run --project MiniSupermarket.API/MiniSupermarket.API.csproj --launch-profile https
```

Mở Swagger theo URL được in trong terminal, thường là `http://localhost:5037/swagger` hoặc URL HTTPS trong `launchSettings.json`. Dùng `POST /api/auth/login`, sao chép `token`, bấm **Authorize** và nhập `Bearer <token>`.

`dotnet-ef` cần được cài nếu muốn tạo/kiểm tra migration bằng CLI:

```powershell
dotnet tool install --global dotnet-ef
dotnet ef migrations list --project MiniSupermarket.API/MiniSupermarket.API.csproj --startup-project MiniSupermarket.API/MiniSupermarket.API.csproj
dotnet ef database update --project MiniSupermarket.API/MiniSupermarket.API.csproj --startup-project MiniSupermarket.API/MiniSupermarket.API.csproj
```

Không drop database để xử lý lỗi migration. Sao lưu database trước khi thay đổi schema.

## Chạy WinForms

1. Khởi động API trước.
2. Kiểm tra base URL trong các client hiện có khớp với API (`http://localhost:5037/`).
3. Build và chạy `MiniSupermarket.WinForms` từ Visual Studio hoặc:

```powershell
dotnet build MiniSupermarket.WinForms/MiniSupermarket.WinForms.csproj
dotnet run --project MiniSupermarket.WinForms/MiniSupermarket.WinForms.csproj
```

## Tài khoản demo

Tài khoản seed trong mã nguồn hiện dùng tên `admin01`, `cashier01`, `ware01` với mật khẩu demo ban đầu `123456`. Khi đăng nhập thành công lần đầu, API nâng cấp mật khẩu cũ sang PBKDF2. Chỉ dùng các tài khoản này trong database phát triển, không dùng cho môi trường thật.

## Endpoint chính

- `POST /api/auth/login`: công khai, cấp JWT.
- `GET/POST/PUT/DELETE /api/categories`: danh mục; ghi dữ liệu yêu cầu Admin/Warehouse.
- `GET/POST/PUT/DELETE /api/products`: sản phẩm; ghi dữ liệu yêu cầu Admin/Warehouse.
- `GET/POST/PUT/DELETE /api/customers`: khách hàng; tạo/sửa Admin/Cashier, xóa Admin.
- `GET /api/ado-products/barcode/{barcode}`: tra cứu barcode bằng ADO.NET, yêu cầu token.
- `POST /api/checkout`: thanh toán, Admin/Cashier, transaction SQL và rollback.
- `GET /api/reports/dashboard-summary`: KPI dashboard, Admin/Warehouse.
- `GET /api/reports/top-products`: top sản phẩm bán chạy, Admin/Warehouse.
- `GET /api/reports/daily`: báo cáo ngày, Admin/Warehouse.

## Kiểm thử đã xác minh

- Solution build baseline thành công.
- API build sau thay đổi thành công.
- WinForms build sau thay đổi thành công.
- Development API kết nối được LocalDB.
- Swagger Development trả HTTP 200.
- Endpoint bảo vệ trả HTTP 401 khi thiếu token.
- Barcode không tồn tại trả HTTP 404.
- Chưa xác minh thanh toán thành công trên database vì môi trường khảo sát chưa có tài khoản đăng nhập hợp lệ được thực thi thành công.

Xem bảng đối chiếu và kịch bản demo tại [`docs/PROJECT_EVIDENCE.md`](docs/PROJECT_EVIDENCE.md).
