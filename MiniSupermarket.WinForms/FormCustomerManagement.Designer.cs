// ================================================================
// ********** THÔNG TIN SINH VIÊN **********
// HỌ TÊN : VO THI THUY AN
// LỚP    : CCQ2411C
// MSSV   : 2124110097
// MÔN    : LẬP TRÌNH ỨNG DỤNG
// NGÀY   : 26/09/2026
// ================================================================
// MÔ TẢ:
// File thiết kế giao diện Quản lý Khách hàng (Customer) WinForms.
// ================================================================

namespace MiniSupermarket.WinForms
{
    partial class FormCustomerManagement
    {
        private System.ComponentModel.IContainer components = null;

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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            pnlHeader = new Panel();
            lblTitle = new Label();
            lblStudent = new Label();
            pnlMainContainer = new Panel();
            dgvCustomers = new DataGridView();
            pnlSearchCard = new Panel();
            lblSearch = new Label();
            txtKeyword = new TextBox();
            btnSearch = new Button();
            btnLoad = new Button();
            pnlCustomerCard = new Panel();
            lblCardHeader = new Label();
            lblId = new Label();
            txtId = new TextBox();
            lblCustomerName = new Label();
            txtCustomerName = new TextBox();
            lblPhone = new Label();
            txtPhone = new TextBox();
            lblAddress = new Label();
            txtAddress = new TextBox();
            lblEmail = new Label();
            txtEmail = new TextBox();
            flpButtons = new FlowLayoutPanel();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            btnClear = new Button();
            pnlHeader.SuspendLayout();
            pnlMainContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCustomers).BeginInit();
            pnlSearchCard.SuspendLayout();
            pnlCustomerCard.SuspendLayout();
            flpButtons.SuspendLayout();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.FromArgb(11, 30, 54);
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Controls.Add(lblStudent);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(1220, 75);
            pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(212, 175, 55);
            lblTitle.Location = new Point(20, 12);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(322, 35);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "QUẢN LÝ KHÁCH HÀNG";
            // 
            // lblStudent
            // 
            lblStudent.AutoSize = true;
            lblStudent.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lblStudent.ForeColor = Color.FromArgb(180, 200, 220);
            lblStudent.Location = new Point(23, 47);
            lblStudent.Name = "lblStudent";
            lblStudent.Size = new Size(344, 20);
            lblStudent.TabIndex = 1;
            lblStudent.Text = "VO THI THUY AN - CCQ2411C - MSSV: 2124110097";
            // 
            // pnlMainContainer
            // 
            pnlMainContainer.AutoScroll = true;
            pnlMainContainer.Controls.Add(dgvCustomers);
            pnlMainContainer.Controls.Add(pnlSearchCard);
            pnlMainContainer.Controls.Add(pnlCustomerCard);
            pnlMainContainer.Dock = DockStyle.Fill;
            pnlMainContainer.Location = new Point(0, 75);
            pnlMainContainer.Name = "pnlMainContainer";
            pnlMainContainer.Padding = new Padding(20);
            pnlMainContainer.Size = new Size(1220, 775);
            pnlMainContainer.TabIndex = 1;
            // 
            // dgvCustomers
            // 
            dgvCustomers.AllowUserToAddRows = false;
            dgvCustomers.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(248, 249, 251);
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(232, 240, 254);
            dataGridViewCellStyle1.SelectionForeColor = Color.FromArgb(11, 30, 54);
            dgvCustomers.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvCustomers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCustomers.BackgroundColor = Color.White;
            dgvCustomers.BorderStyle = BorderStyle.None;
            dgvCustomers.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(11, 30, 54);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(212, 175, 55);
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(11, 30, 54);
            dgvCustomers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvCustomers.ColumnHeadersHeight = 42;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9.5F);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(40, 40, 40);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(232, 240, 254);
            dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(11, 30, 54);
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dgvCustomers.DefaultCellStyle = dataGridViewCellStyle3;
            dgvCustomers.Dock = DockStyle.Fill;
            dgvCustomers.EnableHeadersVisualStyles = false;
            dgvCustomers.GridColor = Color.FromArgb(230, 235, 240);
            dgvCustomers.Location = new Point(20, 270);
            dgvCustomers.MultiSelect = false;
            dgvCustomers.Name = "dgvCustomers";
            dgvCustomers.ReadOnly = true;
            dgvCustomers.RowHeadersVisible = false;
            dgvCustomers.RowHeadersWidth = 51;
            dgvCustomers.RowTemplate.Height = 35;
            dgvCustomers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCustomers.Size = new Size(1180, 485);
            dgvCustomers.TabIndex = 0;
            dgvCustomers.CellContentClick += dgvCustomers_CellContentClick;
            // 
            // pnlSearchCard
            // 
            pnlSearchCard.BackColor = Color.White;
            pnlSearchCard.Controls.Add(lblSearch);
            pnlSearchCard.Controls.Add(txtKeyword);
            pnlSearchCard.Controls.Add(btnSearch);
            pnlSearchCard.Controls.Add(btnLoad);
            pnlSearchCard.Dock = DockStyle.Top;
            pnlSearchCard.Location = new Point(20, 210);
            pnlSearchCard.Name = "pnlSearchCard";
            pnlSearchCard.Size = new Size(1180, 60);
            pnlSearchCard.TabIndex = 1;
            // 
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblSearch.ForeColor = Color.FromArgb(11, 30, 54);
            lblSearch.Location = new Point(20, 18);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(85, 21);
            lblSearch.TabIndex = 0;
            lblSearch.Text = "Tìm kiếm:";
            // 
            // txtKeyword
            // 
            txtKeyword.BorderStyle = BorderStyle.FixedSingle;
            txtKeyword.Font = new Font("Segoe UI", 10F);
            txtKeyword.Location = new Point(140, 15);
            txtKeyword.Name = "txtKeyword";
            txtKeyword.PlaceholderText = " Nhập tên khách hàng, SĐT hoặc Email cần tìm...";
            txtKeyword.Size = new Size(580, 30);
            txtKeyword.TabIndex = 1;
            txtKeyword.TextChanged += txtKeyword_TextChanged;
            // 
            // btnSearch
            // 
            FormatButton(btnSearch, " Tìm kiếm", Color.FromArgb(11, 30, 54), Color.FromArgb(212, 175, 55));
            btnSearch.Location = new Point(740, 12);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(120, 36);
            btnSearch.TabIndex = 2;
            btnSearch.Click += btnSearch_Click;
            // 
            // btnLoad
            // 
            FormatButton(btnLoad, " Tải lại", Color.FromArgb(108, 117, 125), Color.White);
            btnLoad.Location = new Point(870, 12);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(130, 36);
            btnLoad.TabIndex = 3;
            btnLoad.Click += btnLoad_Click;
            // 
            // pnlCustomerCard
            // 
            pnlCustomerCard.BackColor = Color.White;
            pnlCustomerCard.Controls.Add(lblCardHeader);
            pnlCustomerCard.Controls.Add(lblId);
            pnlCustomerCard.Controls.Add(txtId);
            pnlCustomerCard.Controls.Add(lblCustomerName);
            pnlCustomerCard.Controls.Add(txtCustomerName);
            pnlCustomerCard.Controls.Add(lblPhone);
            pnlCustomerCard.Controls.Add(txtPhone);
            pnlCustomerCard.Controls.Add(lblAddress);
            pnlCustomerCard.Controls.Add(txtAddress);
            pnlCustomerCard.Controls.Add(lblEmail);
            pnlCustomerCard.Controls.Add(txtEmail);
            pnlCustomerCard.Controls.Add(flpButtons);
            pnlCustomerCard.Dock = DockStyle.Top;
            pnlCustomerCard.Location = new Point(20, 20);
            pnlCustomerCard.Name = "pnlCustomerCard";
            pnlCustomerCard.Size = new Size(1180, 190);
            pnlCustomerCard.TabIndex = 0;
            // 
            // lblCardHeader
            // 
            lblCardHeader.AutoSize = true;
            lblCardHeader.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblCardHeader.ForeColor = Color.FromArgb(11, 30, 54);
            lblCardHeader.Location = new Point(15, 12);
            lblCardHeader.Name = "lblCardHeader";
            lblCardHeader.Size = new Size(270, 25);
            lblCardHeader.TabIndex = 0;
            lblCardHeader.Text = "Thông tin chi tiết khách hàng";
            // 
            // lblId
            // 
            lblId.AutoSize = true;
            lblId.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblId.ForeColor = Color.FromArgb(70, 80, 95);
            lblId.Location = new Point(20, 55);
            lblId.Name = "lblId";
            lblId.Size = new Size(64, 21);
            lblId.TabIndex = 1;
            lblId.Text = "Mã KH:";
            // 
            // txtId
            // 
            txtId.BackColor = Color.FromArgb(242, 244, 247);
            txtId.BorderStyle = BorderStyle.FixedSingle;
            txtId.Font = new Font("Segoe UI", 10F);
            txtId.Location = new Point(140, 52);
            txtId.Name = "txtId";
            txtId.ReadOnly = true;
            txtId.Size = new Size(180, 30);
            txtId.TabIndex = 2;
            // 
            // lblCustomerName
            // 
            lblCustomerName.AutoSize = true;
            lblCustomerName.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblCustomerName.ForeColor = Color.FromArgb(70, 80, 95);
            lblCustomerName.Location = new Point(340, 55);
            lblCustomerName.Name = "lblCustomerName";
            lblCustomerName.Size = new Size(134, 21);
            lblCustomerName.TabIndex = 3;
            lblCustomerName.Text = "Tên khách hàng:";
            // 
            // txtCustomerName
            // 
            txtCustomerName.BorderStyle = BorderStyle.FixedSingle;
            txtCustomerName.Font = new Font("Segoe UI", 10F);
            txtCustomerName.Location = new Point(480, 52);
            txtCustomerName.Name = "txtCustomerName";
            txtCustomerName.Size = new Size(280, 30);
            txtCustomerName.TabIndex = 4;
            // 
            // lblPhone
            // 
            lblPhone.AutoSize = true;
            lblPhone.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblPhone.ForeColor = Color.FromArgb(70, 80, 95);
            lblPhone.Location = new Point(780, 55);
            lblPhone.Name = "lblPhone";
            lblPhone.Size = new Size(115, 21);
            lblPhone.TabIndex = 5;
            lblPhone.Text = "Số điện thoại:";
            // 
            // txtPhone
            // 
            txtPhone.BorderStyle = BorderStyle.FixedSingle;
            txtPhone.Font = new Font("Segoe UI", 10F);
            txtPhone.Location = new Point(900, 52);
            txtPhone.Name = "txtPhone";
            txtPhone.Size = new Size(260, 30);
            txtPhone.TabIndex = 6;
            // 
            // lblAddress
            // 
            lblAddress.AutoSize = true;
            lblAddress.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblAddress.ForeColor = Color.FromArgb(70, 80, 95);
            lblAddress.Location = new Point(20, 100);
            lblAddress.Name = "lblAddress";
            lblAddress.Size = new Size(67, 21);
            lblAddress.TabIndex = 7;
            lblAddress.Text = "Địa chỉ:";
            // 
            // txtAddress
            // 
            txtAddress.BorderStyle = BorderStyle.FixedSingle;
            txtAddress.Font = new Font("Segoe UI", 10F);
            txtAddress.Location = new Point(140, 97);
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(380, 30);
            txtAddress.TabIndex = 8;
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblEmail.ForeColor = Color.FromArgb(70, 80, 95);
            lblEmail.Location = new Point(540, 100);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(57, 21);
            lblEmail.TabIndex = 9;
            lblEmail.Text = "Email:";
            // 
            // txtEmail
            // 
            txtEmail.BorderStyle = BorderStyle.FixedSingle;
            txtEmail.Font = new Font("Segoe UI", 10F);
            txtEmail.Location = new Point(610, 97);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(550, 30);
            txtEmail.TabIndex = 10;
            // 
            // flpButtons
            // 
            flpButtons.Controls.Add(btnAdd);
            flpButtons.Controls.Add(btnUpdate);
            flpButtons.Controls.Add(btnDelete);
            flpButtons.Controls.Add(btnClear);
            flpButtons.Location = new Point(140, 138);
            flpButtons.Name = "flpButtons";
            flpButtons.Size = new Size(600, 45);
            flpButtons.TabIndex = 11;
            // 
            // btnAdd
            // 
            FormatButton(btnAdd, " Thêm mới", Color.FromArgb(11, 30, 54), Color.FromArgb(212, 175, 55));
            btnAdd.Click += btnAdd_Click;
            // 
            // btnUpdate
            // 
            FormatButton(btnUpdate, " Cập nhật", Color.FromArgb(51, 122, 183), Color.White);
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnDelete
            // 
            FormatButton(btnDelete, " Xóa", Color.FromArgb(217, 83, 79), Color.White);
            btnDelete.Click += btnDelete_Click;
            // 
            // btnClear
            // 
            FormatButton(btnClear, " Làm mới", Color.FromArgb(108, 117, 125), Color.White);
            btnClear.Click += btnClear_Click;
            // 
            // FormCustomerManagement
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(244, 246, 249);
            ClientSize = new Size(1220, 850);
            Controls.Add(pnlMainContainer);
            Controls.Add(pnlHeader);
            MinimumSize = new Size(1050, 750);
            Name = "FormCustomerManagement";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản lý khách hàng - Happy Mart";
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlMainContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvCustomers).EndInit();
            pnlSearchCard.ResumeLayout(false);
            pnlSearchCard.PerformLayout();
            pnlCustomerCard.ResumeLayout(false);
            pnlCustomerCard.PerformLayout();
            flpButtons.ResumeLayout(false);
            ResumeLayout(false);
        }

        private void FormatButton(Button btn, string text, Color backColor, Color foreColor)
        {
            btn.Text = text;
            btn.BackColor = backColor;
            btn.ForeColor = foreColor;
            btn.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Margin = new Padding(0, 0, 12, 0);
            btn.Size = new Size(120, 38);
            btn.Cursor = Cursors.Hand;
        }

        #endregion

        private Panel pnlHeader;
        private Label lblTitle;
        private Label lblStudent;
        private Panel pnlMainContainer;
        private Panel pnlCustomerCard;
        private Label lblCardHeader;
        private Label lblId;
        private TextBox txtId;
        private Label lblCustomerName;
        private TextBox txtCustomerName;
        private Label lblPhone;
        private TextBox txtPhone;
        private Label lblAddress;
        private TextBox txtAddress;
        private Label lblEmail;
        private TextBox txtEmail;
        private FlowLayoutPanel flpButtons;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnClear;
        private Panel pnlSearchCard;
        private Label lblSearch;
        private TextBox txtKeyword;
        private Button btnSearch;
        private Button btnLoad;
        private DataGridView dgvCustomers;
    }
}