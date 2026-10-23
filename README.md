# HAPPY MART – BUỔI 5
## ADO.NET nâng cao, Stored Procedure và giao dịch thanh toán POS

**Tên đề tài:** Thiết kế và thi công hệ thống POS thu ngân tích hợp quản lý kho hàng – Cửa hàng tiện lợi HAPPY MART  
**Dự án:** MiniSupermarket  
**Nội dung:** ADO.NET nâng cao, truy vấn dữ liệu, báo cáo doanh thu, quản lý giao dịch và thanh toán hóa đơn  
**Cơ sở dữ liệu:** `MiniSupermarketDb`  
**Nền tảng:** C# / ASP.NET Core Web API / SQL Server / ADO.NET  
**Công cụ:** Visual Studio, SQL Server Management Studio (SSMS), Swagger UI

---

## 1. Giới thiệu Buổi 5

Buổi 5 tập trung vào việc sử dụng ADO.NET để thao tác trực tiếp với cơ sở dữ liệu SQL Server trong hệ thống bán hàng HAPPY MART. Nội dung được chia thành năm tiết, từ kiến thức kết nối và truy vấn dữ liệu đến xây dựng API báo cáo doanh thu và xử lý giao dịch thanh toán POS.

Các nội dung chính bao gồm:

- Sử dụng các đối tượng ADO.NET để kết nối, truy vấn và cập nhật dữ liệu.
- Xây dựng API tra cứu sản phẩm bằng mã vạch.
- Sử dụng `DataTable` và `SqlDataAdapter` để lưu dữ liệu vào bộ nhớ.
- Xây dựng Stored Procedure thống kê doanh thu theo nhân viên thu ngân.
- Tìm hiểu chuẩn ACID và cách sử dụng `SqlTransaction`.
- Xây dựng quy trình thanh toán gồm tạo hóa đơn, lưu chi tiết, trừ tồn kho và cộng điểm thưởng.
- Kiểm thử giao dịch thành công và giao dịch thất bại trên Swagger UI, đối soát dữ liệu trên SSMS.

Mục tiêu cuối cùng là hiểu cách bảo đảm tính toàn vẹn dữ liệu khi nhiều thao tác bán hàng cần được thực hiện liên tiếp.

## 2. Môi trường và công nghệ sử dụng

| Thành phần | Công nghệ |
|---|---|
| Ngôn ngữ lập trình | C# |
| Nền tảng API | ASP.NET Core Web API |
| Truy cập dữ liệu | ADO.NET |
| Thư viện kết nối SQL Server | `Microsoft.Data.SqlClient` |
| Hệ quản trị cơ sở dữ liệu | Microsoft SQL Server |
| Công cụ quản lý cơ sở dữ liệu | SQL Server Management Studio (SSMS) |
| Kiểm thử API | Swagger UI |
| Ứng dụng giao diện | Windows Forms (WinForms) |
| Cơ sở dữ liệu thực hành | `MiniSupermarketDb` |

### Chuỗi kết nối tham khảo

Ví dụ sử dụng SQL Server LocalDB hoặc SQL Server cài đặt trên máy:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=MiniSupermarketDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

Đây là cấu hình tham khảo. Tùy theo máy tính, máy chủ SQL Server có thể cần sử dụng tên kết nối khác, chẳng hạn `localhost` hoặc tên instance đã cài đặt.

Cần bảo đảm cơ sở dữ liệu `MiniSupermarketDb` đã tồn tại và tài khoản chạy ứng dụng có quyền truy cập.

**Lưu ý:** Không đưa mật khẩu, thông tin kết nối nhạy cảm hoặc khóa bí mật vào Git. Với môi trường triển khai thực tế, cần sử dụng cấu hình bảo mật phù hợp.

## 3. Nội dung thực hành

### 3.1. Tiết 1 – ADO.NET căn bản và các phương thức thực thi SQL

#### Mục tiêu

Hiểu cách C# kết nối SQL Server và sử dụng các đối tượng ADO.NET để đọc, thêm, sửa hoặc xóa dữ liệu.

#### Các đối tượng quan trọng

| Đối tượng | Chức năng |
|---|---|
| `SqlConnection` | Tạo kết nối đến SQL Server. |
| `SqlCommand` | Thực thi câu lệnh SQL hoặc Stored Procedure. |
| `SqlParameter` | Truyền tham số cho câu lệnh SQL một cách an toàn. |
| `SqlDataReader` | Đọc dữ liệu tuần tự từ kết quả truy vấn. |
| `DataTable` | Lưu dữ liệu dạng bảng trong bộ nhớ. |
| `SqlDataAdapter` | Đổ dữ liệu truy vấn vào `DataTable` hoặc `DataSet`. |

