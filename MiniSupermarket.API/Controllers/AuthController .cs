//**********HOTEN: VO THI THUY AN
//**********lOP: CCQ2411C
//**********MSSV: 2124110097
//**********MON: LAP TRINH UNG DUNG
//**********NGAYTHUCHIEN: 19/9/2026

using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        // Constructor: Nhận cấu hình hệ thống (IConfiguration) qua Dependency Injection
        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Endpoint Đăng nhập: POST /api/auth/login
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDto request)
        {
            // Kiểm tra tài khoản mẫu (Trong thực tế sẽ truy vấn qua EF Core / SQL Server)
            if (request.Username == "admin" && request.Password == "123456")
            {
                var token = GenerateJwtToken(request.Username, "Admin");
                return Ok(new { success = true, token = token, role = "Admin" });
            }
            else if (request.Username == "cashier" && request.Password == "123456")
            {
                var token = GenerateJwtToken(request.Username, "Cashier");
                return Ok(new { success = true, token = token, role = "Cashier" });
            }

            return Unauthorized(new { success = false, message = "Sai tài khoản hoặc mật khẩu!" });
        }

        // Hàm phụ trợ: Tạo và ký mã JWT Token cho người dùng
        private string GenerateJwtToken(string username, string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            // Lấy khóa bí mật từ appsettings.json, nếu không có sẽ dùng chuỗi mặc định dự phòng
            var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:Secret"] ?? "SupermarketSecretKeyDoAnMonHoc2026SecureString!!");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                // Đưa thông tin định danh (Claims) vào trong token
                Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, role)
                }),
                Expires = DateTime.UtcNow.AddHours(2), // Thời hạn hiệu lực của Token là 2 giờ
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }

    // Lớp DTO định nghĩa cấu trúc dữ liệu người dùng gửi lên ở phần Body (JSON)
    public class LoginRequestDto
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}