//********** HOTEN: VO THI THUY AN
//********** LOP: CCQ2411C
//********** MSSV: 2124110097
//********** MON: LAP TRINH UNG DUNG
//********** DE TAI: HAPPY MART - POS THU NGAN TICH HOP QUAN LY KHO

namespace MiniSupermarket.WinForms
{
    partial class FormPOS
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Panel pnlProducts;
        private System.Windows.Forms.Panel pnlCart;

        private System.Windows.Forms.Label lblProductsTitle;
        private System.Windows.Forms.Label lblCartTitle;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.Label lblCustomer;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.Label lblStock;
        private System.Windows.Forms.Label lblTotal;

        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ComboBox cboCustomer;
        private System.Windows.Forms.NumericUpDown nudQuantity;

        private System.Windows.Forms.DataGridView dgvProducts;
        private System.Windows.Forms.DataGridView DataGridViewCart;

        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnCheckout;

        // Cột định danh chuẩn cho bảng sản phẩm
        private System.Windows.Forms.DataGridViewTextBoxColumn colProdId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProdName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProdPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProdStock;

        // Cột định danh chuẩn cho giỏ hàng
        private System.Windows.Forms.DataGridViewTextBoxColumn colCartId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCartName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCartQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCartPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCartAmount;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            pnlHeader = new Panel();
            lblTitle = new Label();
            tlpMain = new TableLayoutPanel();
            pnlProducts = new Panel();
            lblProductsTitle = new Label();
            lblSearch = new Label();
            txtSearch = new TextBox();
            dgvProducts = new DataGridView();
            colProdId = new DataGridViewTextBoxColumn();
            colProdName = new DataGridViewTextBoxColumn();
            colProdPrice = new DataGridViewTextBoxColumn();
            colProdStock = new DataGridViewTextBoxColumn();
            lblStock = new Label();
            lblQuantity = new Label();
            nudQuantity = new NumericUpDown();
            btnAdd = new Button();
            pnlCart = new Panel();
            lblCartTitle = new Label();
            DataGridViewCart = new DataGridView();
            colCartId = new DataGridViewTextBoxColumn();
            colCartName = new DataGridViewTextBoxColumn();
            colCartQty = new DataGridViewTextBoxColumn();
            colCartPrice = new DataGridViewTextBoxColumn();
            colCartAmount = new DataGridViewTextBoxColumn();
            btnRemove = new Button();
            lblCustomer = new Label();
            cboCustomer = new ComboBox();
            lblTotal = new Label();
            btnCheckout = new Button();