#### Ba phương thức thực thi thường dùng

**ExecuteScalar()**

Dùng khi cần lấy một giá trị duy nhất, ví dụ đếm tổng số sản phẩm.

```csharp
string sql = "SELECT COUNT(*) FROM Products";

using SqlCommand command = new SqlCommand(sql, connection);

int totalProducts = Convert.ToInt32(command.ExecuteScalar());
```

**ExecuteNonQuery()**

Dùng để thực thi các câu lệnh thêm, sửa hoặc xóa dữ liệu.

```csharp
string sql = @"
    UPDATE Products
    SET StockQuantity = @Stock
    WHERE ProductId = @Id";

using SqlCommand command = new SqlCommand(sql, connection);

command.Parameters.AddWithValue("@Stock", 50);
command.Parameters.AddWithValue("@Id", 1);

int affectedRows = command.ExecuteNonQuery();
```

**ExecuteReader()**

Dùng khi cần đọc nhiều dòng dữ liệu.

```csharp
string sql = @"
    SELECT ProductId, Barcode, ProductName, Price, StockQuantity
    FROM Products
    ORDER BY ProductId ASC";

using SqlCommand command = new SqlCommand(sql, connection);
using SqlDataReader reader = command.ExecuteReader();

while (reader.Read())
{
    Console.WriteLine(
        $"{reader["ProductId"]} - {reader["ProductName"]}");
}
```

Các ví dụ trên là mã nguồn minh họa. Khi thực hành, cần mở kết nối trước khi thực thi lệnh và bảo đảm biến `connection` được khởi tạo đúng.

#### Bảo mật truy vấn

Không nên ghép trực tiếp dữ liệu do người dùng nhập vào chuỗi SQL. Thay vào đó, hãy sử dụng tham số như `@Id`, `@Barcode` hoặc `@Stock`.

Cách này giúp ngăn dữ liệu đầu vào được diễn giải như một phần của câu lệnh SQL đối với các giá trị đã được tham số hóa.

### 3.2. Tiết 2 – API tra cứu mã vạch và bộ nhớ đệm DataTable

#### Mục tiêu

Xây dựng API truy vấn sản phẩm bằng ADO.NET, sử dụng `SqlDataReader` để đọc dữ liệu và `DataTable` để lưu kết quả truy vấn trong bộ nhớ.

#### A. API tra cứu sản phẩm bằng mã vạch

**Tệp dự kiến:** `MiniSupermarket.API/Controllers/AdoProductController.cs`

**Endpoint:**

```http
GET /api/ado-products/barcode/{barcode}
```

Chức năng:

1. Nhận mã vạch sản phẩm từ URL.
2. Kết nối đến SQL Server bằng `SqlConnection`.
3. Truy vấn bảng `Products` bằng `SqlCommand`.
4. Truyền mã vạch qua tham số `@Barcode`.
5. Đọc dữ liệu bằng `SqlDataReader`.
6. Trả về thông tin sản phẩm hoặc thông báo không tìm thấy.

Kết quả mong đợi:

- `200 OK`: Tìm thấy sản phẩm.
- `404 Not Found`: Không tìm thấy sản phẩm tương ứng.
- `500 Internal Server Error`: Có lỗi máy chủ hoặc cơ sở dữ liệu; thông tin chi tiết nên được ghi vào log thay vì trả dữ liệu nhạy cảm cho người dùng.

Ví dụ kiểm thử:

```http
GET /api/ado-products/barcode/893456789001
```

Mã vạch trên là dữ liệu mẫu của tài liệu thực hành. Kết quả thực tế phụ thuộc vào dữ liệu hiện có trong cơ sở dữ liệu.

#### B. API bộ nhớ đệm dữ liệu

**Endpoint:**

```http
GET /api/ado-products/disconnected-cache
```

API sử dụng `DataTable` kết hợp `SqlDataAdapter.Fill()` để tải dữ liệu vào bộ nhớ.

Các thông tin có thể trả về:

- `totalLoaded`: Số dòng dữ liệu đã tải.
- `cachedAt`: Thời điểm tải dữ liệu.
- `data`: Danh sách dữ liệu sản phẩm.

Ưu điểm của mô hình ngắt kết nối là có thể xử lý dữ liệu trong bộ nhớ sau khi quá trình tải hoàn tất, thay vì phải giữ kết nối cơ sở dữ liệu trong suốt thời gian sử dụng dữ liệu.

Tuy nhiên, dữ liệu trong bộ nhớ có thể không phản ánh những thay đổi mới nhất trên SQL Server. Cần xác định thời gian làm mới và giới hạn số dòng tải khi dữ liệu lớn.

