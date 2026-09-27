
using System;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    public partial class FormMain : Form
    {
        // Form con hiện tại
        private Form? currentForm;

        public FormMain()
        {
            InitializeComponent();
            this.Load += FormMain_Load;
        }

        // ============================================================
        // FORM LOAD
        // ============================================================

        private void FormMain_Load(object sender, EventArgs e)
        {
            // Khi mở chương trình sẽ hiển thị Login
            ShowLogin();
        }

        // ============================================================
        // HIỂN THỊ LOGIN
        // ============================================================

        private void ShowLogin()
        {
            OpenChildForm(new FormLogin());
        }

        // ============================================================
        // HIỂN THỊ CATEGORY
        // ============================================================

        private void ShowCategory()
        {
            OpenChildForm(new FormCategoryManagement());
        }

        // ============================================================
        // HIỂN THỊ PRODUCT
        // ============================================================

        private void ShowProduct()
        {
            OpenChildForm(new FormProductManagement());
        }

        // ============================================================
        // HIỂN THỊ CUSTOMER
        // ============================================================

        private void ShowCustomer()
        {
            OpenChildForm(new FormCustomerManagement());
        }

        // ============================================================
        // MỞ FORM CON TRONG PANEL
        // ============================================================

        private void OpenChildForm(Form childForm)
        {
            // Đóng form đang mở
            if (currentForm != null)
            {
                currentForm.Close();
                currentForm.Dispose();
            }

            currentForm = childForm;

            // Cấu hình form con
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            // Xóa nội dung cũ
            pnlContent.Controls.Clear();

            // Đưa form con vào panel
            pnlContent.Controls.Add(childForm);

            childForm.BringToFront();
            childForm.Show();
        }

        // ============================================================
        // NÚT ĐĂNG NHẬP
        // ============================================================

        private void btnLogin_Click(object sender, EventArgs e)
        {
            ShowLogin();
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

        private void pnlContent_Paint(object sender, PaintEventArgs e)
        {
        }
    }
}