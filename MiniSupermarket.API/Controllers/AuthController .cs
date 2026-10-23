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
using MiniSupermarket.API.Models;
using MiniSupermarket.API.Security;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Microsoft.AspNetCore.Authorization.AllowAnonymous]
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
                .FirstOrDefaultAsync(u => u.Username == request.Username.Trim());

            if (user == null ||
                (!PasswordSecurity.Verify(request.Password, user.Password) &&
                 !string.Equals(user.Password, request.Password, StringComparison.Ordinal)))
            {
                return Unauthorized(new
                {
                    success = false,
                    message = "Sai tài khoản hoặc mật khẩu!"
                });
            }

            // Tạo JWT Token
            var (token, expiresAt) = GenerateJwtToken(
                user.UserId,
                user.Username,
                user.Role
            );

            // Nâng cấp tài khoản cũ đang lưu mật khẩu thô ngay sau lần đăng nhập hợp lệ.
            if (!user.Password.StartsWith("PBKDF2-SHA256$", StringComparison.Ordinal))
            {
                user.Password = PasswordSecurity.Hash(request.Password);
                await _context.SaveChangesAsync();
            }

            // Đăng nhập thành công
            return Ok(new LoginResponseDto(token, user.Username, user.Role, user.FullName, expiresAt));
        }

        // ============================================================
        // TẠO JWT TOKEN
        // ============================================================

        private (string Token, DateTime ExpiresAt) GenerateJwtToken(
            int userId,
            string username,
            string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            // Lấy Secret Key từ appsettings.json
            var secretKey = _configuration["JwtSettings:Secret"]!;
            var issuer = _configuration["JwtSettings:Issuer"]!;
            var audience = _configuration["JwtSettings:Audience"]!;
            var expiresAt = DateTime.UtcNow.AddHours(2);

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
                        new Claim(ClaimTypes.NameIdentifier, userId.ToString()),

                        new Claim(
                            ClaimTypes.Role,
                            role
                        )
                    }
                ),

                Issuer = issuer,
                Audience = audience,
                Expires = expiresAt,

                SigningCredentials =
                    new SigningCredentials(
                        new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature
                    )
            };

            var token =
                tokenHandler.CreateToken(tokenDescriptor);

            return (tokenHandler.WriteToken(token), expiresAt);
        }
    }
}