#### C. Bài tập mở rộng

- Xây dựng endpoint `filter-stock` để lọc sản phẩm theo số lượng tồn kho.
- Sử dụng `DataTable` để thống kê số sản phẩm theo từng danh mục.
- Kiểm tra kết quả truy vấn với dữ liệu thực tế trong SSMS.

### 3.3. Tiết 3 – Stored Procedure và báo cáo doanh thu theo thu ngân

#### Mục tiêu

Sử dụng Stored Procedure để tổng hợp dữ liệu bán hàng trên SQL Server, sau đó gọi thủ tục từ C# và trả kết quả dưới dạng JSON.

Stored Procedure là thủ tục được lưu trong cơ sở dữ liệu, có thể nhận tham số, thực hiện câu lệnh SQL và trả về kết quả. Việc đưa logic truy vấn vào thủ tục giúp tổ chức mã SQL tập trung, dễ bảo trì và quản lý quyền truy cập.

#### A. Dữ liệu sử dụng

Bảng `Orders` được sử dụng để thống kê doanh thu theo nhân viên thu ngân.

| Cột | Ý nghĩa |
|---|---|
| `OrderId` | Mã định danh hóa đơn. |
| `OrderCode` | Mã hóa đơn hiển thị. |
| `CreatedDate` | Thời điểm tạo hóa đơn. |
| `CashierUsername` | Tài khoản thu ngân lập hóa đơn. |
| `CustomerId` | Mã khách hàng. |
| `TotalItems` | Tổng số lượng sản phẩm trong hóa đơn. |
| `FinalAmount` | Tổng tiền thanh toán. |
| `RewardPoints` | Điểm thưởng phát sinh từ hóa đơn. |

Tên cột và kiểu dữ liệu phải được đối chiếu với cấu trúc bảng thực tế trước khi chạy script.

#### B. Tạo Stored Procedure

Chạy đoạn SQL sau trong SSMS, sau khi chọn đúng cơ sở dữ liệu `MiniSupermarketDb`:

```sql
CREATE OR ALTER PROCEDURE dbo.sp_GetRevenueByCashier
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        CashierUsername,
        COUNT(OrderId) AS TotalOrders,
        SUM(FinalAmount) AS TotalRevenue,
        SUM(RewardPoints) AS TotalPointsAwarded
    FROM dbo.Orders
    GROUP BY CashierUsername
    ORDER BY TotalRevenue DESC;
END;
GO
```

Kiểm tra thủ tục:

```sql
EXEC dbo.sp_GetRevenueByCashier;
```

Kết quả là danh sách thu ngân cùng tổng số hóa đơn, tổng doanh thu và tổng điểm thưởng tương ứng.

Số liệu cụ thể phụ thuộc vào dữ liệu đang có trong cơ sở dữ liệu; không mặc định kết quả mẫu trong tài liệu là kết quả thực tế của máy đang chạy.

#### C. DTO báo cáo

**Tệp dự kiến:** `MiniSupermarket.API/Models/CashierRevenueDto.cs`

```csharp
namespace MiniSupermarket.API.Models
{
    public class CashierRevenueDto
    {
        public string CashierUsername { get; set; } = string.Empty;

        public int TotalOrders { get; set; }

        public decimal TotalRevenue { get; set; }

        public int TotalPointsAwarded { get; set; }
    }
}
```

DTO (Data Transfer Object – đối tượng truyền dữ liệu) dùng để chuyển kết quả báo cáo từ API sang phía gọi API.

#### D. API báo cáo doanh thu

**Tệp dự kiến:** `MiniSupermarket.API/Controllers/ReportsController.cs`

**Endpoint:**

```http
GET /api/reports/revenue-by-cashier
```

API sử dụng `SqlCommand` với:

```csharp
command.CommandType = CommandType.StoredProcedure;
```

Sau đó gọi `ExecuteReader()` để đọc dữ liệu và ánh xạ từng dòng thành `CashierRevenueDto`.

Kết quả thành công dự kiến có cấu trúc:

```json
{
  "success": true,
  "generatedAt": "2026-09-15 09:30:45",
  "totalCashiers": 5,
  "grandTotalRevenue": 4822000.00,
  "data": [
    {
      "cashierUsername": "cashier03",
      "totalOrders": 2,
      "totalRevenue": 1435000.00,
      "totalPointsAwarded": 143
    }
  ]
}
```

Đây là cấu trúc minh họa. Giá trị và số dòng trả về thực tế phụ thuộc dữ liệu trong SQL Server.

#### E. Các lỗi thường gặp

