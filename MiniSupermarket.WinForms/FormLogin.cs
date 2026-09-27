using System.Net.Http.Json;
using System.Text.Json;

namespace MiniSupermarket.WinForms
{
    public partial class FormLogin : Form
    {
        // Kết nối đến Web API
        private static readonly HttpClient _client = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5037/api/")
        };

        public FormLogin()
        {
            InitializeComponent();
        }

        // ==============================
        // XỬ LÝ ĐĂNG NHẬP
        // ==============================
        private async void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUser.Text.Trim();
            string password = txtPass.Text.Trim();

            // Kiểm tra tài khoản và mật khẩu
            if (string.IsNullOrEmpty(username) ||
                string.IsNullOrEmpty(password))
            {
                MessageBox.Show(
                    "Vui lòng nhập đầy đủ tài khoản và mật khẩu!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            try
            {
                // Dữ liệu gửi lên API
                var loginData = new
                {
                    Username = username,
                    Password = password
                };

                // Gọi POST /api/auth/login
                var response = await _client.PostAsJsonAsync(
                    "auth/login",
                    loginData
                );

                // ==============================
                // ĐĂNG NHẬP THÀNH CÔNG
                // ==============================
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

                    // Lưu Role
                    SessionManager.CurrentRole =
                        doc.RootElement
                            .GetProperty("role")
                            .GetString()
                        ?? string.Empty;

                    MessageBox.Show(
                        $"Đăng nhập thành công!\n\n" +
                        $"Tài khoản: {username}\n" +
                        $"Quyền: {SessionManager.CurrentRole}\n\n" +
                        $"JWT Token đã được cấp.",
                        "Đăng nhập thành công",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    // ==============================
                    // BUỔI 2 CHỈ TEST LOGIN + JWT
                    // CHƯA MỞ FORM BUỔI 1
                    // ==============================

                    // Không mở FormCategoryManagement
                    // Không chuyển sang màn hình khác
                }
                else
                {
                    string errorMessage =
                        await response.Content.ReadAsStringAsync();

                    MessageBox.Show(
                        "Sai tài khoản hoặc mật khẩu!\n\n" +
                        $"Server trả về: {errorMessage}",
                        "Đăng nhập thất bại",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi kết nối đến Server:\n\n" +
                    ex.Message,
                    "Lỗi hệ thống",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // ==============================
        // CÁC EVENT CỦA FORM
        // ==============================

        private void txtPass_TextChanged(object sender, EventArgs e)
        {
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}