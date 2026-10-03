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
        // ============================================================
        // FORM CON HIỆN TẠI
        // ============================================================

        private Form? currentForm;

        // ============================================================
        // CONSTRUCTOR
        // ============================================================

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
            // Ẩn FormMain trong lúc đăng nhập
            this.Hide();

            using (FormLogin loginForm = new FormLogin())
            {
                DialogResult result = loginForm.ShowDialog(this);

                // Nếu đăng nhập thất bại hoặc người dùng đóng Login
                if (result != DialogResult.OK)
                {
                    Application.Exit();
                    return;
                }
            }

            // Hiển thị thông tin người dùng
            UpdateUserInformation();

            // Phân quyền menu
            ApplyRolePermissions();

            // Mở màn hình mặc định theo quyền
            OpenDefaultScreen();

            // Hiện FormMain sau khi đăng nhập thành công
            this.Show();
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

            // ========================================================
            // ADMIN
            // ========================================================

            if (role == "admin")
            {
                btnPOS.Visible = true;
                btnCategory.Visible = true;
                btnProduct.Visible = true;
                btnCustomer.Visible = true;
                btnReport.Visible = true;
                btnUserManagement.Visible = true;
            }

            // ========================================================
            // CASHIER - THU NGÂN
            // ========================================================

            else if (role == "cashier")
            {
                btnPOS.Visible = true;
                btnCustomer.Visible = true;
            }

            // ========================================================
            // WAREHOUSE - THỦ KHO
            // ========================================================

            else if (role == "warehouse")
            {
                btnCategory.Visible = true;
                btnProduct.Visible = true;
            }

            // ========================================================
            // TRƯỜNG HỢP ROLE KHÔNG HỢP LỆ
            // ========================================================

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

            if (role == "admin")
            {
                ShowProduct();
            }
            else if (role == "cashier")
            {
                ShowPOS();
            }
            else if (role == "warehouse")
            {
                ShowProduct();
            }
        }

        // ============================================================
        // MỞ FORM POS
        // ============================================================

        private void ShowPOS()
        {
            // FormPOS sẽ được tạo ở bước tiếp theo.
            ShowComingSoon("POS Thu ngân");
        }

        // ============================================================
        // MỞ FORM CATEGORY
        // ============================================================

        private void ShowCategory()
        {
            OpenChildForm(new FormCategoryManagement());
        }

        // ============================================================
        // MỞ FORM PRODUCT
        // ============================================================

        private void ShowProduct()
        {
            OpenChildForm(new FormProductManagement());
        }

        // ============================================================
        // MỞ FORM CUSTOMER
        // ============================================================

        private void ShowCustomer()
        {
            OpenChildForm(new FormCustomerManagement());
        }

        // ============================================================
        // BÁO CÁO
        // ============================================================

        private void ShowReport()
        {
            // Bảo vệ tầng code: chỉ Admin được xem báo cáo
            if (SessionManager.CurrentRole.Trim().ToLower() != "admin")
            {
                MessageBox.Show(
                    "Bạn không có quyền xem dữ liệu tài chính của siêu thị!",
                    "Từ chối",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Stop
                );

                return;
            }

            OpenChildForm(new FormQuickReport());
        }

        // ============================================================
        // QUẢN LÝ TÀI KHOẢN
        // ============================================================

        // ============================================================
        // QUẢN LÝ TÀI KHOẢN
        // ============================================================

        private void ShowUserManagement()
        {
            OpenChildForm(new FormUserManagement());
        }

        // ============================================================
        // THÔNG BÁO CHỨC NĂNG CHƯA LÀM
        // ============================================================

        private void ShowComingSoon(string featureName)
        {
            pnlContent.Controls.Clear();

            Label lbl = new Label();

            lbl.Text =
                $"HAPPY MART\n\n" +
                $"{featureName}\n\n" +
                "Chức năng đang được xây dựng...";

            lbl.Dock = DockStyle.Fill;
            lbl.TextAlign = ContentAlignment.MiddleCenter;
            lbl.Font = new Font(
                "Segoe UI",
                18F,
                FontStyle.Bold
            );
            lbl.ForeColor = Color.FromArgb(111, 78, 55);

            pnlContent.Controls.Add(lbl);
        }

        // ============================================================
        // MỞ FORM CON TRONG PANEL
        // ============================================================

        private void OpenChildForm(Form childForm)
        {
            // Đóng Form con hiện tại
            if (currentForm != null)
            {
                currentForm.Close();
                currentForm.Dispose();
                currentForm = null;
            }

            currentForm = childForm;

            // Cấu hình Form con
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            // Xóa nội dung cũ
            pnlContent.Controls.Clear();

            // Thêm Form con vào panel
            pnlContent.Controls.Add(childForm);

            childForm.BringToFront();
            childForm.Show();
        }

        // ============================================================
        // NÚT POS
        // ============================================================

        private void btnPOS_Click(object sender, EventArgs e)
        {
            ShowPOS();
        }

        // ============================================================
        // NÚT CATEGORY
        // ============================================================

        private void btnCategory_Click(object sender, EventArgs e)
        {
            ShowCategory();
        }

        // ============================================================
        // NÚT PRODUCT
        // ============================================================

        private void btnProduct_Click(object sender, EventArgs e)
        {
            ShowProduct();
        }

        // ============================================================
        // NÚT CUSTOMER
        // ============================================================

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            ShowCustomer();
        }

        // ============================================================
        // NÚT REPORT
        // ============================================================

        private void btnReport_Click(object sender, EventArgs e)
        {
            ShowReport();
        }

        // ============================================================
        // NÚT USER MANAGEMENT
        // ============================================================

        private void btnUserManagement_Click(object sender, EventArgs e)
        {
            ShowUserManagement();
        }

        // ============================================================
        // ĐĂNG XUẤT
        // ============================================================

        private void btnLogout_Click(
            object sender,
            EventArgs e)
        {
            DialogResult result =
                MessageBox.Show(
                    "Bạn có chắc chắn muốn đăng xuất khỏi Happy Mart?",
                    "Đăng xuất",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

            if (result != DialogResult.Yes)
            {
                return;
            }

            // ============================================================
            // XÓA SESSION HIỆN TẠI
            // ============================================================

            SessionManager.JwtToken = string.Empty;
            SessionManager.CurrentRole = string.Empty;

            // ============================================================
            // ĐÓNG FORM CON ĐANG MỞ
            // ============================================================

            if (currentForm != null)
            {
                currentForm.Close();
                currentForm.Dispose();
                currentForm = null;
            }

            // ============================================================
            // ẨN FORM MAIN
            // ============================================================

            this.Hide();

            // ============================================================
            // MỞ LẠI FORM LOGIN
            // ============================================================

            using (FormLogin loginForm = new FormLogin())
            {
                DialogResult loginResult =
                    loginForm.ShowDialog(this);

                // Nếu đăng nhập lại thành công
                if (loginResult == DialogResult.OK)
                {
                    // Cập nhật thông tin user mới
                    UpdateUserInformation();

                    // Cập nhật quyền
                    ApplyRolePermissions();

                    // Mở màn hình mặc định
                    OpenDefaultScreen();

                    // Hiện lại FormMain
                    this.Show();
                }
                else
                {
                    // Nếu đóng Login thì thoát chương trình
                    Application.Exit();
                }
            }
        }

        // ============================================================
        // FORM CLOSING
        // ============================================================

        private void FormMain_FormClosing(
            object? sender,
            FormClosingEventArgs e)
        {
            if (currentForm != null)
            {
                currentForm.Close();
                currentForm.Dispose();
                currentForm = null;
            }
        }

        // ============================================================
        // PANEL CONTENT PAINT
        // ============================================================

        private void pnlContent_Paint(
            object sender,
            PaintEventArgs e)
        {
        }

        private void lblWelcome_Click(object sender, EventArgs e)
        {

        }
    }
}