| Lỗi | Nguyên nhân có thể xảy ra | Cách kiểm tra |
|---|---|---|
| Không tìm thấy Stored Procedure | Chưa tạo thủ tục hoặc tạo sai database. | Kiểm tra database đang chọn và chạy lại script. |
| Sai cú pháp khi gọi thủ tục | Thiếu `CommandType.StoredProcedure`. | Kiểm tra cấu hình `SqlCommand`. |
| Sai kiểu dữ liệu | Chuyển `DECIMAL` sang kiểu không phù hợp. | Dùng `Convert.ToDecimal()` cho doanh thu. |
| Lỗi giá trị NULL | Kết quả tổng hợp có thể chứa `DBNull.Value`. | Kiểm tra `reader.IsDBNull()` trước khi chuyển kiểu. |

#### F. Bài tập mở rộng

**Báo cáo doanh thu theo khoảng ngày**

Tạo thủ tục `sp_GetRevenueByDateRange` nhận hai tham số `@FromDate` và `@ToDate`, sau đó lọc dữ liệu theo `CreatedDate`.

**Top 5 sản phẩm bán chạy**

Tạo thủ tục `sp_GetTop5SellingProducts`, kết hợp bảng `OrderDetails` và `Products` để thống kê số lượng bán.

#### G. Lưu ý về phân quyền

Stored Procedure có thể được sử dụng để giới hạn quyền truy cập dữ liệu. Trong môi trường thực tế, cần cấu hình quyền SQL Server phù hợp để tài khoản ứng dụng chỉ có quyền cần thiết, chẳng hạn quyền thực thi thủ tục báo cáo.

Chỉ sử dụng Stored Procedure không đồng nghĩa với việc hệ thống đã được bảo mật tuyệt đối. Quyền của tài khoản kết nối, quyền sở hữu đối tượng và cấu hình bảo mật vẫn cần được kiểm tra.

### 3.4. Tiết 4 – Chuẩn ACID và SqlTransaction

#### Mục tiêu

Hiểu cách gom nhiều thao tác cơ sở dữ liệu thành một giao dịch thống nhất, bảo đảm dữ liệu không bị cập nhật dở dang khi phát sinh lỗi.

#### A. Bốn đặc tính ACID

| Đặc tính | Ý nghĩa | Ví dụ trong HAPPY MART |
|---|---|---|
| Atomicity – Tính nguyên tử | Tất cả thao tác trong giao dịch thành công hoặc được hoàn tác. | Không để hóa đơn tồn tại nhưng thiếu chi tiết bán hàng. |
| Consistency – Tính nhất quán | Giao dịch bảo đảm các ràng buộc dữ liệu được duy trì. | Tồn kho không bị âm nếu nghiệp vụ và cơ sở dữ liệu được thiết kế đúng. |
| Isolation – Tính cô lập | Các giao dịch đồng thời được kiểm soát theo mức cô lập. | Hai quầy không cùng bán vượt quá số lượng hàng tồn. |
| Durability – Tính bền vững | Dữ liệu đã Commit được lưu bền vững theo cơ chế của hệ quản trị cơ sở dữ liệu. | Hóa đơn đã thanh toán vẫn được lưu sau khi ứng dụng đóng. |

#### B. Vòng đời của SqlTransaction

Một giao dịch ADO.NET thông thường gồm các bước:

1. Mở kết nối bằng `connection.Open()`.
2. Bắt đầu giao dịch bằng `connection.BeginTransaction()`.
3. Gắn giao dịch vào từng `SqlCommand`.
4. Thực hiện các thao tác cơ sở dữ liệu.
5. Gọi `transaction.Commit()` nếu tất cả thao tác thành công.
6. Gọi `transaction.Rollback()` khi cần hoàn tác do lỗi.

Ví dụ:

```csharp
using SqlConnection connection = new SqlConnection(connectionString);
connection.Open();

using SqlTransaction transaction = connection.BeginTransaction();

try
{
    string sql = @"
        UPDATE Products
        SET StockQuantity = StockQuantity - @Qty
        WHERE ProductId = @Id
          AND StockQuantity >= @Qty";

    using SqlCommand command =
        new SqlCommand(sql, connection, transaction);

    command.Parameters.AddWithValue("@Qty", 5);
    command.Parameters.AddWithValue("@Id", 1);

    int affectedRows = command.ExecuteNonQuery();

    if (affectedRows != 1)
    {
        throw new InvalidOperationException(
            "Không tìm thấy sản phẩm hoặc số lượng tồn kho không đủ.");
    }

    transaction.Commit();
}
catch
{
    try
    {
        transaction.Rollback();
    }
    catch
    {
        // Ghi log lỗi Rollback nếu cần.
    }

    throw;
}
```

