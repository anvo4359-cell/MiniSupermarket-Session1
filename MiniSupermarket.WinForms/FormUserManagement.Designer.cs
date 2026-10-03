namespace MiniSupermarket.WinForms
{
    partial class FormUserManagement
    {
        private System.ComponentModel.IContainer components = null;

        private Panel pnlHeader;
        private Label lblTitle;
        private Label lblSearch;
        private TextBox txtSearch;
        private Button btnSearch;

        private Panel pnlInput;
        private Label lblUserId;
        private Label lblUsername;
        private Label lblPassword;
        private Label lblRole;
        private Label lblFullName;

        private TextBox txtUserId;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private ComboBox cboRole;
        private TextBox txtFullName;

        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnClear;

        private DataGridView dgvUsers;

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
            lblSearch = new Label();
            txtSearch = new TextBox();
            btnSearch = new Button();

            pnlInput = new Panel();

            lblUserId = new Label();
            lblUsername = new Label();
            lblPassword = new Label();
            lblRole = new Label();
            lblFullName = new Label();

            txtUserId = new TextBox();
            txtUsername = new TextBox();
            txtPassword = new TextBox();
            cboRole = new ComboBox();
            txtFullName = new TextBox();

            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            btnClear = new Button();

            dgvUsers = new DataGridView();

            pnlHeader.SuspendLayout();
            pnlInput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUsers).BeginInit();

            SuspendLayout();

            // 
            // FormUserManagement
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 244, 238);
            ClientSize = new Size(1100, 700);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FormUserManagement";
            Text = "Quản lý tài khoản";
            Load += FormUserManagement_Load;

            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.FromArgb(111, 78, 55);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(1100, 95);

            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font(
                "Segoe UI",
                22F,
                FontStyle.Bold
            );
            lblTitle.ForeColor = Color.FromArgb(230, 194, 128);
            lblTitle.Location = new Point(30, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Text = "QUẢN LÝ TÀI KHOẢN";

            // 
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.Font = new Font(
                "Segoe UI",
                10F,
                FontStyle.Regular
            );
            lblSearch.ForeColor = Color.White;
            lblSearch.Location = new Point(540, 31);
            lblSearch.Name = "lblSearch";
            lblSearch.Text = "Tìm kiếm:";

            // 
            // txtSearch
            // 
            txtSearch.Font = new Font(
                "Segoe UI",
                10F,
                FontStyle.Regular
            );
            txtSearch.Location = new Point(620, 27);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(300, 30);

            // 
            // btnSearch
            // 
            btnSearch.BackColor = Color.FromArgb(230, 194, 128);
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.Font = new Font(
                "Segoe UI",
                10F,
                FontStyle.Bold
            );
            btnSearch.ForeColor = Color.FromArgb(70, 45, 30);
            btnSearch.Location = new Point(930, 25);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(125, 35);
            btnSearch.Text = "Tìm kiếm";
            btnSearch.UseVisualStyleBackColor = false;
            btnSearch.Click += btnSearch_Click;

            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Controls.Add(lblSearch);
            pnlHeader.Controls.Add(txtSearch);
            pnlHeader.Controls.Add(btnSearch);

            // 
            // pnlInput
            // 
            pnlInput.BackColor = Color.White;
            pnlInput.BorderStyle = BorderStyle.FixedSingle;
            pnlInput.Location = new Point(30, 115);
            pnlInput.Name = "pnlInput";
            pnlInput.Size = new Size(1040, 185);

            // 
            // lblUserId
            // 
            lblUserId.AutoSize = true;
            lblUserId.Font = new Font(
                "Segoe UI",
                10F,
                FontStyle.Bold
            );
            lblUserId.ForeColor = Color.FromArgb(111, 78, 55);
            lblUserId.Location = new Point(20, 20);
            lblUserId.Text = "Mã tài khoản";

            // 
            // txtUserId
            // 
            txtUserId.Location = new Point(125, 17);
            txtUserId.Name = "txtUserId";
            txtUserId.ReadOnly = true;
            txtUserId.Size = new Size(130, 27);

            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font(
                "Segoe UI",
                10F,
                FontStyle.Bold
            );
            lblUsername.ForeColor = Color.FromArgb(111, 78, 55);
            lblUsername.Location = new Point(290, 20);
            lblUsername.Text = "Tài khoản";

            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(380, 17);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(220, 27);

            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Font = new Font(
                "Segoe UI",
                10F,
                FontStyle.Bold
            );
            lblPassword.ForeColor = Color.FromArgb(111, 78, 55);
            lblPassword.Location = new Point(625, 20);
            lblPassword.Text = "Mật khẩu";

            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(710, 17);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(290, 27);
            txtPassword.UseSystemPasswordChar = true;

            // 
            // lblRole
            // 
            lblRole.AutoSize = true;
            lblRole.Font = new Font(
                "Segoe UI",
                10F,
                FontStyle.Bold
            );
            lblRole.ForeColor = Color.FromArgb(111, 78, 55);
            lblRole.Location = new Point(20, 70);
            lblRole.Text = "Quyền";

            // 
            // cboRole
            // 
            cboRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cboRole.FormattingEnabled = true;
            cboRole.Items.AddRange(
                new object[]
                {
                    "Admin",
                    "Cashier",
                    "Warehouse"
                }
            );
            cboRole.Location = new Point(125, 67);
            cboRole.Name = "cboRole";
            cboRole.Size = new Size(230, 28);

            // 
            // lblFullName
            // 
            lblFullName.AutoSize = true;
            lblFullName.Font = new Font(
                "Segoe UI",
                10F,
                FontStyle.Bold
            );
            lblFullName.ForeColor = Color.FromArgb(111, 78, 55);
            lblFullName.Location = new Point(380, 70);
            lblFullName.Text = "Họ và tên";

            // 
            // txtFullName
            // 
            txtFullName.Location = new Point(465, 67);
            txtFullName.Name = "txtFullName";
            txtFullName.Size = new Size(535, 27);

            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.FromArgb(111, 78, 55);
            btnAdd.FlatAppearance.BorderSize = 0;
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.Font = new Font(
                "Segoe UI",
                10F,
                FontStyle.Bold
            );
            btnAdd.ForeColor = Color.White;
            btnAdd.Location = new Point(125, 120);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(130, 38);
            btnAdd.Text = "Thêm";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += btnAdd_Click;

            // 
            // btnUpdate
            // 
            btnUpdate.BackColor = Color.FromArgb(230, 194, 128);
            btnUpdate.FlatAppearance.BorderSize = 0;
            btnUpdate.FlatStyle = FlatStyle.Flat;
            btnUpdate.Font = new Font(
                "Segoe UI",
                10F,
                FontStyle.Bold
            );
            btnUpdate.ForeColor = Color.FromArgb(70, 45, 30);
            btnUpdate.Location = new Point(275, 120);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(130, 38);
            btnUpdate.Text = "Cập nhật";
            btnUpdate.UseVisualStyleBackColor = false;
            btnUpdate.Click += btnUpdate_Click;

            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.FromArgb(180, 90, 70);
            btnDelete.FlatAppearance.BorderSize = 0;
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.Font = new Font(
                "Segoe UI",
                10F,
                FontStyle.Bold
            );
            btnDelete.ForeColor = Color.White;
            btnDelete.Location = new Point(425, 120);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(130, 38);
            btnDelete.Text = "Xóa";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;

            // 
            // btnClear
            // 
            btnClear.BackColor = Color.FromArgb(235, 228, 218);
            btnClear.FlatAppearance.BorderSize = 0;
            btnClear.FlatStyle = FlatStyle.Flat;
            btnClear.Font = new Font(
                "Segoe UI",
                10F,
                FontStyle.Bold
            );
            btnClear.ForeColor = Color.FromArgb(111, 78, 55);
            btnClear.Location = new Point(575, 120);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(130, 38);
            btnClear.Text = "Làm mới";
            btnClear.UseVisualStyleBackColor = false;
            btnClear.Click += btnClear_Click;

            pnlInput.Controls.Add(lblUserId);
            pnlInput.Controls.Add(txtUserId);
            pnlInput.Controls.Add(lblUsername);
            pnlInput.Controls.Add(txtUsername);
            pnlInput.Controls.Add(lblPassword);
            pnlInput.Controls.Add(txtPassword);
            pnlInput.Controls.Add(lblRole);
            pnlInput.Controls.Add(cboRole);
            pnlInput.Controls.Add(lblFullName);
            pnlInput.Controls.Add(txtFullName);
            pnlInput.Controls.Add(btnAdd);
            pnlInput.Controls.Add(btnUpdate);
            pnlInput.Controls.Add(btnDelete);
            pnlInput.Controls.Add(btnClear);

            // 
            // dgvUsers
            // 
            dgvUsers.AllowUserToAddRows = false;
            dgvUsers.AllowUserToDeleteRows = false;
            dgvUsers.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
            dgvUsers.BackgroundColor = Color.White;
            dgvUsers.BorderStyle = BorderStyle.FixedSingle;
            dgvUsers.ColumnHeadersHeight = 40;
            dgvUsers.Location = new Point(30, 320);
            dgvUsers.MultiSelect = false;
            dgvUsers.Name = "dgvUsers";
            dgvUsers.ReadOnly = true;
            dgvUsers.RowHeadersVisible = false;
            dgvUsers.RowTemplate.Height = 35;
            dgvUsers.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dgvUsers.Size = new Size(1040, 345);
            dgvUsers.CellClick += dgvUsers_CellClick;

            Controls.Add(dgvUsers);
            Controls.Add(pnlInput);
            Controls.Add(pnlHeader);

            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();

            pnlInput.ResumeLayout(false);
            pnlInput.PerformLayout();

            ((System.ComponentModel.ISupportInitialize)dgvUsers).EndInit();

            ResumeLayout(false);
        }

        #endregion
    }
}