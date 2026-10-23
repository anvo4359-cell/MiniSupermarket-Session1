// ================================================================
// ********** THÔNG TIN SINH VIÊN **********
// HỌ TÊN : VO THI THUY AN
// LỚP    : CCQ2411C
// MSSV   : 2124110097
// MÔN    : LẬP TRÌNH ỨNG DỤNG
// NGÀY   : 26/09/2026
// ================================================================
// MÔ TẢ:
// File thiết kế giao diện Quản lý Nhóm hàng (Category) WinForms.
// ================================================================

namespace MiniSupermarket.WinForms
{
    partial class FormCategoryManagement
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
            dgvCategories = new DataGridView();
            pnlSearchCard = new Panel();
            lblSearch = new Label();
            txtKeyword = new TextBox();
            btnSearch = new Button();
            btnLoad = new Button();
            pnlCategoryCard = new Panel();
            lblCardHeader = new Label();
            lblId = new Label();
            txtId = new TextBox();
            lblCategoryName = new Label();
            txtCategoryName = new TextBox();
            lblDescription = new Label();
            txtDescription = new TextBox();
            flpButtons = new FlowLayoutPanel();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            btnClear = new Button();
            pnlHeader.SuspendLayout();
            pnlMainContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCategories).BeginInit();
            pnlSearchCard.SuspendLayout();
            pnlCategoryCard.SuspendLayout();
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
            lblTitle.Size = new Size(295, 35);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "QUẢN LÝ NHÓM HÀNG";
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
            pnlMainContainer.Controls.Add(dgvCategories);
            pnlMainContainer.Controls.Add(pnlSearchCard);
            pnlMainContainer.Controls.Add(pnlCategoryCard);
            pnlMainContainer.Dock = DockStyle.Fill;
            pnlMainContainer.Location = new Point(0, 75);
            pnlMainContainer.Name = "pnlMainContainer";
            pnlMainContainer.Padding = new Padding(20);
            pnlMainContainer.Size = new Size(1220, 775);
            pnlMainContainer.TabIndex = 1;
            // 
            // dgvCategories
            // 
            dgvCategories.AllowUserToAddRows = false;
            dgvCategories.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(248, 249, 251);
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(232, 240, 254);
            dataGridViewCellStyle1.SelectionForeColor = Color.FromArgb(11, 30, 54);
            dgvCategories.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvCategories.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCategories.BackgroundColor = Color.White;
            dgvCategories.BorderStyle = BorderStyle.None;
            dgvCategories.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(11, 30, 54);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(212, 175, 55);
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(11, 30, 54);
            dgvCategories.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvCategories.ColumnHeadersHeight = 42;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9.5F);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(40, 40, 40);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(232, 240, 254);
            dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(11, 30, 54);
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dgvCategories.DefaultCellStyle = dataGridViewCellStyle3;
            dgvCategories.Dock = DockStyle.Fill;
            dgvCategories.EnableHeadersVisualStyles = false;
            dgvCategories.GridColor = Color.FromArgb(230, 235, 240);
            dgvCategories.Location = new Point(20, 270);
            dgvCategories.MultiSelect = false;
            dgvCategories.Name = "dgvCategories";
            dgvCategories.ReadOnly = true;
            dgvCategories.RowHeadersVisible = false;
            dgvCategories.RowHeadersWidth = 51;
            dgvCategories.RowTemplate.Height = 35;
            dgvCategories.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCategories.Size = new Size(1180, 485);
            dgvCategories.TabIndex = 0;
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
            txtKeyword.PlaceholderText = " Nhập tên nhóm hàng hoặc từ khóa cần tìm...";
            txtKeyword.Size = new Size(580, 30);
            txtKeyword.TabIndex = 1;
            txtKeyword.TextChanged += txtKeyword_TextChanged; // Tự động tìm kiếm khi gõ
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
            // pnlCategoryCard
            // 
            pnlCategoryCard.BackColor = Color.White;
            pnlCategoryCard.Controls.Add(lblCardHeader);
            pnlCategoryCard.Controls.Add(lblId);
            pnlCategoryCard.Controls.Add(txtId);
            pnlCategoryCard.Controls.Add(lblCategoryName);
            pnlCategoryCard.Controls.Add(txtCategoryName);
            pnlCategoryCard.Controls.Add(lblDescription);
            pnlCategoryCard.Controls.Add(txtDescription);
            pnlCategoryCard.Controls.Add(flpButtons);
            pnlCategoryCard.Dock = DockStyle.Top;
            pnlCategoryCard.Location = new Point(20, 20);
            pnlCategoryCard.Name = "pnlCategoryCard";
            pnlCategoryCard.Size = new Size(1180, 190);
            pnlCategoryCard.TabIndex = 0;
            // 
            // lblCardHeader
            // 
            lblCardHeader.AutoSize = true;
            lblCardHeader.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblCardHeader.ForeColor = Color.FromArgb(11, 30, 54);
            lblCardHeader.Location = new Point(15, 12);
            lblCardHeader.Name = "lblCardHeader";
            lblCardHeader.Size = new Size(206, 25);
            lblCardHeader.TabIndex = 0;
            lblCardHeader.Text = "Thông tin nhóm hàng";
            // 
            // lblId
            // 
            lblId.AutoSize = true;
            lblId.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblId.ForeColor = Color.FromArgb(70, 80, 95);
            lblId.Location = new Point(20, 55);
            lblId.Name = "lblId";
            lblId.Size = new Size(59, 21);
            lblId.TabIndex = 1;
            lblId.Text = "Mã ID:";
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
            // lblCategoryName
            // 
            lblCategoryName.AutoSize = true;
            lblCategoryName.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblCategoryName.ForeColor = Color.FromArgb(70, 80, 95);
            lblCategoryName.Location = new Point(360, 55);
            lblCategoryName.Name = "lblCategoryName";
            lblCategoryName.Size = new Size(133, 21);
            lblCategoryName.TabIndex = 3;
            lblCategoryName.Text = "Tên nhóm hàng:";
            // 
            // txtCategoryName
            // 
            txtCategoryName.BorderStyle = BorderStyle.FixedSingle;
            txtCategoryName.Font = new Font("Segoe UI", 10F);
            txtCategoryName.Location = new Point(500, 52);
            txtCategoryName.Name = "txtCategoryName";
            txtCategoryName.Size = new Size(660, 30);
            txtCategoryName.TabIndex = 4;
            txtCategoryName.TextChanged += txtCategoryName_TextChanged;
            // 
            // lblDescription
            // 
            lblDescription.AutoSize = true;
            lblDescription.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblDescription.ForeColor = Color.FromArgb(70, 80, 95);
            lblDescription.Location = new Point(20, 100);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(58, 21);
            lblDescription.TabIndex = 5;
            lblDescription.Text = "Mô tả:";
            // 
            // txtDescription
            // 
            txtDescription.BorderStyle = BorderStyle.FixedSingle;
            txtDescription.Font = new Font("Segoe UI", 10F);
            txtDescription.Location = new Point(140, 97);
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(1020, 30);
            txtDescription.TabIndex = 6;
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
            flpButtons.TabIndex = 7;
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
            // FormCategoryManagement
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(244, 246, 249);
            ClientSize = new Size(1220, 850);
            Controls.Add(pnlMainContainer);
            Controls.Add(pnlHeader);
            MinimumSize = new Size(1050, 750);
            Name = "FormCategoryManagement";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản lý nhóm hàng - Happy Mart";
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlMainContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvCategories).EndInit();
            pnlSearchCard.ResumeLayout(false);
            pnlSearchCard.PerformLayout();
            pnlCategoryCard.ResumeLayout(false);
            pnlCategoryCard.PerformLayout();
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
        private Panel pnlCategoryCard;
        private Label lblCardHeader;
        private Label lblId;
        private TextBox txtId;
        private Label lblCategoryName;
        private TextBox txtCategoryName;
        private Label lblDescription;
        private TextBox txtDescription;
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
        private DataGridView dgvCategories;
    }
}