Ví dụ này minh họa cách thực hiện một thao tác có kiểm tra tồn kho trong giao dịch. Khi sử dụng thực tế, cần bổ sung xử lý ngoại lệ phù hợp và tránh để lỗi Rollback che mất lỗi ban đầu.

**Lưu ý quan trọng:** Mọi câu lệnh thuộc giao dịch phải dùng đúng kết nối và giao dịch tương ứng. Có thể truyền `transaction` vào constructor của `SqlCommand` hoặc gán thuộc tính `command.Transaction`.

#### C. Bài tập mô phỏng Rollback

Thực hiện cập nhật tồn kho sản phẩm thứ nhất trong giao dịch, sau đó cố tình phát sinh ngoại lệ trước khi Commit.

Kết quả mong đợi:

- Lệnh cập nhật đầu tiên có thể đã thực thi trong giao dịch.
- Khi phát sinh lỗi và Rollback thành công, thay đổi trong giao dịch được hoàn tác.
- Tồn kho của sản phẩm giữ nguyên so với trước giao dịch.

Có thể kiểm tra bằng SSMS:

```sql
SELECT ProductId, StockQuantity
FROM dbo.Products
WHERE ProductId = 1;
```

Cần so sánh với số lượng trước khi chạy thử để xác nhận kết quả.

#### D. Các lỗi thường gặp

| Lỗi | Nguyên nhân | Hướng xử lý |
|---|---|---|
| Lệnh yêu cầu đối tượng Transaction | Quên gắn giao dịch vào `SqlCommand`. | Truyền `transaction` vào lệnh. |
| Giao dịch bị treo hoặc giữ khóa lâu | Xử lý ngoại lệ không đầy đủ hoặc giao dịch kéo dài. | Giới hạn thời gian giao dịch và luôn xử lý lỗi. |
| Transaction đã hoàn tất | Gọi Commit hoặc Rollback nhiều lần. | Kiểm soát luồng xử lý, tránh hoàn tất giao dịch lặp lại. |

Deadlock (khóa chết) không phải lúc nào cũng do quên Rollback. Nó thường xảy ra khi các giao dịch giữ khóa theo cách xung đột. Cần xử lý giao dịch ngắn gọn, thống nhất thứ tự truy cập dữ liệu và có chiến lược thử lại phù hợp.

### 3.5. Tiết 5 – Xây dựng API thanh toán POS sử dụng SqlTransaction

#### Mục tiêu

Xây dựng quy trình thanh toán hóa đơn, trong đó các thao tác ghi dữ liệu liên quan được thực hiện trong cùng một giao dịch:

1. Tạo hóa đơn trong bảng `Orders`.
2. Thêm các dòng chi tiết vào `OrderDetails`.
3. Kiểm tra và trừ tồn kho trong `Products`.
4. Cộng điểm thưởng vào `Customers` nếu khách hàng hợp lệ.
5. Commit nếu mọi thao tác thành công; Rollback khi xảy ra lỗi.

#### A. DTO nhận dữ liệu thanh toán

**Tệp dự kiến:** `MiniSupermarket.API/Models/CheckoutDtos.cs`

Hai lớp DTO trong tài liệu thực hành:

- `CartItemCheckoutDto`: chứa `ProductId`, `Quantity`, `UnitPrice`.
- `CheckoutRequestDto`: chứa `CashierUsername`, `CustomerId` và danh sách `Items`.

Dữ liệu JSON gửi lên API có thể được mô tả như sau:

```json
{
  "cashierUsername": "cashier01",
  "customerId": 1,
  "items": [
    {
      "productId": 1,
      "quantity": 2,
      "unitPrice": 12500
    },
    {
      "productId": 5,
      "quantity": 3,
      "unitPrice": 10000
    }
  ]
}
```

Đây là dữ liệu mẫu dùng để kiểm thử, không phải xác nhận về số lượng tồn kho hiện tại của cơ sở dữ liệu.

#### B. API thanh toán

**Tệp dự kiến:** `MiniSupermarket.API/Controllers/CheckoutController.cs`

**Endpoint:**

```http
POST /api/checkout
```

Các bước xử lý chính:

- Kiểm tra giỏ hàng có sản phẩm hay không.
- Kiểm tra thông tin thu ngân và dữ liệu đầu vào.
- Tính tổng số lượng, tổng tiền và điểm thưởng.
- Bắt đầu `SqlTransaction`.
- Thêm hóa đơn và lấy `OrderId` vừa tạo bằng `OUTPUT INSERTED.OrderId`.
- Kiểm tra từng sản phẩm, lưu chi tiết và cập nhật tồn kho.
- Cộng điểm thưởng nếu nghiệp vụ yêu cầu.
- Gọi `Commit()` nếu tất cả thao tác thành công.
- Gọi `Rollback()` nếu phát sinh lỗi.

