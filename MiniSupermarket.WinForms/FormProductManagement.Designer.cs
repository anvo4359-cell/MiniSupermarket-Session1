
using System.Drawing;
using System.Windows.Forms;

// ================================================================
// THÔNG TIN SINH VIÊN
// HỌ TÊN : VO THI THUY AN
// LỚP    : CCQ2411C
// MSSV   : 2124110097
// MÔN    : LẬP TRÌNH ỨNG DỤNG
// NGÀY   : 26/09/2026
// ================================================================

namespace MiniSupermarket.WinForms
{
    partial class FormProductManagement
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();

            pnlHeader = new Panel();
            lblTitle = new Label();
            lblStudent = new Label();
            pnlMainContainer = new Panel();
            dgvProducts = new DataGridView();
            pnlSearchCard = new Panel();
            lblSearch = new Label();
            txtKeyword = new TextBox();
            btnSearch = new Button();
            btnLoad = new Button();
            pnlProductCard = new Panel();
            lblCardHeader = new Label();
            lblId = new Label();
            txtId = new TextBox();
            lblBarcode = new Label();
            txtBarcode = new TextBox();
            lblProductName = new Label();
            txtProductName = new TextBox();
            lblPrice = new Label();
            txtPrice = new TextBox();
            lblStockQuantity = new Label();
            txtStockQuantity = new TextBox();
            lblCategory = new Label();
            cboCategory = new ComboBox();
            flpButtons = new FlowLayoutPanel();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            btnClear = new Button();

