//********** HOTEN: VO THI THUY AN
//********** LOP: CCQ2411C
//********** MSSV: 2124110097
//********** MON: LAP TRINH UNG DUNG
//********** NGAYTHUCHIEN: 03/10/2026
//********** DE TAI: HAPPY MART - POS THU NGAN TICH HOP QUAN LY KHO

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MiniSupermarket.API.Data;
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
        private readonly SupermarketDbContext _context;

        // Constructor
        public AuthController(
            IConfiguration configuration,
            SupermarketDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        // ============================================================
        // ĐĂNG NHẬP
        // POST: /api/auth/login
        // ============================================================

        [HttpPost("login")]
        public async Task<IActionResult> Login(
            [FromBody] LoginRequestDto request)
        {
            // Kiểm tra dữ liệu đầu vào
            if (request == null ||
                string.IsNullOrWhiteSpace(request.Username) ||
                string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Vui lòng nhập đầy đủ tài khoản và mật khẩu!"
                });
            }

            // Tìm tài khoản trong bảng Users
            var user = await _context.Users
                .FirstOrDefaultAsync(u =>
                    u.Username == request.Username &&
                    u.Password == request.Password);

            // Không tìm thấy tài khoản
            if (user == null)
            {
                return Unauthorized(new
                {
                    success = false,
                    message = "Sai tài khoản hoặc mật khẩu!"
                });
            }

            // Tạo JWT Token
            var token = GenerateJwtToken(
                user.Username,
                user.Role
            );

            // Đăng nhập thành công
            return Ok(new
            {
                success = true,
                token = token,
                role = user.Role,
                username = user.Username,
                fullName = user.FullName
            });
        }

        // ============================================================
        // TẠO JWT TOKEN
        // ============================================================

        private string GenerateJwtToken(
            string username,
            string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            // Lấy Secret Key từ appsettings.json
            var secretKey =
                _configuration["JwtSettings:Secret"]
                ?? "SupermarketSecretKeyDoAnMonHoc2026SecureString!!";

            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new[]
                    {
                        new Claim(
                            ClaimTypes.Name,
                            username
                        ),

                        new Claim(
                            ClaimTypes.Role,
                            role
                        )
                    }
                ),

                // Token có hiệu lực 2 giờ
                Expires = DateTime.UtcNow.AddHours(2),

                SigningCredentials =
                    new SigningCredentials(
                        new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature
                    )
            };

            var token =
                tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }

    // ================================================================
    // DTO ĐĂNG NHẬP
    // ================================================================

    public class LoginRequestDto
    {
        public string Username { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}