Ví dụ câu lệnh lấy mã hóa đơn vừa tạo:

```sql
INSERT INTO Orders
(
    OrderCode,
    CreatedDate,
    CashierUsername,
    CustomerId,
    TotalItems,
    FinalAmount,
    RewardPoints
)
OUTPUT INSERTED.OrderId
VALUES
(
    @OrderCode,
    GETDATE(),
    @CashierUsername,
    @CustomerId,
    @TotalItems,
    @FinalAmount,
    @RewardPoints
);
```

Trong C#, có thể dùng `ExecuteScalar()` để nhận giá trị `OrderId` do câu lệnh trả về.

#### C. Kiểm tra dữ liệu đầu vào

Trước khi thực hiện giao dịch, cần kiểm tra:

- Giỏ hàng không rỗng.
- `ProductId` hợp lệ.
- `Quantity` lớn hơn 0.
- Giá tiền hợp lệ và không âm.
- Sản phẩm tồn tại và còn đủ hàng.
- Khách hàng tồn tại nếu nghiệp vụ yêu cầu khách hàng.
- Tài khoản thu ngân hợp lệ và có quyền thực hiện thanh toán.

Đặc biệt, **không nên tin tưởng `UnitPrice` hoặc `CashierUsername` do client gửi lên**. Khi triển khai thực tế, giá bán phải được lấy hoặc xác minh từ dữ liệu đáng tin cậy trên máy chủ, còn danh tính thu ngân cần được xác định từ thông tin xác thực đã đăng nhập.

Cần xử lý rõ trường hợp khách vãng lai. Nếu `CustomerId` có thể không có giá trị, kiểu dữ liệu, khóa ngoại và câu lệnh SQL phải được thiết kế tương ứng; không nên mặc định một mã khách hàng cụ thể là khách vãng lai nếu chưa xác minh dữ liệu.

#### D. Kiểm tra tồn kho an toàn

Chỉ đọc tồn kho rồi mới trừ có thể gây tranh chấp khi hai quầy cùng thanh toán một sản phẩm. Để bảo vệ tồn kho trong môi trường nhiều người dùng, nên thực hiện cập nhật có điều kiện trong cùng giao dịch:

```sql
UPDATE Products
SET StockQuantity = StockQuantity - @Qty
WHERE ProductId = @ProductId
  AND StockQuantity >= @Qty;
```

Nếu số dòng bị ảnh hưởng không bằng 1, cần xem đây là trường hợp không thể trừ hàng và hủy giao dịch.

Cách làm này giúp ngăn thao tác trừ kho khi số lượng hiện có không đủ, kể cả khi nhiều giao dịch diễn ra gần như đồng thời.

#### E. Kịch bản kiểm thử 1 – Thanh toán thành công

**Thao tác:**

1. Khởi động API.
2. Mở Swagger UI.
3. Chọn `POST /api/checkout`.
4. Nhấn **Try it out**.
5. Gửi JSON giỏ hàng có sản phẩm tồn tại và số lượng hợp lệ.
6. Nhấn **Execute**.
7. Kiểm tra phản hồi và đối soát cơ sở dữ liệu.

Kết quả mong đợi: API trả về thành công, có mã hóa đơn, tổng số lượng và tổng tiền. Các bảng `Orders`, `OrderDetails`, `Products` và `Customers` được cập nhật phù hợp với nghiệp vụ.

Mã HTTP cụ thể phụ thuộc vào cách triển khai API. Tài liệu bài tập sử dụng `200 OK`.

#### F. Kịch bản kiểm thử 2 – Mua vượt tồn kho

**Mục tiêu:** Xác nhận toàn bộ giao dịch được hoàn tác khi có một sản phẩm không đủ tồn kho.

Ví dụ dữ liệu thử nghiệm:

```json
{
  "cashierUsername": "cashier01",
  "customerId": 1,
  "items": [
    {
      "productId": 1,
      "quantity": 1,
      "unitPrice": 12500
    },
    {
      "productId": 2,
      "quantity": 999,
      "unitPrice": 135000
    }
  ]
}
```

Chỉ chạy kịch bản này sau khi xác nhận các mã sản phẩm tồn tại và số lượng tồn thực tế phù hợp với mục tiêu thử nghiệm.

Kết quả mong đợi:

- API báo giao dịch thất bại.
- Nếu lỗi được xử lý bằng `Rollback()` thành công, hóa đơn và chi tiết phát sinh trong giao dịch bị hoàn tác.
- Tồn kho của sản phẩm xử lý trước đó được khôi phục.
- Điểm thưởng không bị cộng dở dang.

