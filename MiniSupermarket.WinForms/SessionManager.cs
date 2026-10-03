//********** HOTEN: VO THI THUY AN
//********** LOP: CCQ2411C
//********** MSSV: 2124110097
//********** MON: LAP TRINH UNG DUNG
//********** NGAY THUCHIEN: 03/10/2026
//********** DE TAI: HAPPY MART - POS THU NGAN TICH HOP QUAN LY KHO

using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace MiniSupermarket.WinForms
{
    /// <summary>
    /// Quản lý thông tin phiên đăng nhập của người dùng.
    /// Lưu JWT Token, Username và Role để các Form khác có thể sử dụng.
    /// </summary>
    public static class SessionManager
    {
        // JWT Token nhận được từ Web API sau khi đăng nhập thành công
        public static string JwtToken { get; set; } = string.Empty;

        // Tên tài khoản đang đăng nhập
        public static string CurrentUsername { get; set; } = string.Empty;

        // Quyền của tài khoản: Admin / Cashier / Warehouse
        public static string CurrentRole { get; set; } = string.Empty;
    }

    /// <summary>
    /// Lớp dịch vụ kết nối Web API.
    /// Chịu trách nhiệm gửi Request và nhận Response từ Backend.
    /// </summary>
    public static class ApiClientService
    {
        // Địa chỉ Web API
        private static readonly HttpClient _client = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5037/api/")
        };

        /// <summary>
        /// Đăng nhập vào hệ thống.
        /// Nếu thành công sẽ lưu Token, Username và Role vào SessionManager.
        /// </summary>
        public static async Task<bool> LoginAsync(
            string username,
            string password)
        {
            var loginObj = new
            {
                Username = username,
                Password = password
            };

            var response = await _client.PostAsJsonAsync(
                "auth/login",
                loginObj
            );

            if (response.IsSuccessStatusCode)
            {
                string jsonString =
                    await response.Content.ReadAsStringAsync();

                using var doc =
                    JsonDocument.Parse(jsonString);

                // Lưu JWT Token
                SessionManager.JwtToken =
                    doc.RootElement
                        .GetProperty("token")
                        .GetString()
                    ?? string.Empty;

                // Lưu Username
                SessionManager.CurrentUsername = username;

                // Lưu Role
                SessionManager.CurrentRole =
                    doc.RootElement
                        .GetProperty("role")
                        .GetString()
                    ?? string.Empty;

                return true;
            }

            return false;
        }

        /// <summary>
        /// Gọi API GET có yêu cầu JWT Token.
        /// </summary>
        public static async Task<string> GetDataWithTokenAsync(
            string endpoint)
        {
            // Gắn JWT Token vào Header
            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(
                    "Bearer",
                    SessionManager.JwtToken
                );

            var response =
                await _client.GetAsync(endpoint);

            // API trả về thành công
            if (response.IsSuccessStatusCode)
            {
                return await response.Content
                    .ReadAsStringAsync();
            }

            // API trả về 401
            if (response.StatusCode ==
                System.Net.HttpStatusCode.Unauthorized)
            {
                throw new Exception(
                    "Phiên làm việc hết hạn hoặc chưa đăng nhập!"
                );
            }

            // Các lỗi khác
            throw new Exception(
                "Lỗi khi gọi dữ liệu từ Server."
            );
        }
    }
}