
namespace MiniSupermarket.WinForms
{
    partial class FormCustomerManagement
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.Label lblCustomerName;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblKeyword;

        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.TextBox txtCustomerName;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtKeyword;

        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnSearch;

        private System.Windows.Forms.DataGridView dgvCustomers;

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
            lblId = new Label();
            lblCustomerName = new Label();
            lblPhone = new Label();
            lblAddress = new Label();
            lblEmail = new Label();
            lblKeyword = new Label();

            txtId = new TextBox();
            txtCustomerName = new TextBox();
            txtPhone = new TextBox();
            txtAddress = new TextBox();
            txtEmail = new TextBox();
            txtKeyword = new TextBox();

            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            btnLoad = new Button();
            btnSearch = new Button();

            dgvCustomers = new DataGridView();

            ((System.ComponentModel.ISupportInitialize)dgvCustomers).BeginInit();
            SuspendLayout();

            // ============================================================
            // FORM
            // ============================================================

            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;

            ClientSize = new Size(1100, 650);

            Name = "FormCustomerManagement";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản lý khách hàng";

            // ============================================================
            // TITLE
            // ============================================================

            lblTitle.AutoSize = true;
            lblTitle.Font = new Font(
                "Segoe UI",
                20F,
                FontStyle.Bold
            );

            lblTitle.Location = new Point(390, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(300, 46);
            lblTitle.Text = "QUẢN LÝ KHÁCH HÀNG";

            // ============================================================
            // LABEL ID
            // ============================================================

            lblId.AutoSize = true;
            lblId.Location = new Point(40, 90);
            lblId.Name = "lblId";
            lblId.Text = "Mã khách hàng:";

            // ============================================================
            // TEXTBOX ID
            // ============================================================

            txtId.Location = new Point(170, 87);
            txtId.Name = "txtId";
            txtId.Size = new Size(180, 27);
            txtId.ReadOnly = true;

            // ============================================================
            // LABEL CUSTOMER NAME
            // ============================================================

            lblCustomerName.AutoSize = true;
            lblCustomerName.Location = new Point(40, 135);
            lblCustomerName.Name = "lblCustomerName";
            lblCustomerName.Text = "Tên khách hàng:";

            // ============================================================
            // TEXTBOX CUSTOMER NAME
            // ============================================================

            txtCustomerName.Location = new Point(170, 132);
            txtCustomerName.Name = "txtCustomerName";
            txtCustomerName.Size = new Size(300, 27);

            // ============================================================
            // LABEL PHONE
            // ============================================================

            lblPhone.AutoSize = true;
            lblPhone.Location = new Point(500, 90);
            lblPhone.Name = "lblPhone";
            lblPhone.Text = "Số điện thoại:";

            // ============================================================
            // TEXTBOX PHONE
            // ============================================================

            txtPhone.Location = new Point(620, 87);
            txtPhone.Name = "txtPhone";
            txtPhone.Size = new Size(300, 27);

            // ============================================================
            // LABEL ADDRESS
            // ============================================================

            lblAddress.AutoSize = true;
            lblAddress.Location = new Point(500, 135);
            lblAddress.Name = "lblAddress";
            lblAddress.Text = "Địa chỉ:";

            // ============================================================
            // TEXTBOX ADDRESS
            // ============================================================

            txtAddress.Location = new Point(620, 132);
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(300, 27);

            // ============================================================
            // LABEL EMAIL
            // ============================================================

            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(40, 180);
            lblEmail.Name = "lblEmail";
            lblEmail.Text = "Email:";

            // ============================================================
            // TEXTBOX EMAIL
            // ============================================================

            txtEmail.Location = new Point(170, 177);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(300, 27);

            // ============================================================
            // BUTTON ADD
            // ============================================================

            btnAdd.Location = new Point(40, 225);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(120, 40);
            btnAdd.Text = "Thêm";
            btnAdd.UseVisualStyleBackColor = true;

            // ============================================================
            // BUTTON UPDATE
            // ============================================================

            btnUpdate.Location = new Point(175, 225);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(120, 40);
            btnUpdate.Text = "Sửa";
            btnUpdate.UseVisualStyleBackColor = true;

            // ============================================================
            // BUTTON DELETE
            // ============================================================

            btnDelete.Location = new Point(310, 225);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(120, 40);
            btnDelete.Text = "Xóa";
            btnDelete.UseVisualStyleBackColor = true;

            // ============================================================
            // BUTTON LOAD
            // ============================================================

            btnLoad.Location = new Point(445, 225);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(120, 40);
            btnLoad.Text = "Tải lại";
            btnLoad.UseVisualStyleBackColor = true;

            // ============================================================
            // SEARCH LABEL
            // ============================================================

            lblKeyword.AutoSize = true;
            lblKeyword.Location = new Point(40, 295);
            lblKeyword.Name = "lblKeyword";
            lblKeyword.Text = "Tìm kiếm:";

            // ============================================================
            // SEARCH TEXTBOX
            // ============================================================

            txtKeyword.Location = new Point(120, 292);
            txtKeyword.Name = "txtKeyword";
            txtKeyword.Size = new Size(350, 27);

            // ============================================================
            // SEARCH BUTTON
            // ============================================================

            btnSearch.Location = new Point(490, 288);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(120, 35);
            btnSearch.Text = "Tìm kiếm";
            btnSearch.UseVisualStyleBackColor = true;

            // ============================================================
            // DATAGRIDVIEW
            // ============================================================

            dgvCustomers.AllowUserToAddRows = false;
            dgvCustomers.AllowUserToDeleteRows = false;
            dgvCustomers.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dgvCustomers.ColumnHeadersHeightSizeMode =
                DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            dgvCustomers.Location = new Point(40, 340);
            dgvCustomers.MultiSelect = false;
            dgvCustomers.Name = "dgvCustomers";
            dgvCustomers.ReadOnly = true;
            dgvCustomers.RowHeadersWidth = 51;
            dgvCustomers.RowTemplate.Height = 28;
            dgvCustomers.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvCustomers.Size = new Size(1010, 250);

            // ============================================================
            // ADD CONTROLS
            // ============================================================

            Controls.Add(lblTitle);

            Controls.Add(lblId);
            Controls.Add(txtId);

            Controls.Add(lblCustomerName);
            Controls.Add(txtCustomerName);

            Controls.Add(lblPhone);
            Controls.Add(txtPhone);

            Controls.Add(lblAddress);
            Controls.Add(txtAddress);

            Controls.Add(lblEmail);
            Controls.Add(txtEmail);

            Controls.Add(btnAdd);
            Controls.Add(btnUpdate);
            Controls.Add(btnDelete);
            Controls.Add(btnLoad);

            Controls.Add(lblKeyword);
            Controls.Add(txtKeyword);
            Controls.Add(btnSearch);

            Controls.Add(dgvCustomers);

            ((System.ComponentModel.ISupportInitialize)dgvCustomers).EndInit();

            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}

