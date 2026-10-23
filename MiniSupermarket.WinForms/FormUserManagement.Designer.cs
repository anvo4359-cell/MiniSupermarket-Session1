// ================================================================
// ********** THÔNG TIN SINH VIÊN **********
// HỌ TÊN : VO THI THUY AN
// LỚP    : CCQ2411C
// MSSV   : 2124110097
// MÔN    : LẬP TRÌNH ỨNG DỤNG
// NGÀY   : 26/09/2026
// ================================================================

namespace MiniSupermarket.WinForms
{
    partial class FormUserManagement
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;

        private System.Windows.Forms.Panel pnlInput;
        private System.Windows.Forms.Label lblUserId;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblRole;
        private System.Windows.Forms.Label lblFullName;

        private System.Windows.Forms.TextBox txtUserId;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.ComboBox cboRole;
        private System.Windows.Forms.TextBox txtFullName;

        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClear;

        private System.Windows.Forms.DataGridView dgvUsers;

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
            System.Windows.Forms.DataGridViewCellStyle dgvHeaderStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dgvCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dgvAltCellStyle = new System.Windows.Forms.DataGridViewCellStyle();

            pnlHeader = new Panel();
            lblTitle = new Label();
            lblSearch = new Label();
            txtSearch = new TextBox();
            btnSearch = new Button();
            pnlInput = new Panel();
            lblUserId = new Label();
            txtUserId = new TextBox();
            lblUsername = new Label();
            txtUsername = new TextBox();
            lblPassword = new Label();
            txtPassword = new TextBox();
            lblRole = new Label();
            cboRole = new ComboBox();
            lblFullName = new Label();
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
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.FromArgb(11, 30, 54);
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Controls.Add(lblSearch);
            pnlHeader.Controls.Add(txtSearch);
            pnlHeader.Controls.Add(btnSearch);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(1100, 95);
            pnlHeader.TabIndex = 2;

            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(212, 175, 55);
            lblTitle.Location = new Point(30, 26);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(310, 41);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "QUẢN LÝ TÀI KHOẢN";

            // 
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblSearch.ForeColor = Color.White;
            lblSearch.Location = new Point(510, 35);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(88, 23);
            lblSearch.TabIndex = 1;
            lblSearch.Text = "Tìm kiếm:";

