
// ================================================================
// ********** THÔNG TIN SINH VIÊN **********
// HỌ TÊN : VO THI THUY AN
// LỚP    : CCQ2411C
// MSSV   : 2124110097
// MÔN    : LẬP TRÌNH ỨNG DỤNG
// NGÀY   : 26/09/2026
// ================================================================
// MÔ TẢ:
// File thiết kế giao diện quản lý sản phẩm (Product) trên WinForms.
// ================================================================

namespace MiniSupermarket.WinForms
{
    partial class FormProductManagement
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

        /// <summary>
        /// Required method for Designer support.
        /// </summary>
        private void InitializeComponent()
        {
            lblTitle = new Label();
            lblStudent = new Label();
            groupProductInfo = new GroupBox();
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
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            btnClear = new Button();
            lblSearch = new Label();
            txtKeyword = new TextBox();
            btnSearch = new Button();
            btnLoad = new Button();
            dgvProducts = new DataGridView();
            groupProductInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProducts).BeginInit();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTitle.Location = new Point(29, 27);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(359, 46);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "QUẢN LÝ SẢN PHẨM";
            // 
            // lblStudent
            // 
            lblStudent.AutoSize = true;
            lblStudent.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lblStudent.Location = new Point(32, 77);
            lblStudent.Name = "lblStudent";
            lblStudent.Size = new Size(344, 20);
            lblStudent.TabIndex = 1;
            lblStudent.Text = "VO THI THUY AN - CCQ2411C - MSSV: 2124110097";
            // 
            // groupProductInfo
            // 
            groupProductInfo.Controls.Add(lblId);
            groupProductInfo.Controls.Add(txtId);
            groupProductInfo.Controls.Add(lblBarcode);
            groupProductInfo.Controls.Add(txtBarcode);
            groupProductInfo.Controls.Add(lblProductName);
            groupProductInfo.Controls.Add(txtProductName);
            groupProductInfo.Controls.Add(lblPrice);
            groupProductInfo.Controls.Add(txtPrice);
            groupProductInfo.Controls.Add(lblStockQuantity);
            groupProductInfo.Controls.Add(txtStockQuantity);
            groupProductInfo.Controls.Add(lblCategory);
            groupProductInfo.Controls.Add(cboCategory);
            groupProductInfo.Controls.Add(btnAdd);
            groupProductInfo.Controls.Add(btnUpdate);
            groupProductInfo.Controls.Add(btnDelete);
            groupProductInfo.Controls.Add(btnClear);
            groupProductInfo.Location = new Point(23, 113);
            groupProductInfo.Margin = new Padding(3, 4, 3, 4);
            groupProductInfo.Name = "groupProductInfo";
            groupProductInfo.Padding = new Padding(3, 4, 3, 4);
            groupProductInfo.Size = new Size(1189, 253);
            groupProductInfo.TabIndex = 2;
            groupProductInfo.TabStop = false;
            groupProductInfo.Text = "Thông tin sản phẩm";
            groupProductInfo.Enter += groupProductInfo_Enter;
            // 
            // lblId
            // 
            lblId.AutoSize = true;
            lblId.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblId.Location = new Point(23, 43);
            lblId.Name = "lblId";
            lblId.Size = new Size(55, 20);
            lblId.TabIndex = 0;
            lblId.Text = "Mã ID:";
            // 
            // txtId
            // 
            txtId.Location = new Point(114, 36);
            txtId.Margin = new Padding(3, 4, 3, 4);
            txtId.Name = "txtId";
            txtId.ReadOnly = true;
            txtId.Size = new Size(205, 27);
            txtId.TabIndex = 1;
            // 
            // lblBarcode
            // 
            lblBarcode.AutoSize = true;
            lblBarcode.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblBarcode.Location = new Point(354, 43);
            lblBarcode.Name = "lblBarcode";
            lblBarcode.Size = new Size(71, 20);
            lblBarcode.TabIndex = 2;
            lblBarcode.Text = "Mã vạch:";
            // 
            // txtBarcode
            // 
            txtBarcode.Location = new Point(457, 36);
            txtBarcode.Margin = new Padding(3, 4, 3, 4);
            txtBarcode.Name = "txtBarcode";
            txtBarcode.Size = new Size(251, 27);
            txtBarcode.TabIndex = 3;
            // 
            // lblProductName
            // 
            lblProductName.AutoSize = true;
            lblProductName.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblProductName.Location = new Point(743, 43);
            lblProductName.Name = "lblProductName";
            lblProductName.Size = new Size(59, 20);
            lblProductName.TabIndex = 4;
            lblProductName.Text = "Tên SP:";
            // 
            // txtProductName
            // 
            txtProductName.Location = new Point(834, 36);
            txtProductName.Margin = new Padding(3, 4, 3, 4);
            txtProductName.Name = "txtProductName";
            txtProductName.Size = new Size(319, 27);
            txtProductName.TabIndex = 5;
            // 
            // lblPrice
            // 
            lblPrice.AutoSize = true;
            lblPrice.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblPrice.Location = new Point(23, 103);
            lblPrice.Name = "lblPrice";
            lblPrice.Size = new Size(36, 20);
            lblPrice.TabIndex = 6;
            lblPrice.Text = "Giá:";
            // 
            // txtPrice
            // 
            txtPrice.Location = new Point(114, 96);
            txtPrice.Margin = new Padding(3, 4, 3, 4);
            txtPrice.Name = "txtPrice";
            txtPrice.Size = new Size(205, 27);
            txtPrice.TabIndex = 7;
            // 
            // lblStockQuantity
            // 
            lblStockQuantity.AutoSize = true;
            lblStockQuantity.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblStockQuantity.Location = new Point(354, 103);
            lblStockQuantity.Name = "lblStockQuantity";
            lblStockQuantity.Size = new Size(70, 20);
            lblStockQuantity.TabIndex = 8;
            lblStockQuantity.Text = "Tồn kho:";
            // 
            // txtStockQuantity
            // 
            txtStockQuantity.Location = new Point(457, 96);
            txtStockQuantity.Margin = new Padding(3, 4, 3, 4);
            txtStockQuantity.Name = "txtStockQuantity";
            txtStockQuantity.Size = new Size(251, 27);
            txtStockQuantity.TabIndex = 9;
            // 
            // lblCategory
            // 
            lblCategory.AutoSize = true;
            lblCategory.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblCategory.Location = new Point(743, 103);
            lblCategory.Name = "lblCategory";
            lblCategory.Size = new Size(96, 20);
            lblCategory.TabIndex = 10;
            lblCategory.Text = "Nhóm hàng:";
            // 
            // cboCategory
            // 
            cboCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCategory.FormattingEnabled = true;
            cboCategory.Location = new Point(834, 96);
            cboCategory.Margin = new Padding(3, 4, 3, 4);
            cboCategory.Name = "cboCategory";
            cboCategory.Size = new Size(319, 28);
            cboCategory.TabIndex = 11;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(114, 167);
            btnAdd.Margin = new Padding(3, 4, 3, 4);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(114, 43);
            btnAdd.TabIndex = 12;
            btnAdd.Text = "Thêm";
            btnAdd.UseVisualStyleBackColor = true;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(240, 167);
            btnUpdate.Margin = new Padding(3, 4, 3, 4);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(114, 43);
            btnUpdate.TabIndex = 13;
            btnUpdate.Text = "Sửa";
            btnUpdate.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(366, 167);
            btnDelete.Margin = new Padding(3, 4, 3, 4);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(114, 43);
            btnDelete.TabIndex = 14;
            btnDelete.Text = "Xóa";
            btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(491, 167);
            btnClear.Margin = new Padding(3, 4, 3, 4);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(114, 43);
            btnClear.TabIndex = 15;
            btnClear.Text = "Xóa trắng";
            btnClear.UseVisualStyleBackColor = true;
            // 
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblSearch.Location = new Point(29, 393);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(91, 23);
            lblSearch.TabIndex = 3;
            lblSearch.Text = "Tìm kiếm:";
            // 
            // txtKeyword
            // 
            txtKeyword.Location = new Point(126, 388);
            txtKeyword.Margin = new Padding(3, 4, 3, 4);
            txtKeyword.Name = "txtKeyword";
            txtKeyword.Size = new Size(399, 27);
            txtKeyword.TabIndex = 4;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(543, 384);
            btnSearch.Margin = new Padding(3, 4, 3, 4);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(114, 43);
            btnSearch.TabIndex = 5;
            btnSearch.Text = "Tìm kiếm";
            btnSearch.UseVisualStyleBackColor = true;
            // 
            // btnLoad
            // 
            btnLoad.Location = new Point(674, 384);
            btnLoad.Margin = new Padding(3, 4, 3, 4);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(114, 43);
            btnLoad.TabIndex = 6;
            btnLoad.Text = "Tải lại";
            btnLoad.UseVisualStyleBackColor = true;
            // 
            // dgvProducts
            // 
            dgvProducts.AllowUserToAddRows = false;
            dgvProducts.AllowUserToDeleteRows = false;
            dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProducts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProducts.Location = new Point(23, 447);
            dgvProducts.Margin = new Padding(3, 4, 3, 4);
            dgvProducts.MultiSelect = false;
            dgvProducts.Name = "dgvProducts";
            dgvProducts.ReadOnly = true;
            dgvProducts.RowHeadersWidth = 51;
            dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProducts.Size = new Size(1189, 400);
            dgvProducts.TabIndex = 7;
            // 
            // FormProductManagement
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1257, 933);
            Controls.Add(dgvProducts);
            Controls.Add(btnLoad);
            Controls.Add(btnSearch);
            Controls.Add(txtKeyword);
            Controls.Add(lblSearch);
            Controls.Add(groupProductInfo);
            Controls.Add(lblStudent);
            Controls.Add(lblTitle);
            Margin = new Padding(3, 4, 3, 4);
            MinimumSize = new Size(1083, 784);
            Name = "FormProductManagement";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản lý sản phẩm - Mini Supermarket";
            groupProductInfo.ResumeLayout(false);
            groupProductInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProducts).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        // =========================================================
        // CONTROL
        // =========================================================

        private Label lblTitle;
        private Label lblStudent;

        private GroupBox groupProductInfo;

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

        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnClear;

        private Label lblSearch;
        private TextBox txtKeyword;

        private Button btnSearch;
        private Button btnLoad;

        private DataGridView dgvProducts;
    }
}