Tài liệu thực hành sử dụng `400 Bad Request` cho trường hợp không đủ tồn kho. Mã lỗi thực tế phụ thuộc vào cách phân loại lỗi trong API.

#### G. Đối soát dữ liệu bằng SSMS

Kiểm tra các hóa đơn mới nhất:

```sql
SELECT TOP 10 *
FROM dbo.Orders
ORDER BY OrderId DESC;
```

Kiểm tra chi tiết hóa đơn:

```sql
SELECT *
FROM dbo.OrderDetails
WHERE OrderId = @OrderId;
```

Kiểm tra tồn kho:

```sql
SELECT ProductId, ProductName, StockQuantity
FROM dbo.Products
WHERE ProductId IN (1, 2, 5);
```

Kiểm tra điểm thưởng khách hàng:

```sql
SELECT CustomerId, RewardPoints
FROM dbo.Customers
WHERE CustomerId = 1;
```

Đối với kịch bản Rollback, hãy ghi lại số liệu trước khi thử nghiệm rồi so sánh với số liệu sau khi API trả về lỗi. Không chỉ dựa vào thông báo trên Swagger để kết luận dữ liệu đã được hoàn tác đúng.

#### H. Bài tập mở rộng – API hủy hóa đơn

**Endpoint dự kiến:**

```http
DELETE /api/checkout/cancel/{orderId}
```

Quy trình cần thực hiện trong một giao dịch:

1. Kiểm tra hóa đơn có tồn tại.
2. Lấy thông tin khách hàng và điểm thưởng của hóa đơn.
3. Đọc danh sách sản phẩm đã bán trong `OrderDetails`.
4. Hoàn trả số lượng tồn kho.
5. Khấu trừ điểm thưởng đã cộng, theo quy tắc nghiệp vụ.
6. Xóa chi tiết hóa đơn.
7. Xóa hóa đơn tổng.
8. Commit nếu thành công; Rollback nếu có lỗi.

Đây là bài tập mở rộng, không mặc định đã được triển khai trong dự án.

**Lưu ý nghiệp vụ:** Trong hệ thống bán hàng thực tế, hủy hóa đơn có thể cần lưu lịch sử thay vì xóa dữ liệu. Cũng cần kiểm tra các ràng buộc khóa ngoại, trạng thái thanh toán, quyền của người hủy và nguy cơ hoàn trả tồn kho hoặc trừ điểm thưởng nhiều lần.

---

## 4. Hướng dẫn chạy và kiểm thử

### Bước 1. Kiểm tra dự án

Mở solution bằng Visual Studio và xác định các project liên quan, đặc biệt là `MiniSupermarket.API`.

Kiểm tra:

- Project API biên dịch được.
- Chuỗi kết nối trỏ đến đúng SQL Server và database.
- Các bảng cần thiết đã tồn tại.
- Tài khoản kết nối có đủ quyền thực hiện truy vấn.
- Các Stored Procedure đã được tạo nếu API có sử dụng.

### Bước 2. Khởi chạy Web API

Chọn `MiniSupermarket.API` làm project khởi động và chạy bằng F5 hoặc Ctrl + F5.

Mở địa chỉ Swagger UI do ứng dụng cung cấp, thường có dạng:

```text
http://localhost:<port>/swagger/index.html
```

Hoặc sử dụng địa chỉ HTTPS nếu cấu hình chạy ứng dụng yêu cầu.

Không mặc định cổng trong tài liệu là cổng thực tế trên mọi máy.

### Bước 3. Kiểm thử các endpoint

Các endpoint của Buổi 5 được mô tả trong tài liệu:

| Phương thức | Endpoint | Mục đích |
|---|---|---|
| GET | `/api/ado-products/barcode/{barcode}` | Tra cứu sản phẩm bằng mã vạch. |
| GET | `/api/ado-products/disconnected-cache` | Kiểm tra dữ liệu được tải vào `DataTable`. |
| GET | `/api/reports/revenue-by-cashier` | Báo cáo doanh thu theo thu ngân. |
| POST | `/api/checkout` | Thanh toán hóa đơn POS. |
| DELETE | `/api/checkout/cancel/{orderId}` | Bài tập mở rộng hủy hóa đơn. |

Các đường dẫn trên là đường dẫn dự kiến theo tài liệu thực hành. Cần đối chiếu với attribute route và mã nguồn thực tế trước khi sử dụng.

### Bước 4. Kiểm tra dữ liệu trong SSMS

Sau mỗi lần thử nghiệm, kiểm tra bảng liên quan bằng câu lệnh `SELECT`.

