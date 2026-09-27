
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
            lblTitle = new Label();
            pnlMenu = new Panel();

            btnProduct = new Button();
            btnCustomer = new Button();
            btnCategory = new Button();
            btnLogin = new Button();

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
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(1200, 70);
            pnlHeader.TabIndex = 0;

            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font(
                "Segoe UI",
                20F,
                FontStyle.Bold
            );
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(25, 15);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(355, 46);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "MINI SUPERMARKET";

            // 
            // pnlMenu
            // 
            pnlMenu.BackColor = Color.FromArgb(245, 235, 220);

            pnlMenu.Controls.Add(btnProduct);
            pnlMenu.Controls.Add(btnCustomer);
            pnlMenu.Controls.Add(btnCategory);
            pnlMenu.Controls.Add(btnLogin);

            pnlMenu.Dock = DockStyle.Left;
            pnlMenu.Location = new Point(0, 70);
            pnlMenu.Name = "pnlMenu";
            pnlMenu.Size = new Size(220, 680);
            pnlMenu.TabIndex = 1;

            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.FromArgb(111, 78, 55);
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Font = new Font(
                "Segoe UI",
                11F,
                FontStyle.Bold
            );
            btnLogin.ForeColor = Color.White;
            btnLogin.Location = new Point(20, 40);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(180, 55);
            btnLogin.TabIndex = 0;
            btnLogin.Text = "Đăng nhập";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click;

            // 
            // btnCategory
            // 
            btnCategory.BackColor = Color.FromArgb(160, 120, 85);
            btnCategory.FlatAppearance.BorderSize = 0;
            btnCategory.FlatStyle = FlatStyle.Flat;
            btnCategory.Font = new Font(
                "Segoe UI",
                11F,
                FontStyle.Bold
            );
            btnCategory.ForeColor = Color.White;
            btnCategory.Location = new Point(20, 115);
            btnCategory.Name = "btnCategory";
            btnCategory.Size = new Size(180, 55);
            btnCategory.TabIndex = 1;
            btnCategory.Text = "Category";
            btnCategory.UseVisualStyleBackColor = false;
            btnCategory.Click += btnCategory_Click;

            // 
            // btnCustomer
            // 
            btnCustomer.BackColor = Color.FromArgb(160, 120, 85);
            btnCustomer.FlatAppearance.BorderSize = 0;
            btnCustomer.FlatStyle = FlatStyle.Flat;
            btnCustomer.Font = new Font(
                "Segoe UI",
                11F,
                FontStyle.Bold
            );
            btnCustomer.ForeColor = Color.White;
            btnCustomer.Location = new Point(20, 190);
            btnCustomer.Name = "btnCustomer";
            btnCustomer.Size = new Size(180, 55);
            btnCustomer.TabIndex = 2;
            btnCustomer.Text = "Customer";
            btnCustomer.UseVisualStyleBackColor = false;
            btnCustomer.Click += btnCustomer_Click;

            // 
            // btnProduct
            // 
            btnProduct.BackColor = Color.FromArgb(160, 120, 85);
            btnProduct.FlatAppearance.BorderSize = 0;
            btnProduct.FlatStyle = FlatStyle.Flat;
            btnProduct.Font = new Font(
                "Segoe UI",
                11F,
                FontStyle.Bold
            );
            btnProduct.ForeColor = Color.White;
            btnProduct.Location = new Point(20, 265);
            btnProduct.Name = "btnProduct";
            btnProduct.Size = new Size(180, 55);
            btnProduct.TabIndex = 3;
            btnProduct.Text = "Product";
            btnProduct.UseVisualStyleBackColor = false;
            btnProduct.Click += btnProduct_Click;

            // 
            // pnlContent
            // 
            pnlContent.BackColor = Color.White;
            pnlContent.Controls.Add(lblWelcome);
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Location = new Point(220, 70);
            pnlContent.Name = "pnlContent";
            pnlContent.Size = new Size(980, 680);
            pnlContent.TabIndex = 2;
            pnlContent.Paint += pnlContent_Paint;

            // 
            // lblWelcome
            // 
            lblWelcome.AutoSize = true;
            lblWelcome.Font = new Font(
                "Segoe UI",
                18F,
                FontStyle.Bold
            );
            lblWelcome.ForeColor = Color.FromArgb(111, 78, 55);
            lblWelcome.Location = new Point(300, 280);
            lblWelcome.Name = "lblWelcome";
            lblWelcome.Size = new Size(315, 41);
            lblWelcome.TabIndex = 0;
            lblWelcome.Text = "MINI SUPERMARKET";

            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1200, 750);

            Controls.Add(pnlContent);
            Controls.Add(pnlMenu);
            Controls.Add(pnlHeader);

            MinimumSize = new Size(1000, 650);
            Name = "FormMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Mini Supermarket";

            Load += FormMain_Load;

            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();

            pnlMenu.ResumeLayout(false);

            pnlContent.ResumeLayout(false);
            pnlContent.PerformLayout();

            ResumeLayout(false);
        }

        #endregion

        // ============================================================
        // CONTROLS
        // ============================================================

        private Panel pnlHeader;
        private Label lblTitle;

        private Panel pnlMenu;

        private Button btnLogin;
        private Button btnCategory;
        private Button btnCustomer;
        private Button btnProduct;

        private Panel pnlContent;
        private Label lblWelcome;
    }
}
