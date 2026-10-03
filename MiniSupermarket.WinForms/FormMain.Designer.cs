namespace MiniSupermarket.WinForms
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            pnlHeader = new Panel();
            lblUserInfo = new Label();
            lblTitle = new Label();
            pnlMenu = new Panel();
            btnLogout = new Button();
            btnUserManagement = new Button();
            btnReport = new Button();
            btnCustomer = new Button();
            btnProduct = new Button();
            btnCategory = new Button();
            btnPOS = new Button();
            lblMenuTitle = new Label();
            pnlContent = new Panel();
            lblWelcome = new Label();
            pnlHeader.SuspendLayout();
            pnlMenu.SuspendLayout();
            pnlContent.SuspendLayout();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.FromArgb(111, 78, 55);
            pnlHeader.Controls.Add(lblUserInfo);
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(1280, 70);
            pnlHeader.TabIndex = 0;
            // 
            // lblUserInfo
            // 
            lblUserInfo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblUserInfo.AutoSize = true;
            lblUserInfo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblUserInfo.ForeColor = Color.White;
            lblUserInfo.Location = new Point(1000, 25);
            lblUserInfo.Name = "lblUserInfo";
            lblUserInfo.Size = new Size(312, 23);
            lblUserInfo.TabIndex = 1;
            lblUserInfo.Text = "Xin chào | Quyền: CHƯA ĐĂNG NHẬP";
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(25, 15);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(297, 46);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "\U0001f6d2 HAPPY MART";
            // 
            // pnlMenu
            // 
            pnlMenu.BackColor = Color.FromArgb(245, 235, 220);
            pnlMenu.Controls.Add(btnLogout);
            pnlMenu.Controls.Add(btnUserManagement);
            pnlMenu.Controls.Add(btnReport);
            pnlMenu.Controls.Add(btnCustomer);
            pnlMenu.Controls.Add(btnProduct);
            pnlMenu.Controls.Add(btnCategory);
            pnlMenu.Controls.Add(btnPOS);
            pnlMenu.Controls.Add(lblMenuTitle);
            pnlMenu.Dock = DockStyle.Left;
            pnlMenu.Location = new Point(0, 70);
            pnlMenu.Name = "pnlMenu";
            pnlMenu.Size = new Size(230, 680);
            pnlMenu.TabIndex = 1;
            // 
            // btnLogout
            // 
            btnLogout.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnLogout.BackColor = Color.FromArgb(90, 60, 45);
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            btnLogout.ForeColor = Color.White;
            btnLogout.Location = new Point(15, 610);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(200, 50);
            btnLogout.TabIndex = 7;
            btnLogout.Text = "🚪  Đăng xuất";
            btnLogout.UseVisualStyleBackColor = false;
            btnLogout.Click += btnLogout_Click;
            // 
            // btnUserManagement
            // 
            btnUserManagement.BackColor = Color.FromArgb(160, 120, 85);
            btnUserManagement.FlatAppearance.BorderSize = 0;
            btnUserManagement.FlatStyle = FlatStyle.Flat;
            btnUserManagement.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            btnUserManagement.ForeColor = Color.White;
            btnUserManagement.Location = new Point(15, 355);
            btnUserManagement.Name = "btnUserManagement";
            btnUserManagement.Size = new Size(200, 50);
            btnUserManagement.TabIndex = 6;
            btnUserManagement.Text = "🛡  Tài khoản";
            btnUserManagement.UseVisualStyleBackColor = false;
            btnUserManagement.Click += btnUserManagement_Click;
            // 
            // btnReport
            // 
            btnReport.BackColor = Color.FromArgb(160, 120, 85);
            btnReport.FlatAppearance.BorderSize = 0;
            btnReport.FlatStyle = FlatStyle.Flat;
            btnReport.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            btnReport.ForeColor = Color.White;
            btnReport.Location = new Point(15, 295);
            btnReport.Name = "btnReport";
            btnReport.Size = new Size(200, 50);
            btnReport.TabIndex = 5;
            btnReport.Text = "📊  Báo cáo";
            btnReport.UseVisualStyleBackColor = false;
            btnReport.Click += btnReport_Click;
            // 
            // btnCustomer
            // 
            btnCustomer.BackColor = Color.FromArgb(160, 120, 85);
            btnCustomer.FlatAppearance.BorderSize = 0;
            btnCustomer.FlatStyle = FlatStyle.Flat;
            btnCustomer.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            btnCustomer.ForeColor = Color.White;
            btnCustomer.Location = new Point(15, 235);
            btnCustomer.Name = "btnCustomer";
            btnCustomer.Size = new Size(200, 50);
            btnCustomer.TabIndex = 4;
            btnCustomer.Text = "👥  Khách hàng";
            btnCustomer.UseVisualStyleBackColor = false;
            btnCustomer.Click += btnCustomer_Click;
            // 
            // btnProduct
            // 
            btnProduct.BackColor = Color.FromArgb(160, 120, 85);
            btnProduct.FlatAppearance.BorderSize = 0;
            btnProduct.FlatStyle = FlatStyle.Flat;
            btnProduct.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            btnProduct.ForeColor = Color.White;
            btnProduct.Location = new Point(15, 175);
            btnProduct.Name = "btnProduct";
            btnProduct.Size = new Size(200, 50);
            btnProduct.TabIndex = 3;
            btnProduct.Text = "📦  Sản phẩm & Kho";
            btnProduct.UseVisualStyleBackColor = false;
            btnProduct.Click += btnProduct_Click;
            // 
            // btnCategory
            // 
            btnCategory.BackColor = Color.FromArgb(160, 120, 85);
            btnCategory.FlatAppearance.BorderSize = 0;
            btnCategory.FlatStyle = FlatStyle.Flat;
            btnCategory.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            btnCategory.ForeColor = Color.White;
            btnCategory.Location = new Point(15, 115);
            btnCategory.Name = "btnCategory";
            btnCategory.Size = new Size(200, 50);
            btnCategory.TabIndex = 2;
            btnCategory.Text = "📁  Nhóm hàng";
            btnCategory.UseVisualStyleBackColor = false;
            btnCategory.Click += btnCategory_Click;
            // 
            // btnPOS
            // 
            btnPOS.BackColor = Color.FromArgb(111, 78, 55);
            btnPOS.FlatAppearance.BorderSize = 0;
            btnPOS.FlatStyle = FlatStyle.Flat;
            btnPOS.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            btnPOS.ForeColor = Color.White;
            btnPOS.Location = new Point(15, 55);
            btnPOS.Name = "btnPOS";
            btnPOS.Size = new Size(200, 50);
            btnPOS.TabIndex = 1;
            btnPOS.Text = "\U0001f6d2  Bán hàng POS";
            btnPOS.UseVisualStyleBackColor = false;
            btnPOS.Click += btnPOS_Click;
            // 
            // lblMenuTitle
            // 
            lblMenuTitle.AutoSize = true;
            lblMenuTitle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblMenuTitle.ForeColor = Color.FromArgb(111, 78, 55);
            lblMenuTitle.Location = new Point(22, 18);
            lblMenuTitle.Name = "lblMenuTitle";
            lblMenuTitle.Size = new Size(228, 30);
            lblMenuTitle.TabIndex = 0;
            lblMenuTitle.Text = "HỆ THỐNG QUẢN LÝ";
            // 
            // pnlContent
            // 
            pnlContent.BackColor = Color.FromArgb(250, 248, 245);
            pnlContent.Controls.Add(lblWelcome);
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Location = new Point(230, 70);
            pnlContent.Name = "pnlContent";
            pnlContent.Size = new Size(1050, 680);
            pnlContent.TabIndex = 2;
            pnlContent.Paint += pnlContent_Paint;
            // 
            // lblWelcome
            // 
            lblWelcome.Dock = DockStyle.Fill;
            lblWelcome.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblWelcome.ForeColor = Color.FromArgb(111, 78, 55);
            lblWelcome.Location = new Point(0, 0);
            lblWelcome.Name = "lblWelcome";
            lblWelcome.Size = new Size(1050, 680);
            lblWelcome.TabIndex = 0;
            lblWelcome.Text = "\U0001f6d2 HAPPY MART\r\n\r\nHỆ THỐNG POS THU NGÂN\r\nTÍCH HỢP QUẢN LÝ KHO";
            lblWelcome.TextAlign = ContentAlignment.MiddleCenter;
            lblWelcome.Click += lblWelcome_Click;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1280, 750);
            Controls.Add(pnlContent);
            Controls.Add(pnlMenu);
            Controls.Add(pnlHeader);
            MinimumSize = new Size(1100, 650);
            Name = "FormMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Happy Mart - POS & Quản lý kho";
            Load += FormMain_Load;
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlMenu.ResumeLayout(false);
            pnlMenu.PerformLayout();
            pnlContent.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        // ============================================================
        // CONTROLS
        // ============================================================

        private Panel pnlHeader;
        private Label lblTitle;
        private Label lblUserInfo;

        private Panel pnlMenu;
        private Label lblMenuTitle;

        private Button btnPOS;
        private Button btnCategory;
        private Button btnProduct;
        private Button btnCustomer;
        private Button btnReport;
        private Button btnUserManagement;
        private Button btnLogout;

        private Panel pnlContent;
        private Label lblWelcome;
    }
}