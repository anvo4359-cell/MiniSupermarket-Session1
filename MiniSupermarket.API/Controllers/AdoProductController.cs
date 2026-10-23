/*
 * Ho ten sinh vien: VoThiThuyAn
 * Ma sinh vien: 2124110097
 * Ngay tao: 10/10/2026
 * Mo ta: Controller ADO.NET thuan - Tra cuu ma vach sieu toc va demo DataTable
 */
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Data.SqlClient;
using System.Data;

namespace MiniSupermarket.API.Controllers
{
    public class ProductLookupDto
    {
        public int ProductId { get; set; }
        public string Barcode { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
    }

    [Route("api/ado-products")]
    [ApiController]
    [Authorize(Roles = "Admin,Cashier,Warehouse")]
    public class AdoProductController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AdoProductController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // API 1: Tra cuu san pham bang barcode
        [HttpGet("barcode/{barcode}")]
        public IActionResult GetProductByBarcode(string barcode)
        {
            if (string.IsNullOrWhiteSpace(barcode))
            {
                return BadRequest(new
                {
                    message = "Ma vach khong duoc de trong!"
                });
            }

            string connectionString =
                _configuration.GetConnectionString("DefaultConnection")!;

            using (SqlConnection connection =
                   new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT ProductId, Barcode, ProductName,
                           Price, StockQuantity
                    FROM Products
                    WHERE Barcode = @Barcode";

                using (SqlCommand command =
                       new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue(
                        "@Barcode", barcode.Trim());

                    try
                    {
                        connection.Open();

                        using (SqlDataReader reader =
                               command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                var product = new ProductLookupDto
                                {
                                    ProductId = Convert.ToInt32(
                                        reader["ProductId"]),
                                    Barcode = reader["Barcode"].ToString()
                                        ?? string.Empty,
                                    ProductName = reader["ProductName"].ToString()
                                        ?? string.Empty,
                                    Price = Convert.ToDecimal(
                                        reader["Price"]),
                                    StockQuantity = Convert.ToInt32(
                                        reader["StockQuantity"])
                                };

                                return Ok(product);
                            }

                            return NotFound(new
                            {
                                message = "Khong tim thay san pham!"
                            });
                        }
                    }
                    catch (SqlException ex)
                    {
                        return StatusCode(500, new
                        {
                            message = "Loi truy van SQL Server.",
                            error = ex.Message
                        });
                    }
                }
            }
        }

        // API 2: Lay danh sach san pham bang DataTable
        [HttpGet("list")]
        public IActionResult GetProductList()
        {
            string connectionString =
                _configuration.GetConnectionString("DefaultConnection")!;

            DataTable productTable = new DataTable();

            string query = @"
                SELECT ProductId, Barcode, ProductName,
                       Price, StockQuantity
                FROM Products
                ORDER BY ProductId ASC";

            try
            {
                using (SqlConnection connection =
                       new SqlConnection(connectionString))
                using (SqlCommand command =
                       new SqlCommand(query, connection))
                using (SqlDataAdapter adapter =
                       new SqlDataAdapter(command))
                {
                    adapter.Fill(productTable);
                }

                var resultList = new List<ProductLookupDto>();

                foreach (DataRow row in productTable.Rows)
                {
                    resultList.Add(new ProductLookupDto
                    {
                        ProductId = Convert.ToInt32(row["ProductId"]),
                        Barcode = row["Barcode"].ToString()
                            ?? string.Empty,
                        ProductName = row["ProductName"].ToString()
                            ?? string.Empty,
                        Price = Convert.ToDecimal(row["Price"]),
                        StockQuantity = Convert.ToInt32(
                            row["StockQuantity"])
                    });
                }

                return Ok(new
                {
                    totalLoaded = productTable.Rows.Count,
                    cachedAt = DateTime.Now.ToString(
                        "yyyy-MM-dd HH:mm:ss"),
                    data = resultList
                });
            }
            catch (SqlException ex)
            {
                return StatusCode(500, new
                {
                    message = "Loi truy van SQL Server.",
                    error = ex.Message
                });
            }
        }
    }
}