            // 
            // txtSearch
            // 
            txtSearch.BorderStyle = BorderStyle.FixedSingle;
            txtSearch.Font = new Font("Segoe UI", 10F);
            txtSearch.Location = new Point(605, 32);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = " Nhập tên đăng nhập hoặc họ tên...";
            txtSearch.Size = new Size(310, 30);
            txtSearch.TabIndex = 2;
            txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged); // Tự động tìm kiếm khi gõ

            // 
            // btnSearch
            // 
            btnSearch.BackColor = Color.FromArgb(212, 175, 55);
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnSearch.ForeColor = Color.FromArgb(11, 30, 54);
            btnSearch.Location = new Point(930, 30);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(130, 34);
            btnSearch.TabIndex = 3;
            btnSearch.Text = "Tìm kiếm";
            btnSearch.UseVisualStyleBackColor = false;
            btnSearch.Click += btnSearch_Click;
            btnSearch.Cursor = Cursors.Hand;

            // 
            // pnlInput
            // 
            pnlInput.BackColor = Color.White;
            pnlInput.BorderStyle = BorderStyle.FixedSingle;
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
            pnlInput.Location = new Point(30, 115);
            pnlInput.Name = "pnlInput";
            pnlInput.Size = new Size(1040, 185);
            pnlInput.TabIndex = 1;

            // 
            // lblUserId
            // 
            lblUserId.AutoSize = true;
            lblUserId.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblUserId.ForeColor = Color.FromArgb(70, 80, 95);
            lblUserId.Location = new Point(20, 22);
            lblUserId.Name = "lblUserId";
            lblUserId.Size = new Size(62, 21);
            lblUserId.TabIndex = 0;
            lblUserId.Text = "Mã ID:";

            // 
            // txtUserId
            // 
            txtUserId.BackColor = Color.FromArgb(242, 244, 247);
            txtUserId.BorderStyle = BorderStyle.FixedSingle;
            txtUserId.Font = new Font("Segoe UI", 10F);
            txtUserId.Location = new Point(130, 19);
            txtUserId.Name = "txtUserId";
            txtUserId.ReadOnly = true;
            txtUserId.Size = new Size(130, 30);

            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblUsername.ForeColor = Color.FromArgb(70, 80, 95);
            lblUsername.Location = new Point(290, 22);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(88, 21);
            lblUsername.TabIndex = 2;
            lblUsername.Text = "Tài khoản:";

            // 
            // txtUsername
            // 
            txtUsername.BorderStyle = BorderStyle.FixedSingle;
            txtUsername.Font = new Font("Segoe UI", 10F);
            txtUsername.Location = new Point(385, 19);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(210, 30);

            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblPassword.ForeColor = Color.FromArgb(70, 80, 95);
            lblPassword.Location = new Point(620, 22);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(86, 21);
            lblPassword.TabIndex = 4;
            lblPassword.Text = "Mật khẩu:";

            // 
            // txtPassword
            // 
            txtPassword.BorderStyle = BorderStyle.FixedSingle;
            txtPassword.Font = new Font("Segoe UI", 10F);
            txtPassword.Location = new Point(715, 19);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(295, 30);
            txtPassword.UseSystemPasswordChar = true;

            // 
            // lblRole
            // 
            lblRole.AutoSize = true;
            lblRole.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblRole.ForeColor = Color.FromArgb(70, 80, 95);
            lblRole.Location = new Point(20, 72);
            lblRole.Name = "lblRole";
            lblRole.Size = new Size(63, 21);
            lblRole.TabIndex = 6;
            lblRole.Text = "Quyền:";

            // 
            // cboRole
            // 
            cboRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cboRole.Font = new Font("Segoe UI", 10F);
            cboRole.FormattingEnabled = true;
            cboRole.Items.AddRange(new object[] { "ADMIN", "CASHIER", "STAFF" });
            cboRole.Location = new Point(130, 69);
            cboRole.Name = "cboRole";
            cboRole.Size = new Size(130, 31);

            // 
            // lblFullName
            // 
            lblFullName.AutoSize = true;
            lblFullName.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblFullName.ForeColor = Color.FromArgb(70, 80, 95);
            lblFullName.Location = new Point(290, 72);
            lblFullName.Name = "lblFullName";
            lblFullName.Size = new Size(92, 21);
            lblFullName.TabIndex = 8;
            lblFullName.Text = "Họ và tên:";

            // 
            // txtFullName
            // 
            txtFullName.BorderStyle = BorderStyle.FixedSingle;
            txtFullName.Font = new Font("Segoe UI", 10F);
            txtFullName.Location = new Point(385, 69);
            txtFullName.Name = "txtFullName";
            txtFullName.Size = new Size(625, 30);

            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.FromArgb(11, 30, 54);
            btnAdd.FlatAppearance.BorderSize = 0;
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnAdd.ForeColor = Color.FromArgb(212, 175, 55);
            btnAdd.Location = new Point(130, 122);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(125, 38);
            btnAdd.TabIndex = 10;
            btnAdd.Text = "Thêm mới";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += btnAdd_Click;
            btnAdd.Cursor = Cursors.Hand;

            // 
            // btnUpdate
            // 
            btnUpdate.BackColor = Color.FromArgb(51, 122, 183);
            btnUpdate.FlatAppearance.BorderSize = 0;
            btnUpdate.FlatStyle = FlatStyle.Flat;
            btnUpdate.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnUpdate.ForeColor = Color.White;
            btnUpdate.Location = new Point(275, 122);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(125, 38);
            btnUpdate.TabIndex = 11;
            btnUpdate.Text = "Cập nhật";
            btnUpdate.UseVisualStyleBackColor = false;
            btnUpdate.Click += btnUpdate_Click;
            btnUpdate.Cursor = Cursors.Hand;

            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.FromArgb(217, 83, 79);
            btnDelete.FlatAppearance.BorderSize = 0;
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnDelete.ForeColor = Color.White;
            btnDelete.Location = new Point(420, 122);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(125, 38);
            btnDelete.TabIndex = 12;
            btnDelete.Text = "Xóa";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;
            btnDelete.Cursor = Cursors.Hand;

            // 
            // btnClear
            // 
            btnClear.BackColor = Color.FromArgb(108, 117, 125);
            btnClear.FlatAppearance.BorderSize = 0;
            btnClear.FlatStyle = FlatStyle.Flat;
            btnClear.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnClear.ForeColor = Color.White;
            btnClear.Location = new Point(565, 122);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(125, 38);
            btnClear.TabIndex = 13;
            btnClear.Text = "Làm mới";
            btnClear.UseVisualStyleBackColor = false;
            btnClear.Click += btnClear_Click;
            btnClear.Cursor = Cursors.Hand;

            // 
            // dgvUsers
            // 
            dgvUsers.AllowUserToAddRows = false;
            dgvUsers.AllowUserToDeleteRows = false;
            dgvUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvUsers.BackgroundColor = Color.White;
            dgvUsers.BorderStyle = BorderStyle.FixedSingle;
            dgvUsers.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

            dgvHeaderStyle.BackColor = Color.FromArgb(11, 30, 54);
            dgvHeaderStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvHeaderStyle.ForeColor = Color.FromArgb(212, 175, 55);
            dgvHeaderStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvHeaderStyle.SelectionBackColor = Color.FromArgb(11, 30, 54);
            dgvUsers.ColumnHeadersDefaultCellStyle = dgvHeaderStyle;
            dgvUsers.ColumnHeadersHeight = 42;
            dgvUsers.EnableHeadersVisualStyles = false;

            dgvCellStyle.BackColor = Color.White;
            dgvCellStyle.Font = new Font("Segoe UI", 9.5F);
            dgvCellStyle.ForeColor = Color.FromArgb(40, 40, 40);
            dgvCellStyle.SelectionBackColor = Color.FromArgb(232, 240, 254);
            dgvCellStyle.SelectionForeColor = Color.FromArgb(11, 30, 54);
            dgvUsers.DefaultCellStyle = dgvCellStyle;

            dgvAltCellStyle.BackColor = Color.FromArgb(248, 249, 251);
            dgvAltCellStyle.SelectionBackColor = Color.FromArgb(232, 240, 254);
            dgvAltCellStyle.SelectionForeColor = Color.FromArgb(11, 30, 54);
            dgvUsers.AlternatingRowsDefaultCellStyle = dgvAltCellStyle;

            dgvUsers.GridColor = Color.FromArgb(230, 235, 240);
            dgvUsers.Location = new Point(30, 320);
            dgvUsers.MultiSelect = false;
            dgvUsers.Name = "dgvUsers";
            dgvUsers.ReadOnly = true;
            dgvUsers.RowHeadersVisible = false;
            dgvUsers.RowTemplate.Height = 35;
            dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsers.Size = new Size(1040, 345);
            dgvUsers.TabIndex = 0;
            dgvUsers.CellClick += dgvUsers_CellClick;
            dgvUsers.CellContentClick += dgvUsers_CellContentClick;

            // 
            // FormUserManagement
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(244, 246, 249);
            ClientSize = new Size(1100, 700);
            Controls.Add(dgvUsers);
            Controls.Add(pnlInput);
            Controls.Add(pnlHeader);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FormUserManagement";
            Text = "Quản lý tài khoản";
            Load += FormUserManagement_Load;
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