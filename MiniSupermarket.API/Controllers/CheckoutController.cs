
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MiniSupermarket.API.Models;
using System.Data;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckoutController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public CheckoutController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // POST: api/checkout
        [HttpPost]
        public IActionResult ProcessCheckout(
            [FromBody] CheckoutRequestDto request)
        {
            if (request.Items == null || request.Items.Count == 0)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Giỏ hàng rỗng, không thể thanh toán!"
                });
            }

            if (string.IsNullOrWhiteSpace(request.CashierUsername))
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Thiếu tài khoản thu ngân!"
                });
            }

            if (request.CustomerId <= 0)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "CustomerId không hợp lệ!"
                });
            }

            if (request.Items.Any(i =>
                i.ProductId <= 0 ||
                i.Quantity <= 0 ||
                i.UnitPrice < 0))
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Thông tin sản phẩm hoặc số lượng không hợp lệ!"
                });
            }

            string connectionString =
                _configuration.GetConnectionString("DefaultConnection")!;

            using var connection = new SqlConnection(connectionString);
            connection.Open();

            using SqlTransaction transaction =
                connection.BeginTransaction();

            try
            {
                decimal finalAmount = 0;
                int totalItems = request.Items.Sum(i => i.Quantity);

                // Kiểm tra sản phẩm, giá và tồn kho
                foreach (var item in request.Items)
                {
                    const string checkSql = @"
                        SELECT ProductName, Price, StockQuantity
                        FROM dbo.Products WITH (UPDLOCK, HOLDLOCK)
                        WHERE ProductId = @ProductId";

                    using var checkCommand =
                        new SqlCommand(checkSql, connection, transaction);

                    checkCommand.Parameters.Add(
                        "@ProductId", SqlDbType.Int).Value = item.ProductId;

                    using var reader = checkCommand.ExecuteReader();

                    if (!reader.Read())
                    {
                        throw new InvalidOperationException(
                            $"Không tìm thấy sản phẩm ID {item.ProductId}!");
                    }

                    string productName = reader["ProductName"].ToString()!;
                    decimal price = Convert.ToDecimal(reader["Price"]);
                    int stock = Convert.ToInt32(reader["StockQuantity"]);

                    reader.Close();

                    if (stock < item.Quantity)
                    {
                        throw new InvalidOperationException(
                            $"Sản phẩm '{productName}' không đủ tồn kho! " +
                            $"Tồn kho: {stock}, yêu cầu: {item.Quantity}.");
                    }

                    // Lấy giá từ database, không tin giá do client gửi lên
                    item.UnitPrice = price;
                    finalAmount += price * item.Quantity;
                }

                // Quy tắc: mỗi 10.000 đồng được 1 điểm
                int rewardPoints = (int)(finalAmount / 10000m);

                string orderCode =
                    $"HD-{DateTime.Now:yyyyMMdd}-{Guid.NewGuid():N}"
                    .Substring(0, 18).ToUpper();

                // Tạo hóa đơn tổng
                const string insertOrderSql = @"
                    INSERT INTO dbo.Orders
                    (
                        OrderCode, CustomerId, CashierUsername,
                        CreatedDate, TotalAmount, FinalAmount, RewardPoints
                    )
                    OUTPUT INSERTED.OrderId
                    VALUES
                    (
                        @OrderCode, @CustomerId, @CashierUsername,
                        SYSDATETIME(), @TotalAmount, @FinalAmount, @RewardPoints
                    )";

                int orderId;

                using (var command =
                    new SqlCommand(insertOrderSql, connection, transaction))
                {
                    command.Parameters.Add(
                        "@OrderCode", SqlDbType.NVarChar, 50).Value = orderCode;

                    command.Parameters.Add(
                        "@CustomerId", SqlDbType.Int).Value = request.CustomerId;

                    command.Parameters.Add(
                        "@CashierUsername", SqlDbType.NVarChar, 100)
                        .Value = request.CashierUsername.Trim();

                    command.Parameters.Add(
                        "@TotalAmount", SqlDbType.Decimal).Value = finalAmount;

                    command.Parameters["@TotalAmount"].Precision = 18;
                    command.Parameters["@TotalAmount"].Scale = 2;

                    command.Parameters.Add(
                        "@FinalAmount", SqlDbType.Decimal).Value = finalAmount;

                    command.Parameters["@FinalAmount"].Precision = 18;
                    command.Parameters["@FinalAmount"].Scale = 2;

                    command.Parameters.Add(
                        "@RewardPoints", SqlDbType.Int).Value = rewardPoints;

                    orderId = Convert.ToInt32(command.ExecuteScalar());
                }

                // Lưu chi tiết hóa đơn và trừ tồn kho
                foreach (var item in request.Items)
                {
                    const string detailSql = @"
                        INSERT INTO dbo.OrderDetails
                            (OrderId, ProductId, Quantity, UnitPrice)
                        VALUES
                            (@OrderId, @ProductId, @Quantity, @UnitPrice)";

                    using (var detailCommand =
                        new SqlCommand(detailSql, connection, transaction))
                    {
                        detailCommand.Parameters.Add(
                            "@OrderId", SqlDbType.Int).Value = orderId;

                        detailCommand.Parameters.Add(
                            "@ProductId", SqlDbType.Int).Value = item.ProductId;

                        detailCommand.Parameters.Add(
                            "@Quantity", SqlDbType.Int).Value = item.Quantity;

                        var unitPriceParameter = detailCommand.Parameters.Add(
                            "@UnitPrice", SqlDbType.Decimal);

                        unitPriceParameter.Precision = 18;
                        unitPriceParameter.Scale = 2;
                        unitPriceParameter.Value = item.UnitPrice;

                        detailCommand.ExecuteNonQuery();
                    }

                    const string updateStockSql = @"
                        UPDATE dbo.Products
                        SET StockQuantity = StockQuantity - @Quantity
                        WHERE ProductId = @ProductId
                          AND StockQuantity >= @Quantity";

                    using (var stockCommand =
                        new SqlCommand(updateStockSql, connection, transaction))
                    {
                        stockCommand.Parameters.Add(
                            "@Quantity", SqlDbType.Int).Value = item.Quantity;

                        stockCommand.Parameters.Add(
                            "@ProductId", SqlDbType.Int).Value = item.ProductId;

                        if (stockCommand.ExecuteNonQuery() != 1)
                        {
                            throw new InvalidOperationException(
                                $"Không thể cập nhật tồn kho sản phẩm ID {item.ProductId}.");
                        }
                    }
                }

                // Cộng điểm thưởng cho khách hàng
                const string updatePointsSql = @"
                    UPDATE dbo.Customers
                    SET RewardPoints = RewardPoints + @Points
                    WHERE CustomerId = @CustomerId";

                using (var customerCommand =
                    new SqlCommand(updatePointsSql, connection, transaction))
                {
                    customerCommand.Parameters.Add(
                        "@Points", SqlDbType.Int).Value = rewardPoints;

                    customerCommand.Parameters.Add(
                        "@CustomerId", SqlDbType.Int).Value = request.CustomerId;

                    if (customerCommand.ExecuteNonQuery() != 1)
                    {
                        throw new InvalidOperationException(
                            "Không tìm thấy khách hàng để cộng điểm!");
                    }
                }

                // Tất cả thành công mới lưu dữ liệu
                transaction.Commit();

                return Ok(new
                {
                    success = true,
                    message = "Thanh toán hóa đơn POS thành công!",
                    orderId,
                    orderCode,
                    totalItems,
                    finalAmount,
                    rewardPointsAdded = rewardPoints
                });
            }
            catch (Exception ex)
            {
                try
                {
                    transaction.Rollback();
                }
                catch
                {
                    // Kết nối có thể đã gặp lỗi trong lúc rollback
                }

                return BadRequest(new
                {
                    success = false,
                    message = "Giao dịch thanh toán bị hủy và đã được Rollback!",
                    errorDetail = ex.Message
                });
            }
        }

        // DELETE: api/checkout/cancel/{orderId}
        [HttpDelete("cancel/{orderId}")]
        public IActionResult CancelOrder(int orderId)
        {
            if (orderId <= 0)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "OrderId không hợp lệ!"
                });
            }

            string connectionString =
                _configuration.GetConnectionString("DefaultConnection")!;

            using var connection = new SqlConnection(connectionString);
            connection.Open();

            using SqlTransaction transaction = connection.BeginTransaction();

            try
            {
                // 1. Kiểm tra hóa đơn, lấy khách hàng và điểm thưởng
                const string getOrderSql = @"
                    SELECT CustomerId, RewardPoints
                    FROM dbo.Orders WITH (UPDLOCK, HOLDLOCK)
                    WHERE OrderId = @OrderId";

                int? customerId = null;
                int pointsToDeduct = 0;

                using (var command =
                    new SqlCommand(getOrderSql, connection, transaction))
                {
                    command.Parameters.Add("@OrderId", SqlDbType.Int)
                        .Value = orderId;

                    using var reader = command.ExecuteReader();

                    if (!reader.Read())
                    {
                        throw new InvalidOperationException(
                            $"Không tìm thấy hóa đơn ID {orderId}!");
                    }

                    if (reader["CustomerId"] != DBNull.Value)
                    {
                        customerId = Convert.ToInt32(reader["CustomerId"]);
                    }

                    pointsToDeduct =
                        Convert.ToInt32(reader["RewardPoints"]);
                }

                // 2. Lấy danh sách sản phẩm và số lượng cần hoàn kho
                var itemsToRestore = new List<(int ProductId, int Quantity)>();

                const string getDetailsSql = @"
                    SELECT ProductId, Quantity
                    FROM dbo.OrderDetails
                    WHERE OrderId = @OrderId";

                using (var command =
                    new SqlCommand(getDetailsSql, connection, transaction))
                {
                    command.Parameters.Add("@OrderId", SqlDbType.Int)
                        .Value = orderId;

                    using var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        itemsToRestore.Add((
                            Convert.ToInt32(reader["ProductId"]),
                            Convert.ToInt32(reader["Quantity"])
                        ));
                    }
                }

                // 3. Cộng trả tồn kho từng sản phẩm
                foreach (var item in itemsToRestore)
                {
                    const string restoreStockSql = @"
                        UPDATE dbo.Products
                        SET StockQuantity = StockQuantity + @Quantity
                        WHERE ProductId = @ProductId";

                    using var command = new SqlCommand(
                        restoreStockSql, connection, transaction);

                    command.Parameters.Add("@Quantity", SqlDbType.Int)
                        .Value = item.Quantity;

                    command.Parameters.Add("@ProductId", SqlDbType.Int)
                        .Value = item.ProductId;

                    if (command.ExecuteNonQuery() != 1)
                    {
                        throw new InvalidOperationException(
                            $"Không tìm thấy sản phẩm ID {item.ProductId}!");
                    }
                }

                // 4. Trừ lại điểm thưởng đã cấp cho khách hàng
                if (customerId.HasValue && pointsToDeduct > 0)
                {
                    const string updatePointsSql = @"
                        UPDATE dbo.Customers
                        SET RewardPoints =
                            CASE
                                WHEN RewardPoints >= @Points
                                THEN RewardPoints - @Points
                                ELSE 0
                            END
                        WHERE CustomerId = @CustomerId";

                    using var command = new SqlCommand(
                        updatePointsSql, connection, transaction);

                    command.Parameters.Add("@Points", SqlDbType.Int)
                        .Value = pointsToDeduct;

                    command.Parameters.Add("@CustomerId", SqlDbType.Int)
                        .Value = customerId.Value;

                    if (command.ExecuteNonQuery() != 1)
                    {
                        throw new InvalidOperationException(
                            "Không tìm thấy khách hàng của hóa đơn!");
                    }
                }

                // 5. Xóa chi tiết hóa đơn trước
                const string deleteDetailsSql = @"
                    DELETE FROM dbo.OrderDetails
                    WHERE OrderId = @OrderId";

                using (var command =
                    new SqlCommand(deleteDetailsSql, connection, transaction))
                {
                    command.Parameters.Add("@OrderId", SqlDbType.Int)
                        .Value = orderId;

                    command.ExecuteNonQuery();
                }

                // 6. Xóa hóa đơn tổng
                const string deleteOrderSql = @"
                    DELETE FROM dbo.Orders
                    WHERE OrderId = @OrderId";

                using (var command =
                    new SqlCommand(deleteOrderSql, connection, transaction))
                {
                    command.Parameters.Add("@OrderId", SqlDbType.Int)
                        .Value = orderId;

                    if (command.ExecuteNonQuery() != 1)
                    {
                        throw new InvalidOperationException(
                            "Không thể xóa hóa đơn!");
                    }
                }

                // 7. Xác nhận hoàn tất
                transaction.Commit();

                return Ok(new
                {
                    success = true,
                    message = $"Đã hủy hóa đơn #{orderId} và hoàn trả tồn kho thành công!"
                });
            }
            catch (Exception ex)
            {
                try
                {
                    transaction.Rollback();
                }
                catch
                {
                    // Ghi nhận lỗi rollback nếu cần xử lý thêm
                }

                return BadRequest(new
                {
                    success = false,
                    message = "Hủy hóa đơn thất bại, giao dịch đã được Rollback!",
                    errorDetail = ex.Message
                });
            }
        }
    }
}
