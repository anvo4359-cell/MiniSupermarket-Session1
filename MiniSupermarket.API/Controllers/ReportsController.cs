
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace MiniSupermarket.API.Controllers
{
    [ApiController]
    [Route("api/reports")]
    public class ReportsController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ReportsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

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
    }
}