            pnlHeader.SuspendLayout();
            pnlMainContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProducts).BeginInit();
            pnlSearchCard.SuspendLayout();
            pnlProductCard.SuspendLayout();
            flpButtons.SuspendLayout();
            SuspendLayout();

            // pnlHeader
            pnlHeader.BackColor = Color.FromArgb(11, 30, 54);
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Controls.Add(lblStudent);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(1220, 75);
            pnlHeader.TabIndex = 0;

            // lblTitle
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(212, 175, 55);
            lblTitle.Location = new Point(20, 12);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(266, 35);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "QUẢN LÝ SẢN PHẨM";

            // lblStudent
            lblStudent.AutoSize = true;
            lblStudent.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lblStudent.ForeColor = Color.FromArgb(180, 200, 220);
            lblStudent.Location = new Point(23, 47);
            lblStudent.Name = "lblStudent";
            lblStudent.Size = new Size(344, 20);
            lblStudent.TabIndex = 1;
            lblStudent.Text = "VO THI THUY AN - CCQ2411C - MSSV: 2124110097";

            // pnlMainContainer
            pnlMainContainer.AutoScroll = true;
            pnlMainContainer.Controls.Add(dgvProducts);
            pnlMainContainer.Controls.Add(pnlSearchCard);
            pnlMainContainer.Controls.Add(pnlProductCard);
            pnlMainContainer.Dock = DockStyle.Fill;
            pnlMainContainer.Location = new Point(0, 75);
            pnlMainContainer.Name = "pnlMainContainer";
            pnlMainContainer.Padding = new Padding(20);
            pnlMainContainer.Size = new Size(1220, 775);
            pnlMainContainer.TabIndex = 1;

            // dgvProducts
            dgvProducts.AllowUserToAddRows = false;
            dgvProducts.AllowUserToDeleteRows = false;

            dataGridViewCellStyle1.BackColor = Color.FromArgb(248, 249, 251);
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(232, 240, 254);
            dataGridViewCellStyle1.SelectionForeColor = Color.FromArgb(11, 30, 54);
            dgvProducts.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;

            dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProducts.BackgroundColor = Color.White;
            dgvProducts.BorderStyle = BorderStyle.None;
            dgvProducts.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(11, 30, 54);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(212, 175, 55);
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(11, 30, 54);
            dataGridViewCellStyle2.SelectionForeColor = Color.White;
            dgvProducts.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvProducts.ColumnHeadersHeight = 42;

            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9.5F);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(40, 40, 40);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(232, 240, 254);
            dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(11, 30, 54);
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dgvProducts.DefaultCellStyle = dataGridViewCellStyle3;

            dgvProducts.Dock = DockStyle.Fill;
            dgvProducts.EnableHeadersVisualStyles = false;
            dgvProducts.GridColor = Color.FromArgb(230, 235, 240);
            dgvProducts.Location = new Point(20, 290);
            dgvProducts.MultiSelect = false;
            dgvProducts.Name = "dgvProducts";
            dgvProducts.ReadOnly = true;
            dgvProducts.RowHeadersVisible = false;
            dgvProducts.RowHeadersWidth = 51;
            dgvProducts.RowTemplate.Height = 35;
            dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProducts.Size = new Size(1180, 465);
            dgvProducts.TabIndex = 0;
            dgvProducts.CellContentClick += dgvProducts_CellContentClick;

            // pnlSearchCard
            pnlSearchCard.BackColor = Color.White;
            pnlSearchCard.Controls.Add(lblSearch);
            pnlSearchCard.Controls.Add(txtKeyword);
            pnlSearchCard.Controls.Add(btnSearch);
            pnlSearchCard.Controls.Add(btnLoad);
            pnlSearchCard.Dock = DockStyle.Top;
            pnlSearchCard.Location = new Point(20, 230);
            pnlSearchCard.Name = "pnlSearchCard";
            pnlSearchCard.Size = new Size(1180, 60);
            pnlSearchCard.TabIndex = 1;

            // lblSearch
            lblSearch.AutoSize = true;
            lblSearch.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblSearch.ForeColor = Color.FromArgb(11, 30, 54);
            lblSearch.Location = new Point(20, 18);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(85, 21);
            lblSearch.TabIndex = 0;
            lblSearch.Text = "Tìm kiếm:";

            // txtKeyword
            txtKeyword.BorderStyle = BorderStyle.FixedSingle;
            txtKeyword.Font = new Font("Segoe UI", 10F);
            txtKeyword.Location = new Point(115, 15);
            txtKeyword.Name = "txtKeyword";
            txtKeyword.PlaceholderText = "Nhập tên sản phẩm hoặc mã vạch cần tìm...";
            txtKeyword.Size = new Size(585, 30);
            txtKeyword.TabIndex = 1;
            txtKeyword.TextChanged += txtKeyword_TextChanged;

            // btnSearch
            btnSearch.Location = new Point(720, 12);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(120, 36);
            btnSearch.TabIndex = 2;
            btnSearch.UseVisualStyleBackColor = false;

            // btnLoad
            btnLoad.Location = new Point(850, 12);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(130, 36);
            btnLoad.TabIndex = 3;
            btnLoad.UseVisualStyleBackColor = false;

            // pnlProductCard
            pnlProductCard.BackColor = Color.White;
            pnlProductCard.Controls.Add(lblCardHeader);
            pnlProductCard.Controls.Add(lblId);
            pnlProductCard.Controls.Add(txtId);
            pnlProductCard.Controls.Add(lblBarcode);
            pnlProductCard.Controls.Add(txtBarcode);
            pnlProductCard.Controls.Add(lblProductName);
            pnlProductCard.Controls.Add(txtProductName);
            pnlProductCard.Controls.Add(lblPrice);
            pnlProductCard.Controls.Add(txtPrice);
            pnlProductCard.Controls.Add(lblStockQuantity);
            pnlProductCard.Controls.Add(txtStockQuantity);
            pnlProductCard.Controls.Add(lblCategory);
            pnlProductCard.Controls.Add(cboCategory);
            pnlProductCard.Controls.Add(flpButtons);
            pnlProductCard.Dock = DockStyle.Top;
            pnlProductCard.Location = new Point(20, 20);
            pnlProductCard.Name = "pnlProductCard";
            pnlProductCard.Size = new Size(1180, 210);
            pnlProductCard.TabIndex = 0;
            pnlProductCard.Paint += pnlProductCard_Paint;

            // lblCardHeader
            lblCardHeader.AutoSize = true;
            lblCardHeader.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblCardHeader.ForeColor = Color.FromArgb(11, 30, 54);
            lblCardHeader.Location = new Point(15, 12);
            lblCardHeader.Name = "lblCardHeader";
            lblCardHeader.Size = new Size(253, 25);
            lblCardHeader.TabIndex = 0;
            lblCardHeader.Text = "Thông tin chi tiết sản phẩm";

            // lblId
            lblId.AutoSize = true;
            lblId.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblId.ForeColor = Color.FromArgb(70, 80, 95);
            lblId.Location = new Point(20, 55);
            lblId.Name = "lblId";
            lblId.Size = new Size(59, 21);
            lblId.TabIndex = 1;
            lblId.Text = "Mã ID:";

            // txtId
            txtId.BackColor = Color.FromArgb(242, 244, 247);
            txtId.BorderStyle = BorderStyle.FixedSingle;
            txtId.Font = new Font("Segoe UI", 10F);
            txtId.Location = new Point(115, 52);
            txtId.Name = "txtId";
            txtId.ReadOnly = true;
            txtId.Size = new Size(205, 30);
            txtId.TabIndex = 2;

            // lblBarcode
            lblBarcode.AutoSize = true;
            lblBarcode.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblBarcode.ForeColor = Color.FromArgb(70, 80, 95);
            lblBarcode.Location = new Point(345, 55);
            lblBarcode.Name = "lblBarcode";
            lblBarcode.Size = new Size(78, 21);
            lblBarcode.TabIndex = 3;
            lblBarcode.Text = "Mã vạch:";

            // txtBarcode
            txtBarcode.BorderStyle = BorderStyle.FixedSingle;
            txtBarcode.Font = new Font("Segoe UI", 10F);
            txtBarcode.Location = new Point(440, 52);
            txtBarcode.Name = "txtBarcode";
            txtBarcode.Size = new Size(260, 30);
            txtBarcode.TabIndex = 4;

            // lblProductName
            lblProductName.AutoSize = true;
            lblProductName.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblProductName.ForeColor = Color.FromArgb(70, 80, 95);
            lblProductName.Location = new Point(730, 55);
            lblProductName.Name = "lblProductName";
            lblProductName.Size = new Size(64, 21);
            lblProductName.TabIndex = 5;
            lblProductName.Text = "Tên SP:";

            // txtProductName
            txtProductName.BorderStyle = BorderStyle.FixedSingle;
            txtProductName.Font = new Font("Segoe UI", 10F);
            txtProductName.Location = new Point(810, 52);
            txtProductName.Name = "txtProductName";
            txtProductName.Size = new Size(350, 30);
            txtProductName.TabIndex = 6;

            // lblPrice
            lblPrice.AutoSize = true;
            lblPrice.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblPrice.ForeColor = Color.FromArgb(70, 80, 95);
            lblPrice.Location = new Point(20, 103);
            lblPrice.Name = "lblPrice";
            lblPrice.Size = new Size(39, 21);
            lblPrice.TabIndex = 7;
            lblPrice.Text = "Giá:";

            // txtPrice
            txtPrice.BorderStyle = BorderStyle.FixedSingle;
            txtPrice.Font = new Font("Segoe UI", 10F);
            txtPrice.Location = new Point(115, 100);
            txtPrice.Name = "txtPrice";
            txtPrice.Size = new Size(205, 30);
            txtPrice.TabIndex = 8;

            // lblStockQuantity
            lblStockQuantity.AutoSize = true;
            lblStockQuantity.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblStockQuantity.ForeColor = Color.FromArgb(70, 80, 95);
            lblStockQuantity.Location = new Point(345, 103);
            lblStockQuantity.Name = "lblStockQuantity";
            lblStockQuantity.Size = new Size(76, 21);
            lblStockQuantity.TabIndex = 9;
            lblStockQuantity.Text = "Tồn kho:";

            // txtStockQuantity
            txtStockQuantity.BorderStyle = BorderStyle.FixedSingle;
            txtStockQuantity.Font = new Font("Segoe UI", 10F);
            txtStockQuantity.Location = new Point(440, 100);
            txtStockQuantity.Name = "txtStockQuantity";
            txtStockQuantity.Size = new Size(260, 30);
            txtStockQuantity.TabIndex = 10;

            // lblCategory
            lblCategory.AutoSize = true;
            lblCategory.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblCategory.ForeColor = Color.FromArgb(70, 80, 95);
            lblCategory.Location = new Point(730, 103);
            lblCategory.Name = "lblCategory";
            lblCategory.Size = new Size(92, 21);
            lblCategory.TabIndex = 11;
            lblCategory.Text = "Danh mục:";

            // cboCategory
            cboCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCategory.Font = new Font("Segoe UI", 10F);
            cboCategory.FormattingEnabled = true;
            cboCategory.Location = new Point(810, 100);
            cboCategory.Name = "cboCategory";
            cboCategory.Size = new Size(350, 31);
            cboCategory.TabIndex = 12;

            // flpButtons
            flpButtons.Controls.Add(btnAdd);
            flpButtons.Controls.Add(btnUpdate);
            flpButtons.Controls.Add(btnDelete);
            flpButtons.Controls.Add(btnClear);
            flpButtons.Location = new Point(115, 148);
            flpButtons.Name = "flpButtons";
            flpButtons.Size = new Size(650, 48);
            flpButtons.TabIndex = 13;
            flpButtons.WrapContents = false;

            // Các nút thao tác
            btnAdd.Name = "btnAdd";
            btnAdd.TabIndex = 0;
            FormatButton(btnAdd, "Thêm",
                Color.FromArgb(40, 167, 69), Color.White);

            btnUpdate.Name = "btnUpdate";
            btnUpdate.TabIndex = 1;
            FormatButton(btnUpdate, "Sửa",
                Color.FromArgb(0, 123, 255), Color.White);

            btnDelete.Name = "btnDelete";
            btnDelete.TabIndex = 2;
            FormatButton(btnDelete, "Xóa",
                Color.FromArgb(220, 53, 69), Color.White);

            btnClear.Name = "btnClear";
            btnClear.TabIndex = 3;
            FormatButton(btnClear, "Làm mới",
                Color.FromArgb(108, 117, 125), Color.White);

            // Nút tìm kiếm và tải dữ liệu
            FormatButton(btnSearch, "Tìm kiếm",
                Color.FromArgb(212, 175, 55), Color.FromArgb(11, 30, 54));

            btnSearch.Location = new Point(720, 12);
            btnSearch.Size = new Size(120, 36);
            btnSearch.TabIndex = 2;

            FormatButton(btnLoad, "Tải dữ liệu",
                Color.FromArgb(11, 30, 54), Color.White);

            btnLoad.Location = new Point(850, 12);
            btnLoad.Size = new Size(130, 36);
            btnLoad.TabIndex = 3;

            // FormProductManagement
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(244, 246, 249);
            ClientSize = new Size(1220, 850);
            Controls.Add(pnlMainContainer);
            Controls.Add(pnlHeader);
            MinimumSize = new Size(1050, 750);
            Name = "FormProductManagement";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản lý sản phẩm - Happy Mart";

            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlMainContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvProducts).EndInit();
            pnlSearchCard.ResumeLayout(false);
            pnlSearchCard.PerformLayout();
            pnlProductCard.ResumeLayout(false);
            pnlProductCard.PerformLayout();
            flpButtons.ResumeLayout(false);
            ResumeLayout(false);
        }

        private void FormatButton(
            Button btn,
            string text,
            Color backColor,
            Color foreColor)
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
            btn.UseVisualStyleBackColor = false;
        }

        #endregion

        private Panel pnlHeader;
        private Label lblTitle;
        private Label lblStudent;
        private Panel pnlMainContainer;
        private Panel pnlProductCard;
        private Label lblCardHeader;
        private Label lblId;
        private TextBox txtId;
        private Label lblBarcode;
        private TextBox txtBarcode;
        private Label lblProductName;
        private TextBox txtProductName;
        private Label lblPrice;
        private TextBox txtPrice;
        private Label lblStockQuantity;
        private TextBox txtStockQuantity;
        private Label lblCategory;
        private ComboBox cboCategory;
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
        private DataGridView dgvProducts;
    }
}
