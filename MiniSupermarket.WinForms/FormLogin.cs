//********** HOTEN: VO THI THUY AN
//********** LOP: CCQ2411C
//********** MSSV: 2124110097
//********** MON: LAP TRINH UNG DUNG
//********** NGAY THUCHIEN: 03/10/2026
//********** DE TAI: HAPPY MART - POS THU NGAN TICH HOP QUAN LY KHO

using System;
using System.Drawing;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    public partial class FormLogin : Form
    {
        // ============================================================
        // KẾT NỐI ĐẾN WEB API
        // ============================================================
        private static readonly HttpClient _client = new HttpClient
        {
            BaseAddress = new Uri(
                Environment.GetEnvironmentVariable("HAPPYMART_API_URL")
                ?? "http://localhost:5037/api/")
        };

        // BỘ MÀU TONE TỐI SANG TRỌNG: DARK NAVY & METALLIC GOLD
        private readonly Color navyPrimary = Color.FromArgb(11, 30, 54);     // #0B1E36 - Navy Đậm
        private readonly Color navyHeader = Color.FromArgb(15, 39, 71);      // #0F2747 - Navy Panel
        private readonly Color goldAccent = Color.FromArgb(212, 175, 55);   // #D4AF37 - Metallic Gold
        private readonly Color bgLight = Color.FromArgb(248, 249, 250);      // #F8F9FA - Nền bên phải
        private readonly Color textDark = Color.FromArgb(33, 37, 41);        // Màu chữ chính

        private CheckBox chkShowPassword;
        private Label lblStatus;

        // ============================================================
        // CONSTRUCTOR
        // ============================================================
        public FormLogin()
        {
            // Khởi tạo các Component truyền thống (nếu có Designer)
            InitializeComponent();

            // Dựng lại giao diện đồng bộ theo đề tài Happy Mart
            BuildUI();
        }

        // ============================================================
        // DỰNG GIAO DIỆN HIỆN ĐẠI BẰNG CODE
        // ============================================================
        private void BuildUI()
        {
            SuspendLayout();

            Text = "Happy Mart POS - Đăng Nhập Hệ Thống";
            Name = "FormLogin";
            Size = new Size(850, 500);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            BackColor = bgLight;
            Font = new Font("Segoe UI", 9F, FontStyle.Regular);

            // Xóa hết control cũ nếu tạo động để tránh đè giao diện
            Controls.Clear();

            // 1. BANNER BÊN TRÁI (BRANDING HAPPY MART)
            Panel leftPanel = new Panel
            {
                Dock = DockStyle.Left,
                Width = 360,
                BackColor = navyPrimary,
                Padding = new Padding(30)
            };

            Label lblBrandLogo = new Label
            {
                Text = "🛒 HAPPY MART",
                Font = new Font("Segoe UI", 22F, FontStyle.Bold),
                ForeColor = goldAccent,
                AutoSize = true,
                Location = new Point(25, 110)
            };

            Label lblBrandTitle = new Label
            {
                Text = "HỆ THỐNG POS THU NGÂN\r\n& QUẢN LÝ KHO HÀNG",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(28, 165)
            };

            Label lblBrandDesc = new Label
            {
                Text = "Giải pháp quản lý cửa hàng tiện lợi chuyên nghiệp, tối ưu quy trình bán hàng và kiểm soát kho hàng realtime.\r\n\r\nSVTH: Võ Thị Thúy An - 2124110097",
                Font = new Font("Segoe UI", 8.5F, FontStyle.Regular),
                ForeColor = Color.FromArgb(180, 195, 215),
                Size = new Size(300, 90),
                Location = new Point(28, 230)
            };

            Label lblFooterVersion = new Label
            {
                Text = "Lập Trình Ung Dụng | Class CCQ2411C",
                Font = new Font("Segoe UI", 8F, FontStyle.Italic),
                ForeColor = Color.FromArgb(120, 140, 165),
                AutoSize = true,
                Location = new Point(28, 420)
            };

            leftPanel.Controls.AddRange(new Control[]
            {
                lblBrandLogo, lblBrandTitle, lblBrandDesc, lblFooterVersion
            });

            // 2. KHUNG ĐĂNG NHẬP BÊN PHẢI (LOGIN PANEL)
            Panel rightPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(45, 40, 45, 30)
            };

            Label lblLoginHeader = new Label
            {
                Text = "ĐĂNG NHẬP",
                Font = new Font("Segoe UI", 20F, FontStyle.Bold),
                ForeColor = navyPrimary,
                AutoSize = true,
                Location = new Point(45, 40)
            };

            Label lblLoginSub = new Label
            {
                Text = "Vui lòng nhập tài khoản nhân viên để truy cập",
                Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                ForeColor = Color.Gray,
                AutoSize = true,
                Location = new Point(47, 80)
            };

            // Ô nhập Tài khoản
            Label lblUser = new Label
            {
                Text = "Tên đăng nhập / Mã nhân viên:",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = navyPrimary,
                AutoSize = true,
                Location = new Point(47, 125)
            };

            if (txtUser == null) txtUser = new TextBox();
            txtUser.Location = new Point(47, 150);
            txtUser.Size = new Size(380, 30);
            txtUser.Font = new Font("Segoe UI", 11F);
            txtUser.BorderStyle = BorderStyle.FixedSingle;

            // Ô nhập Mật khẩu
            Label lblPass = new Label
            {
                Text = "Mật khẩu:",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = navyPrimary,
                AutoSize = true,
                Location = new Point(47, 200)
            };

            if (txtPass == null) txtPass = new TextBox();
            txtPass.Location = new Point(47, 225);
            txtPass.Size = new Size(380, 30);
            txtPass.Font = new Font("Segoe UI", 11F);
            txtPass.UseSystemPasswordChar = true;
            txtPass.BorderStyle = BorderStyle.FixedSingle;

            // Checkbox Ẩn/Hiện Mật khẩu
            chkShowPassword = new CheckBox
            {
                Text = "Hiển thị mật khẩu",
                Font = new Font("Segoe UI", 8.5F),
                ForeColor = textDark,
                Location = new Point(47, 263),
                AutoSize = true,
                Cursor = Cursors.Hand
            };
            chkShowPassword.CheckedChanged += (s, e) =>
            {
                txtPass.UseSystemPasswordChar = !chkShowPassword.Checked;
            };

            // Label thông báo trạng thái
            lblStatus = new Label
            {
                Text = "",
                Font = new Font("Segoe UI", 8.5F, FontStyle.Italic),
                ForeColor = Color.Red,
                Size = new Size(380, 25),
                Location = new Point(47, 290),
                TextAlign = ContentAlignment.MiddleLeft
            };

            // Nút Đăng nhập
            if (btnLogin == null) btnLogin = new Button();
            btnLogin.Text = "ĐĂNG NHẬP";
            btnLogin.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnLogin.BackColor = goldAccent;
            btnLogin.ForeColor = navyPrimary;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Size = new Size(240, 42);
            btnLogin.Location = new Point(47, 325);
            btnLogin.Cursor = Cursors.Hand;
            btnLogin.FlatAppearance.BorderSize = 0;

            // 🛑 LƯU Ý QUAN TRỌNG: Đảm bảo DialogResult của nút bấm là None để không bị đóng Form sớm
            btnLogin.DialogResult = DialogResult.None;
            btnLogin.Click -= btnLogin_Click; // gỡ event cũ nếu có
            btnLogin.Click += btnLogin_Click;

            // Nút Thoát
            Button btnExit = new Button
            {
                Text = "Thoát",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Regular),
                BackColor = Color.FromArgb(240, 240, 240),
                ForeColor = textDark,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(125, 42),
                Location = new Point(302, 325),
                Cursor = Cursors.Hand
            };
            btnExit.FlatAppearance.BorderSize = 0;
            btnExit.Click += (s, e) => Application.Exit();

            // Nhấn Enter để thực hiện Đăng nhập nhanh
            AcceptButton = btnLogin;

            rightPanel.Controls.AddRange(new Control[]
            {
                lblLoginHeader, lblLoginSub,
                lblUser, txtUser,
                lblPass, txtPass,
                chkShowPassword, lblStatus,
                btnLogin, btnExit
            });

            Controls.Add(rightPanel);
            Controls.Add(leftPanel);

            ResumeLayout(false);
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
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                lblStatus.ForeColor = Color.Red;
                lblStatus.Text = "Vui lòng nhập đầy đủ tài khoản và mật khẩu!";
                return;
            }

            btnLogin.Enabled = false;
            btnLogin.Text = "Đang xử lý...";
            lblStatus.ForeColor = navyPrimary;
            lblStatus.Text = "Đang xác thực thông tin với Server...";

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
                // GỌI API: POST /api/auth/login
                // ====================================================
                var response = await _client.PostAsJsonAsync("auth/login", loginData);

                // ====================================================
                // ĐĂNG NHẬP THÀNH CÔNG
                // ====================================================
                if (response.IsSuccessStatusCode)
                {
                    string jsonString = await response.Content.ReadAsStringAsync();

                    using var doc = JsonDocument.Parse(jsonString);

                    // LƯU JWT TOKEN
                    SessionManager.JwtToken = doc.RootElement.TryGetProperty("token", out var tokenElem)
                        ? tokenElem.GetString() ?? string.Empty
                        : string.Empty;

                    // LƯU ROLE
                    SessionManager.CurrentRole = doc.RootElement.TryGetProperty("role", out var roleElem)
                        ? roleElem.GetString() ?? string.Empty
                        : string.Empty;

                    SessionManager.CurrentUsername = username;

                    lblStatus.ForeColor = Color.Green;
                    lblStatus.Text = "Đăng nhập thành công!";

                    MessageBox.Show(
                        $"Đăng nhập thành công!\n\n" +
                        $"Tài khoản: {username}\n" +
                        $"Quyền: {SessionManager.CurrentRole}",
                        "Happy Mart POS",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    // =================================================
                    // TRẢ KẾT QUẢ VỀ CHO FORMMMAIN HOẶC PROGRAM.CS
                    // =================================================
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    // =================================================
                    // ĐĂNG NHẬP THẤT BẠI
                    // =================================================
                    string errorMessage = await response.Content.ReadAsStringAsync();

                    lblStatus.ForeColor = Color.Red;
                    lblStatus.Text = "Sai tài khoản hoặc mật khẩu!";

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
                lblStatus.ForeColor = Color.Red;
                lblStatus.Text = "Lỗi kết nối đến Server!";

                MessageBox.Show(
                    "Lỗi kết nối đến Server:\n\n" + ex.Message,
                    "Lỗi hệ thống",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            finally
            {
                btnLogin.Enabled = true;
                btnLogin.Text = "ĐĂNG NHẬP";
            }
        }

        // ============================================================
        // CÁC EVENT PHỤ CỦA FORM
        // ============================================================
        private void txtPass_TextChanged(object sender, EventArgs e) { }
        private void groupBox1_Enter(object sender, EventArgs e) { }
        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void FormLogin_Load(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
    }
}