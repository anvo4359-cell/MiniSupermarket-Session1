//********** HOTEN: VO THI THUY AN
//********** LOP: CCQ2411C
//********** MSSV: 2124110097
//********** MON: LAP TRINH UNG DUNG
//********** NGAY THUCHIEN: 03/10/2026
//********** DE TAI: HAPPY MART - POS THU NGAN TICH HOP QUAN LY KHO

using System;
using System.Drawing;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    public partial class FormMain : Form
    {
        private Form? currentForm;

        public FormMain()
        {
            InitializeComponent();

            this.Load += FormMain_Load;
            this.FormClosing += FormMain_FormClosing;
        }

        // ============================================================
        // FORM LOAD
        // ============================================================
        private void FormMain_Load(object sender, EventArgs e)
        {
            // LƯU Ý: Không gọi FormLogin ở đây nữa vì Program.cs đã gọi rồi!

            // Hiển thị thông tin người dùng từ SessionManager đã lưu
            UpdateUserInformation();

            // Phân quyền menu theo Role
            ApplyRolePermissions();

            // Mở màn hình mặc định theo quyền
            OpenDefaultScreen();
        }

        // ============================================================
        // HIỂN THỊ THÔNG TIN USER
        // ============================================================
        private void UpdateUserInformation()
        {
            string role = SessionManager.CurrentRole;

            if (string.IsNullOrWhiteSpace(role))
            {
                role = "Chưa xác định";
            }

            lblUserInfo.Text = $"Xin chào | Quyền: {role.ToUpper()}";
        }

        // ============================================================
        // PHÂN QUYỀN
        // ============================================================
        private void ApplyRolePermissions()
        {
            string role = SessionManager.CurrentRole.Trim().ToLower();

            // Mặc định khóa toàn bộ menu nghiệp vụ
            btnPOS.Visible = false;
            btnCategory.Visible = false;
            btnProduct.Visible = false;
            btnCustomer.Visible = false;
            btnReport.Visible = false;
            btnUserManagement.Visible = false;

            if (role == "admin")
            {
                btnPOS.Visible = true;
                btnCategory.Visible = true;
                btnProduct.Visible = true;
                btnCustomer.Visible = true;
                btnReport.Visible = true;
                btnUserManagement.Visible = true;
            }
            else if (role == "cashier")
            {
                btnPOS.Visible = true;
                btnCustomer.Visible = true;
            }
            else if (role == "warehouse")
            {
                btnCategory.Visible = true;
                btnProduct.Visible = true;
            }
            else
            {
                MessageBox.Show(
                    "Tài khoản chưa được cấp quyền sử dụng hệ thống.",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
        }

        // ============================================================
        // MỞ MÀN HÌNH MẶC ĐỊNH
        // ============================================================
        private void OpenDefaultScreen()
        {
            string role = SessionManager.CurrentRole.Trim().ToLower();

            if (role == "admin" || role == "warehouse")
            {
                ShowProduct();
            }
            else if (role == "cashier")
            {
                ShowPOS();
            }
        }

        private void ShowPOS()
        {
            string role = SessionManager.CurrentRole.Trim().ToLower();
            if (role != "admin" && role != "cashier")
            {
                MessageBox.Show("Bạn không có quyền sử dụng chức năng bán hàng.", "Happy Mart", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            OpenChildForm(new FormPOS());
        }

        private void ShowCategory() => OpenChildForm(new FormCategoryManagement());
        private void ShowProduct() => OpenChildForm(new FormProductManagement());
        private void ShowCustomer() => OpenChildForm(new FormCustomerManagement());

        private void ShowReport()
        {
            string role = SessionManager.CurrentRole.Trim().ToLower();
            if (role != "admin" && role != "warehouse")
            {
                MessageBox.Show("Bạn không có quyền xem dữ liệu tài chính của siêu thị!", "Từ chối", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            OpenChildForm(new FormQuickReport());
        }

        private void ShowUserManagement() => OpenChildForm(new FormUserManagement());

        private void OpenChildForm(Form childForm)
        {
            if (currentForm != null)
            {
                currentForm.Close();
                currentForm.Dispose();
                currentForm = null;
            }

            currentForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            pnlContent.Controls.Clear();
            pnlContent.Controls.Add(childForm);
            childForm.BringToFront();
            childForm.Show();
        }

        private void btnPOS_Click(object sender, EventArgs e) => ShowPOS();
        private void btnCategory_Click(object sender, EventArgs e) => ShowCategory();
        private void btnProduct_Click(object sender, EventArgs e) => ShowProduct();
        private void btnCustomer_Click(object sender, EventArgs e) => ShowCustomer();
        private void btnReport_Click(object sender, EventArgs e) => ShowReport();
        private void btnUserManagement_Click(object sender, EventArgs e) => ShowUserManagement();

        // ============================================================
        // ĐĂNG XUẤT
        // ============================================================
        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn đăng xuất khỏi Happy Mart?",
                "Đăng xuất",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result != DialogResult.Yes) return;

            // Xóa session
            SessionManager.JwtToken = string.Empty;
            SessionManager.CurrentRole = string.Empty;

            if (currentForm != null)
            {
                currentForm.Close();
                currentForm.Dispose();
                currentForm = null;
            }

            this.Hide();

            // Mở lại Form Login khi đăng xuất
            using (FormLogin loginForm = new FormLogin())
            {
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    UpdateUserInformation();
                    ApplyRolePermissions();
                    OpenDefaultScreen();
                    this.Show();
                }
                else
                {
                    Application.Exit();
                }
            }
        }

        private void FormMain_FormClosing(object? sender, FormClosingEventArgs e)
        {
            if (currentForm != null)
            {
                currentForm.Close();
                currentForm.Dispose();
                currentForm = null;
            }
        }

        private void pnlContent_Paint(object sender, PaintEventArgs e) { }
        private void lblWelcome_Click(object sender, EventArgs e) { }
    }
}