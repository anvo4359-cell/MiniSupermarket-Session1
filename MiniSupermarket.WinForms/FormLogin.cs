//********** HOTEN: VO THI THUY AN
//********** LOP: CCQ2411C
//********** MSSV: 2124110097
//********** MON: LAP TRINH UNG DUNG
//********** NGAY THUCHIEN: 03/10/2026
//********** DE TAI: HAPPY MART - POS THU NGAN TICH HOP QUAN LY KHO

using System.Net.Http.Json;
using System.Text.Json;

namespace MiniSupermarket.WinForms
{
    public partial class FormLogin : Form
    {
        // ============================================================
        // KẾT NỐI ĐẾN WEB API
        // ============================================================

        private static readonly HttpClient _client = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5037/api/")
        };

        // ============================================================
        // CONSTRUCTOR
        // ============================================================

        public FormLogin()
        {
            InitializeComponent();
        }

        // ============================================================
        // XỬ LÝ ĐĂNG NHẬP
        // ============================================================

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUser.Text.Trim();
            string password = txtPass.Text.Trim();

            // ========================================================
            // KIỂM TRA DỮ LIỆU NHẬP
            // ========================================================

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
                // ====================================================
                // DỮ LIỆU ĐĂNG NHẬP GỬI LÊN API
                // ====================================================

                var loginData = new
                {
                    Username = username,
                    Password = password
                };

                // ====================================================
                // GỌI API:
                // POST /api/auth/login
                // ====================================================

                var response = await _client.PostAsJsonAsync(
                    "auth/login",
                    loginData
                );

                // ====================================================
                // ĐĂNG NHẬP THÀNH CÔNG
                // ====================================================

                if (response.IsSuccessStatusCode)
                {
                    string jsonString =
                        await response.Content.ReadAsStringAsync();

                    using var doc =
                        JsonDocument.Parse(jsonString);

                    // =================================================
                    // LƯU JWT TOKEN
                    // =================================================

                    SessionManager.JwtToken =
                        doc.RootElement
                            .GetProperty("token")
                            .GetString()
                        ?? string.Empty;

                    // =================================================
                    // LƯU ROLE
                    // =================================================

                    SessionManager.CurrentRole =
                        doc.RootElement
                            .GetProperty("role")
                            .GetString()
                        ?? string.Empty;

                    SessionManager.CurrentUsername = username;

                    // =================================================
                    // THÔNG BÁO ĐĂNG NHẬP THÀNH CÔNG
                    // =================================================

                    MessageBox.Show(
                        $"Đăng nhập thành công!\n\n" +
                        $"Tài khoản: {username}\n" +
                        $"Quyền: {SessionManager.CurrentRole}",
                        "Đăng nhập thành công",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    // =================================================
                    // TRẢ KẾT QUẢ VỀ CHO FORMMMAIN
                    // =================================================

                    DialogResult = DialogResult.OK;

                    // Đóng FormLogin
                    Close();
                }
                else
                {
                    // =================================================
                    // ĐĂNG NHẬP THẤT BẠI
                    // =================================================

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
                // ====================================================
                // LỖI KẾT NỐI API
                // ====================================================

                MessageBox.Show(
                    "Lỗi kết nối đến Server:\n\n" +
                    ex.Message,
                    "Lỗi hệ thống",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // ============================================================
        // CÁC EVENT CỦA FORM
        // ============================================================

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