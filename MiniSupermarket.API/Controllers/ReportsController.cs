
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Data.SqlClient;
using System.Data;

namespace MiniSupermarket.API.Controllers
{
    [ApiController]
    [Route("api/reports")]
    [Authorize(Roles = "Admin,Warehouse")]
    public class ReportsController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ReportsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("products.csv")]
        public async Task<IActionResult> ExportProductsCsv()
        {
            const string sql = @"
                SELECT p.Barcode, p.ProductName, c.CategoryName,
                       p.Price, p.StockQuantity
                FROM dbo.Products p
                LEFT JOIN dbo.Categories c ON c.CategoryId = p.CategoryId
                ORDER BY p.ProductName;";

            try
            {
                await using var connection = new SqlConnection(
                    _configuration.GetConnectionString("DefaultConnection"));
                await connection.OpenAsync();
                await using var command = new SqlCommand(sql, connection);
                await using var reader = await command.ExecuteReaderAsync();
                var csv = new System.Text.StringBuilder();
                csv.AppendLine("Barcode,ProductName,CategoryName,Price,StockQuantity");

                while (await reader.ReadAsync())
                {
                    csv.AppendLine(string.Join(",",
                        Csv(reader["Barcode"]),
                        Csv(reader["ProductName"]),
                        Csv(reader["CategoryName"]),
                        Convert.ToDecimal(reader["Price"]).ToString("0.00", System.Globalization.CultureInfo.InvariantCulture),
                        Convert.ToInt32(reader["StockQuantity"])));
                }

                var utf8 = new System.Text.UTF8Encoding(encoderShouldEmitUTF8Identifier: true);
                byte[] content = utf8.GetPreamble()
                    .Concat(utf8.GetBytes(csv.ToString()))
                    .ToArray();
                return File(content, "text/csv; charset=utf-8", $"happymart-products-{DateTime.Today:yyyyMMdd}.csv");
            }
            catch (SqlException)
            {
                return StatusCode(500, new { message = "Không thể xuất danh sách sản phẩm." });
            }
        }

        private static string Csv(object value)
        {
            string text = value == DBNull.Value ? string.Empty : value.ToString() ?? string.Empty;
            return $"\"{text.Replace("\"", "\"\"")}\"";
        }

        [HttpGet("dashboard-summary")]
        public async Task<IActionResult> GetDashboardSummary()
        {
            const string sql = @"
                SELECT
                    (SELECT COALESCE(SUM(FinalAmount), 0) FROM dbo.Orders
                     WHERE CreatedDate >= CONVERT(date, SYSDATETIME())
                       AND CreatedDate < DATEADD(day, 1, CONVERT(date, SYSDATETIME()))) AS RevenueToday,
                    (SELECT COUNT(*) FROM dbo.Orders) AS TotalOrders,
                    (SELECT COUNT(*) FROM dbo.Customers) AS TotalCustomers,
                    (SELECT COUNT(*) FROM dbo.Products WHERE StockQuantity <= 10) AS LowStockProducts;";

            try
            {
                await using var connection = new SqlConnection(
                    _configuration.GetConnectionString("DefaultConnection"));
                await connection.OpenAsync();
                await using var command = new SqlCommand(sql, connection);
                await using var reader = await command.ExecuteReaderAsync();

                if (!await reader.ReadAsync())
                {
                    return StatusCode(500, new { message = "Không đọc được số liệu dashboard." });
                }

                return Ok(new
                {
                    revenueToday = reader.GetDecimal(reader.GetOrdinal("RevenueToday")),
                    totalOrders = reader.GetInt32(reader.GetOrdinal("TotalOrders")),
                    totalCustomers = reader.GetInt32(reader.GetOrdinal("TotalCustomers")),
                    lowStockProducts = reader.GetInt32(reader.GetOrdinal("LowStockProducts"))
                });
            }
            catch (SqlException)
            {
                return StatusCode(500, new { message = "Không thể truy vấn số liệu dashboard từ SQL Server." });
            }
        }

        [HttpGet("top-products")]
        public async Task<IActionResult> GetTopProducts([FromQuery] int take = 5)
        {
            if (take is < 1 or > 20)
            {
                return BadRequest(new { message = "Số lượng sản phẩm phải từ 1 đến 20." });
            }

            const string sql = @"
                SELECT TOP (@Take)
                    p.ProductId,
                    p.ProductName,
                    p.Barcode,
                    SUM(od.Quantity) AS QuantitySold,
                    SUM(od.Quantity * od.UnitPrice) AS Revenue
                FROM dbo.OrderDetails od
                INNER JOIN dbo.Products p ON p.ProductId = od.ProductId
                INNER JOIN dbo.Orders o ON o.OrderId = od.OrderId
                GROUP BY p.ProductId, p.ProductName, p.Barcode
                ORDER BY QuantitySold DESC, p.ProductName;";

            try
            {
                await using var connection = new SqlConnection(
                    _configuration.GetConnectionString("DefaultConnection"));
                await connection.OpenAsync();
                await using var command = new SqlCommand(sql, connection);
                command.Parameters.Add("@Take", SqlDbType.Int).Value = take;
                await using var reader = await command.ExecuteReaderAsync();
                var results = new List<object>();

                while (await reader.ReadAsync())
                {
                    results.Add(new
                    {
                        productId = reader.GetInt32(reader.GetOrdinal("ProductId")),
                        productName = reader.GetString(reader.GetOrdinal("ProductName")),
                        barcode = reader.GetString(reader.GetOrdinal("Barcode")),
                        quantitySold = reader.GetInt32(reader.GetOrdinal("QuantitySold")),
                        revenue = reader.GetDecimal(reader.GetOrdinal("Revenue"))
                    });
                }

                return Ok(results);
            }
            catch (SqlException)
            {
                return StatusCode(500, new { message = "Không thể truy vấn sản phẩm bán chạy từ SQL Server." });
            }
        }

        // API CŨ: DOANH THU THEO THU NGÂN
        [HttpGet("revenue-by-cashier")]
        public async Task<IActionResult> GetRevenueByCashier()
        {
            var connectionString =
                _configuration.GetConnectionString("DefaultConnection");

            var data = new List<object>();

            try
            {
                using var connection =
                    new SqlConnection(connectionString);

                await connection.OpenAsync();

                using var command = new SqlCommand(
                    "dbo.sp_GetRevenueByCashier", connection);

                command.CommandType = CommandType.StoredProcedure;

                using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    data.Add(new
                    {
                        CashierUsername =
                            reader["CashierUsername"].ToString(),

                        TotalOrders =
                            Convert.ToInt32(reader["TotalOrders"]),

                        TotalRevenue =
                            reader["TotalRevenue"] == DBNull.Value
                                ? 0m
                                : Convert.ToDecimal(reader["TotalRevenue"]),

                        TotalPointsAwarded =
                            Convert.ToInt32(reader["TotalPointsAwarded"])
                    });
                }

                return Ok(new
                {
                    success = true,
                    totalCashiers = data.Count,
                    data
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        // API MỚI: BÁO CÁO CHI TIẾT THEO NGÀY
        // Ví dụ: GET /api/reports/daily?date=2026-10-15
        [HttpGet("daily")]
        public async Task<IActionResult> GetDailyReport(
            [FromQuery] DateTime? date)
        {
            DateTime reportDate = (date ?? DateTime.Today).Date;

            var connectionString =
                _configuration.GetConnectionString("DefaultConnection");

            try
            {
                using var connection =
                    new SqlConnection(connectionString);

                await connection.OpenAsync();

                // 1. Tổng số hóa đơn và doanh thu trong ngày
                int totalOrders;
                decimal totalRevenue;

                const string summarySql = @"
                    SELECT
                        COUNT(*) AS TotalOrders,
                        COALESCE(SUM(FinalAmount), 0) AS TotalRevenue
                    FROM dbo.Orders
                    WHERE CreatedDate >= @StartDate
                      AND CreatedDate < @EndDate;";

                using (var command = new SqlCommand(
                    summarySql, connection))
                {
                    AddDateParameters(command, reportDate);

                    using var reader = await command.ExecuteReaderAsync();

                    await reader.ReadAsync();

                    totalOrders = Convert.ToInt32(reader["TotalOrders"]);
                    totalRevenue = Convert.ToDecimal(reader["TotalRevenue"]);
                }

                // 2. Danh sách hóa đơn trong ngày
                var orders = new List<object>();

                const string ordersSql = @"
                    SELECT
                        OrderId,
                        OrderCode,
                        CashierUsername,
                        CreatedDate,
                        TotalAmount,
                        FinalAmount,
                        RewardPoints
                    FROM dbo.Orders
                    WHERE CreatedDate >= @StartDate
                      AND CreatedDate < @EndDate
                    ORDER BY CreatedDate DESC;";

                using (var command = new SqlCommand(
                    ordersSql, connection))
                {
                    AddDateParameters(command, reportDate);

                    using var reader = await command.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        orders.Add(new
                        {
                            OrderId = Convert.ToInt32(reader["OrderId"]),
                            OrderCode = reader["OrderCode"]?.ToString(),
                            CashierUsername =
                                reader["CashierUsername"]?.ToString(),
                            CreatedDate =
                                Convert.ToDateTime(reader["CreatedDate"]),
                            TotalAmount =
                                Convert.ToDecimal(reader["TotalAmount"]),
                            FinalAmount =
                                Convert.ToDecimal(reader["FinalAmount"]),
                            RewardPoints =
                                Convert.ToInt32(reader["RewardPoints"])
                        });
                    }
                }

                // 3. Chi tiết sản phẩm bán trong ngày
                var productSales = new List<object>();

                const string productsSql = @"
                    SELECT
                        p.ProductId,
                        p.ProductName,
                        SUM(od.Quantity) AS QuantitySold,
                        od.UnitPrice,
                        SUM(od.Quantity * od.UnitPrice) AS Revenue
                    FROM dbo.Orders o
                    INNER JOIN dbo.OrderDetails od
                        ON o.OrderId = od.OrderId
                    INNER JOIN dbo.Products p
                        ON od.ProductId = p.ProductId
                    WHERE o.CreatedDate >= @StartDate
                      AND o.CreatedDate < @EndDate
                    GROUP BY
                        p.ProductId,
                        p.ProductName,
                        od.UnitPrice
                    ORDER BY QuantitySold DESC, Revenue DESC;";

                using (var command = new SqlCommand(
                    productsSql, connection))
                {
                    AddDateParameters(command, reportDate);

                    using var reader = await command.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        productSales.Add(new
                        {
                            ProductId =
                                Convert.ToInt32(reader["ProductId"]),
                            ProductName =
                                reader["ProductName"]?.ToString(),
                            QuantitySold =
                                Convert.ToInt32(reader["QuantitySold"]),
                            UnitPrice =
                                Convert.ToDecimal(reader["UnitPrice"]),
                            Revenue =
                                Convert.ToDecimal(reader["Revenue"])
                        });
                    }
                }

                // 4. Mặt hàng bán chạy nhất
                object? bestSeller = productSales.Count > 0
                    ? productSales[0]
                    : null;

                return Ok(new
                {
                    success = true,
                    reportDate = reportDate.ToString("yyyy-MM-dd"),
                    totalOrders,
                    totalRevenue,
                    bestSeller,
                    orders,
                    productSales
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        private static void AddDateParameters(
            SqlCommand command, DateTime reportDate)
        {
            command.Parameters.Add(
                "@StartDate", SqlDbType.DateTime2).Value = reportDate;

            command.Parameters.Add(
                "@EndDate", SqlDbType.DateTime2).Value =
                reportDate.AddDays(1);
        }
    }
}