Đối với thanh toán thành công, xác nhận hóa đơn, chi tiết, tồn kho và điểm thưởng được cập nhật đồng bộ.

Đối với thanh toán thất bại, xác nhận không có thay đổi dở dang do giao dịch thất bại để lại.

---

## 5. Cấu trúc tệp dự kiến

Các tệp dưới đây là cấu trúc tham khảo theo nội dung bài học. Không tự tạo lại những tệp đã tồn tại hoặc thay thế mã nguồn hiện có nếu chưa kiểm tra dự án.

```text
MiniSupermarket.API/
├── Controllers/
│   ├── AdoProductController.cs
│   ├── ReportsController.cs
│   └── CheckoutController.cs
├── Models/
│   ├── CashierRevenueDto.cs
│   └── CheckoutDtos.cs
├── appsettings.json
└── Program.cs

MiniSupermarket.WinForms/
└── Các form giao diện POS hiện có

README.md
```

- `AdoProductController.cs`: API tra cứu mã vạch và bộ nhớ đệm.
- `ReportsController.cs`: API báo cáo doanh thu.
- `CheckoutController.cs`: API thanh toán và bài tập mở rộng hủy hóa đơn.
- `CashierRevenueDto.cs`: DTO báo cáo doanh thu.
- `CheckoutDtos.cs`: DTO nhận dữ liệu giỏ hàng.
- `appsettings.json`: cấu hình kết nối cơ sở dữ liệu, cần bảo vệ thông tin nhạy cảm.

---

## 6. Checklist nghiệm thu Buổi 5

Các tiêu chí sau dùng để tự kiểm tra khi thực hành. Chỉ đánh dấu hoàn thành sau khi đã chạy thử và xác minh kết quả.

- [ ] **Tiết 1 – ADO.NET:** Hiểu và sử dụng đúng `SqlConnection`, `SqlCommand`, `SqlParameter`, `SqlDataReader`, `DataTable` và `SqlDataAdapter`.
- [ ] **Tiết 1 – Phương thức thực thi:** Phân biệt được `ExecuteScalar()`, `ExecuteNonQuery()` và `ExecuteReader()`.
- [ ] **Tiết 2 – Tra cứu mã vạch:** Kiểm thử API tra cứu sản phẩm và xử lý trường hợp không tìm thấy.
- [ ] **Tiết 2 – Bộ nhớ đệm:** Tải và kiểm tra dữ liệu bằng `DataTable` và `SqlDataAdapter`.
- [ ] **Tiết 3 – Stored Procedure:** Tạo và chạy được `sp_GetRevenueByCashier`.
- [ ] **Tiết 3 – API báo cáo:** Kiểm thử endpoint báo cáo và đối chiếu kết quả với SQL Server.
- [ ] **Tiết 4 – ACID:** Giải thích được bốn đặc tính Atomicity, Consistency, Isolation và Durability.
- [ ] **Tiết 4 – Giao dịch:** Sử dụng đúng `BeginTransaction()`, `Commit()` và `Rollback()`.
- [ ] **Tiết 5 – Thanh toán:** Kiểm thử quy trình tạo hóa đơn, lưu chi tiết, trừ kho và cộng điểm thưởng.
- [ ] **Tiết 5 – Rollback:** Kiểm chứng giao dịch thất bại không để lại dữ liệu cập nhật dở dang.
- [ ] **Bài tập mở rộng:** Nếu đã thực hiện, kiểm thử API hủy hóa đơn và đối soát việc hoàn trả tồn kho, điểm thưởng.

---

## 7. Kết luận

Buổi 5 giúp củng cố kiến thức ADO.NET và cách tổ chức các thao tác dữ liệu trong hệ thống bán hàng HAPPY MART. Qua việc xây dựng API tra cứu sản phẩm, Stored Procedure báo cáo doanh thu và quy trình thanh toán POS, sinh viên hiểu rõ hơn vai trò của việc truy vấn an toàn, xử lý ngoại lệ và bảo đảm tính toàn vẹn dữ liệu.

Đặc biệt, `SqlTransaction` kết hợp với `Commit()` và `Rollback()` giúp bảo vệ các thao tác liên quan trong một giao dịch, hạn chế tình trạng hóa đơn, chi tiết bán hàng, tồn kho và điểm thưởng bị cập nhật không đồng bộ.

Các ví dụ mã nguồn và kết quả kiểm thử trong tài liệu là nội dung hướng dẫn thực hành. Việc xác nhận chức năng đã hoàn thành cần dựa trên mã nguồn hiện có, kết quả chạy API và dữ liệu thực tế trong cơ sở dữ liệu.

**HAPPY MART – POS thu ngân tích hợp quản lý kho hàng**