            pnlHeader.SuspendLayout();
            tlpMain.SuspendLayout();
            pnlProducts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProducts).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudQuantity).BeginInit();
            pnlCart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DataGridViewCart).BeginInit();
            SuspendLayout();

            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.FromArgb(11, 30, 54); // Dark Navy chuẩn Happy Mart
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(1140, 60);
            pnlHeader.TabIndex = 0;

            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(212, 175, 55); // Metallic Gold
            lblTitle.Location = new Point(20, 12);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(505, 37);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "HAPPY MART  |  QUẦY THANH TOÁN POS";

            // 
            // tlpMain
            // 
            tlpMain.ColumnCount = 2;
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpMain.Controls.Add(pnlProducts, 0, 0);
            tlpMain.Controls.Add(pnlCart, 1, 0);
            tlpMain.Dock = DockStyle.Fill;
            tlpMain.Location = new Point(0, 60);
            tlpMain.Name = "tlpMain";
            tlpMain.Padding = new Padding(12);
            tlpMain.RowCount = 1;
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpMain.Size = new Size(1140, 620);
            tlpMain.TabIndex = 1;

            // 
            // pnlProducts (Khung danh sách sản phẩm)
            // 
            pnlProducts.BackColor = Color.White;
            pnlProducts.BorderStyle = BorderStyle.FixedSingle;
            pnlProducts.Controls.Add(lblProductsTitle);
            pnlProducts.Controls.Add(lblSearch);
            pnlProducts.Controls.Add(txtSearch);
            pnlProducts.Controls.Add(dgvProducts);
            pnlProducts.Controls.Add(lblStock);
            pnlProducts.Controls.Add(lblQuantity);
            pnlProducts.Controls.Add(nudQuantity);
            pnlProducts.Controls.Add(btnAdd);
            pnlProducts.Dock = DockStyle.Fill;
            pnlProducts.Margin = new Padding(6);
            pnlProducts.Name = "pnlProducts";
            pnlProducts.Padding = new Padding(14);

            // 
            // lblProductsTitle
            // 
            lblProductsTitle.AutoSize = true;
            lblProductsTitle.Font = new Font("Segoe UI", 12.5F, FontStyle.Bold);
            lblProductsTitle.ForeColor = Color.FromArgb(11, 30, 54);
            lblProductsTitle.Location = new Point(14, 14);
            lblProductsTitle.Name = "lblProductsTitle";
            lblProductsTitle.Size = new Size(245, 30);
            lblProductsTitle.Text = "DANH SÁCH SẢN PHẨM";

            // 
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblSearch.ForeColor = Color.FromArgb(33, 37, 41);
            lblSearch.Location = new Point(14, 56);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(82, 21);
            lblSearch.Text = "Tìm kiếm:";

            // 
            // txtSearch
            // 
            txtSearch.Font = new Font("Segoe UI", 10.5F);
            txtSearch.Location = new Point(105, 52);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(432, 31);
            txtSearch.TabIndex = 1;

            // 
            // dgvProducts
            // 
            ConfigureGrid(dgvProducts);
            dgvProducts.Columns.Clear();
            dgvProducts.Columns.AddRange(new DataGridViewColumn[] { colProdId, colProdName, colProdPrice, colProdStock });
            dgvProducts.Location = new Point(14, 96);
            dgvProducts.Name = "dgvProducts";
            dgvProducts.Size = new Size(523, 390);
            dgvProducts.TabIndex = 2;

            colProdId.HeaderText = "Mã ID";
            colProdId.Name = "colProdId";
            colProdId.FillWeight = 45F;

            colProdName.HeaderText = "Tên sản phẩm";
            colProdName.Name = "colProdName";
            colProdName.FillWeight = 165F;

            colProdPrice.HeaderText = "Đơn giá (đ)";
            colProdPrice.Name = "colProdPrice";
            colProdPrice.FillWeight = 90F;

            colProdStock.HeaderText = "Tồn kho";
            colProdStock.Name = "colProdStock";
            colProdStock.FillWeight = 65F;

            // 
            // lblStock
            // 
            lblStock.AutoSize = true;
            lblStock.Font = new Font("Segoe UI", 9.5F, FontStyle.Italic);
            lblStock.ForeColor = Color.DimGray;
            lblStock.Location = new Point(14, 500);
            lblStock.Name = "lblStock";
            lblStock.Size = new Size(220, 21);
            lblStock.Text = "Chọn sản phẩm để xem tồn kho";

            // 
            // lblQuantity
            // 
            lblQuantity.AutoSize = true;
            lblQuantity.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblQuantity.ForeColor = Color.FromArgb(33, 37, 41);
            lblQuantity.Location = new Point(14, 538);
            lblQuantity.Name = "lblQuantity";
            lblQuantity.Size = new Size(81, 21);
            lblQuantity.Text = "Số lượng:";

            // 
            // nudQuantity
            // 
            nudQuantity.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            nudQuantity.Location = new Point(105, 534);
            nudQuantity.Maximum = new decimal(new int[] { 999, 0, 0, 0 });
            nudQuantity.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudQuantity.Name = "nudQuantity";
            nudQuantity.Size = new Size(90, 31);
            nudQuantity.TabIndex = 3;
            nudQuantity.Value = new decimal(new int[] { 1, 0, 0, 0 });

            // 
            // btnAdd
            // 
            StyleButton(btnAdd, "+ THÊM VÀO GIỎ HÀNG", Color.FromArgb(11, 30, 54), Color.FromArgb(212, 175, 55));
            btnAdd.Location = new Point(210, 529);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(327, 40);
            btnAdd.TabIndex = 4;

            // 
            // pnlCart (Khung giỏ hàng)
            // 
            pnlCart.BackColor = Color.White;
            pnlCart.BorderStyle = BorderStyle.FixedSingle;
            pnlCart.Controls.Add(lblCartTitle);
            pnlCart.Controls.Add(DataGridViewCart);
            pnlCart.Controls.Add(btnRemove);
            pnlCart.Controls.Add(lblCustomer);
            pnlCart.Controls.Add(cboCustomer);
            pnlCart.Controls.Add(lblTotal);
            pnlCart.Controls.Add(btnCheckout);
            pnlCart.Dock = DockStyle.Fill;
            pnlCart.Margin = new Padding(6);
            pnlCart.Name = "pnlCart";
            pnlCart.Padding = new Padding(14);

            // 
            // lblCartTitle
            // 
            lblCartTitle.AutoSize = true;
            lblCartTitle.Font = new Font("Segoe UI", 12.5F, FontStyle.Bold);
            lblCartTitle.ForeColor = Color.FromArgb(11, 30, 54);
            lblCartTitle.Location = new Point(14, 14);
            lblCartTitle.Name = "lblCartTitle";
            lblCartTitle.Size = new Size(250, 30);
            lblCartTitle.Text = "GIỎ HÀNG THANH TOÁN";

            // 
            // DataGridViewCart (dgvCart)
            // 
            ConfigureGrid(DataGridViewCart);
            DataGridViewCart.Columns.Clear();
            DataGridViewCart.Columns.AddRange(new DataGridViewColumn[] { colCartId, colCartName, colCartQty, colCartPrice, colCartAmount });
            DataGridViewCart.Location = new Point(14, 52);
            DataGridViewCart.Name = "dgvCart";
            DataGridViewCart.Size = new Size(523, 310);
            DataGridViewCart.TabIndex = 1;

            colCartId.HeaderText = "ID";
            colCartId.Name = "colCartId";
            colCartId.FillWeight = 35F;

            colCartName.HeaderText = "Tên sản phẩm";
            colCartName.Name = "colCartName";
            colCartName.FillWeight = 150F;

            colCartQty.HeaderText = "SL";
            colCartQty.Name = "colCartQty";
            colCartQty.FillWeight = 40F;

            colCartPrice.HeaderText = "Đơn giá";
            colCartPrice.Name = "colCartPrice";
            colCartPrice.FillWeight = 85F;

            colCartAmount.HeaderText = "Thành tiền";
            colCartAmount.Name = "colCartAmount";
            colCartAmount.FillWeight = 95F;

            // 
            // btnRemove
            // 
            StyleButton(btnRemove, "🗑 Xóa sản phẩm", Color.FromArgb(190, 40, 40), Color.White);
            btnRemove.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnRemove.Location = new Point(14, 374);
            btnRemove.Name = "btnRemove";
            btnRemove.Size = new Size(160, 35);
            btnRemove.TabIndex = 2;

            // 
            // lblCustomer
            // 
            lblCustomer.AutoSize = true;
            lblCustomer.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblCustomer.ForeColor = Color.FromArgb(33, 37, 41);
            lblCustomer.Location = new Point(14, 424);
            lblCustomer.Name = "lblCustomer";
            lblCustomer.Size = new Size(103, 21);
            lblCustomer.Text = "Khách hàng:";

            // 
            // cboCustomer
            // 
            cboCustomer.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCustomer.Font = new Font("Segoe UI", 10.5F);
            cboCustomer.Location = new Point(125, 420);
            cboCustomer.Name = "cboCustomer";
            cboCustomer.Size = new Size(412, 31);
            cboCustomer.TabIndex = 3;

            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTotal.ForeColor = Color.FromArgb(11, 30, 54);
            lblTotal.Location = new Point(14, 466);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(198, 37);
            lblTotal.Text = "Tổng tiền: 0 đ";

            // 
            // btnCheckout
            // 
            StyleButton(btnCheckout, "💳 XÁC NHẬN THANH TOÁN", Color.FromArgb(212, 175, 55), Color.FromArgb(11, 30, 54));
            btnCheckout.Font = new Font("Segoe UI", 11.5F, FontStyle.Bold);
            btnCheckout.Location = new Point(14, 514);
            btnCheckout.Name = "btnCheckout";
            btnCheckout.Size = new Size(523, 46);
            btnCheckout.TabIndex = 4;

            // 
            // FormPOS
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 249, 250);
            ClientSize = new Size(1140, 680);
            Controls.Add(tlpMain);
            Controls.Add(pnlHeader);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FormPOS";
            Text = "Happy Mart - POS Thu ngân";
            Load += FormPOS_Load;

            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            tlpMain.ResumeLayout(false);
            pnlProducts.ResumeLayout(false);
            pnlProducts.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProducts).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudQuantity).EndInit();
            pnlCart.ResumeLayout(false);
            pnlCart.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)DataGridViewCart).EndInit();
            ResumeLayout(false);
        }

        private void ConfigureGrid(DataGridView grid)
        {
            // Bật double buffer để cuộn bảng mượt mà không bị giật xé hình
            typeof(DataGridView).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.SetProperty,
                null, grid, new object[] { true });

            grid.BackgroundColor = Color.White;
            grid.BorderStyle = BorderStyle.None;
            grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal; // Chỉ để đường gạch ngang mỏng, bỏ gạch dọc
            grid.GridColor = Color.FromArgb(235, 238, 242);                      // Màu gạch ngang xám cực nhẹ

            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.ReadOnly = true;
            grid.MultiSelect = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.AutoGenerateColumns = false;
            grid.RowHeadersVisible = false;                                       // Tắt cột mũi tên bên trái
            grid.ColumnHeadersHeight = 40;
            grid.RowTemplate.Height = 32;                                         // Giúp dòng cao rảnh rỗi, dễ nhìn hơn
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // XÓA BỎ VIỀN NÉT ĐỨT (FOCUS RECTANGLE) AN TOÀN KHÔNG BÁO LỖI
            grid.CellPainting -= Grid_CellPainting;
            grid.CellPainting += Grid_CellPainting;

            // Header Dark Navy & Metallic Gold
            grid.EnableHeadersVisualStyles = false;
            grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(11, 30, 54);      // #0B1E36 Dark Navy
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(212, 175, 55);    // #D4AF37 Metallic Gold
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            grid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            grid.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(11, 30, 54); // Giữ nguyên màu Navy khi focus header

            // Style dòng dữ liệu
            grid.DefaultCellStyle.BackColor = Color.White;
            grid.DefaultCellStyle.ForeColor = Color.FromArgb(40, 40, 40);
            grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(232, 240, 254);     // Màu xanh nhạt dịu nhẹ khi chọn dòng
            grid.DefaultCellStyle.SelectionForeColor = Color.FromArgb(11, 30, 54);
            grid.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);

            // Dòng xen kẽ màu (Zebra Striping) nhẹ nhàng
            grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            grid.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(232, 240, 254);
            grid.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.FromArgb(11, 30, 54);
        }

        // HÀM HỖ TRỢ XÓA VIỀN NÉT ĐỨT KHÔNG LỖI
        private void Grid_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // Vẽ lại cell bình thường nhưng loại bỏ hiệu ứng focus rectangle mặc định của Windows
                e.Paint(e.ClipBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.Focus);
                e.Handled = true;
            }
        }

        private void StyleButton(Button button, string text, Color backColor, Color foreColor)
        {
            button.Text = text;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.BackColor = backColor;
            button.ForeColor = foreColor;
            button.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            button.Cursor = Cursors.Hand;
        }
    }
}