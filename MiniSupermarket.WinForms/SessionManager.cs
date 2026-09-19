//********** HOTEN: VO THI THUY AN
//********** LOP: CCQ2411C
//********** MSSV: 2124110097
//********** MON: LAP TRINH UNG DUNG
//********** NGAY THUCHIEN: 19/9/2026

using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace MiniSupermarket.WinForms
{
    /// <summary>
    /// [CHỨC NĂNG 1] Lớp tĩnh quản lý phiên làm việc (Session)
    /// Ý nghĩa: Lưu trữ tạm thời Token và Quyền của người dùng sau khi đăng nhập thành công 
    /// để các Form khác trong ứng dụng có thể sử dụng lại mà không cần phải hỏi lại mật khẩu.
    /// </summary>
    public static class SessionManager
    {
        // Lưu trữ chuỗi JWT Token (mã thông hành) nhận từ Server
        public static string JwtToken { get; set; } = string.Empty;

        // Lưu trữ vai trò người dùng (Ví dụ: Admin, Cashier) để phân quyền hiển thị giao diện
        public static string CurrentRole { get; set; } = string.Empty;
    }

    /// <summary>
    /// [CHỨC NĂNG 2] Lớp dịch vụ kết nối Web API (ApiClientService)
    /// Ý nghĩa: Đảm nhận toàn bộ việc gửi yêu cầu (Request) và nhận phản hồi (Response) từ Server Backend.
    /// </summary>
    public static class ApiClientService
    {
        // Khởi tạo một HttpClient duy nhất cho toàn ứng dụng để tối ưu kết nối mạng, tránh tràn cổng (socket exhaustion).
        private static readonly HttpClient _client = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7123/api/") // Lưu ý: Thay đổi port cho khớp với API của bạn
        };

        /// <summary>
        /// [CHỨC NĂNG 3] Hàm gửi yêu cầu đăng nhập (LoginAsync)
        /// Ý nghĩa: Gửi tài khoản và mật khẩu lên Server, nếu đúng sẽ tự động bóc tách lưu Token và Role vào SessionManager.
        /// </summary>
        public static async Task<bool> LoginAsync(string username, string password)
        {
            // Đóng gói thông tin đăng nhập thành dạng đối tượng JSON
            var loginObj = new { Username = username, Password = password };

            // Gửi request kiểu POST tới API đăng nhập
            var response = await _client.PostAsJsonAsync("auth/login", loginObj);

            // Kiểm tra xem Server phản hồi thành công hay không (Mã 200-299)
            if (response.IsSuccessStatusCode)
            {
                // Đọc chuỗi JSON trả về từ Server
                var jsonString = await response.Content.ReadAsStringAsync();

                // Dùng JsonDocument để bóc tách lấy riêng token và role lưu vào bộ nhớ tạm SessionManager
                using var doc = JsonDocument.Parse(jsonString);
                SessionManager.JwtToken = doc.RootElement.GetProperty("token").GetString() ?? string.Empty;
                SessionManager.CurrentRole = doc.RootElement.GetProperty("role").GetString() ?? string.Empty;

                return true; // Đăng nhập thành công
            }
            return false; // Đăng nhập thất bại (Sai tài khoản/mật khẩu)
        }

        /// <summary>
        /// [CHỨC NĂNG 4] Hàm gọi dữ liệu bảo mật kèm Token (GetDataWithTokenAsync)
        /// Ý nghĩa: Gắn mã thông hành (Bearer Token) vào Header của yêu cầu để lấy dữ liệu từ các API yêu cầu quyền [Authorize].
        /// </summary>
        public static async Task<string> GetDataWithTokenAsync(string endpoint)
        {
            // Đính kèm JWT Token vào tiêu đề Authorization dưới dạng "Bearer [Token]"
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionManager.JwtToken);

            // Gửi request lấy dữ liệu (GET)
            var response = await _client.GetAsync(endpoint);

            // Nếu thành công -> Trả về dữ liệu dạng chuỗi JSON
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            // Nếu Server báo lỗi 401 Unauthorized -> Token hết hạn hoặc chưa đăng nhập
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new Exception("Phiên làm việc hết hạn hoặc chưa đăng nhập!");
            }

            // Các lỗi phát sinh khác
            throw new Exception("Lỗi khi gọi dữ liệu từ Server.");
        }
    }
}
