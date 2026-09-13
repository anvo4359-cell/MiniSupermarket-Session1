namespace MiniSupermarket.WinForms
{
    partial class FormCategoryManagement
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtId = new TextBox();
            txtCategoryName = new TextBox();
            txtDescription = new TextBox();
            txtKeyword = new TextBox();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnSearch = new Button();
            btnLoad = new Button();
            btnDelete = new Button();
            groupBox1 = new GroupBox();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            groupBox2 = new GroupBox();
            dgvCategories = new DataGridView();
            groupBox3 = new GroupBox();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCategories).BeginInit();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // txtId
            // 
            txtId.Location = new Point(6, 57);
            txtId.Name = "txtId";
            txtId.Size = new Size(125, 27);
            txtId.TabIndex = 1;
            // 
            // txtCategoryName
            // 
            txtCategoryName.Location = new Point(6, 128);
            txtCategoryName.Name = "txtCategoryName";
            txtCategoryName.Size = new Size(125, 27);
            txtCategoryName.TabIndex = 2;
            // 
            // txtDescription
            // 
            txtDescription.Location = new Point(6, 204);
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(125, 27);
            txtDescription.TabIndex = 3;
            // 
            // txtKeyword
            // 
            txtKeyword.Location = new Point(6, 19);
            txtKeyword.Name = "txtKeyword";
            txtKeyword.Size = new Size(310, 27);
            txtKeyword.TabIndex = 4;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(566, 344);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(85, 29);
            btnAdd.TabIndex = 5;
            btnAdd.Text = "Thêm mới";
            btnAdd.UseVisualStyleBackColor = true;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(657, 344);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(76, 29);
            btnUpdate.TabIndex = 6;
            btnUpdate.Text = "Cập nhật";
            btnUpdate.UseVisualStyleBackColor = true;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(573, 19);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(94, 29);
            btnSearch.TabIndex = 7;
            btnSearch.Text = "Tìm kiếm";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // btnLoad
            // 
            btnLoad.Location = new Point(673, 19);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(94, 29);
            btnLoad.TabIndex = 8;
            btnLoad.Text = "Tải lại";
            btnLoad.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(739, 344);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(70, 29);
            btnDelete.TabIndex = 9;
            btnDelete.Text = "Xóa";
            btnDelete.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(txtId);
            groupBox1.Controls.Add(txtCategoryName);
            groupBox1.Controls.Add(txtDescription);
            groupBox1.Location = new Point(532, 81);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(277, 234);
            groupBox1.TabIndex = 10;
            groupBox1.TabStop = false;
            groupBox1.Text = "Thông tin Nhóm hàng";
            groupBox1.Enter += groupBox1_Enter;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(7, 181);
            label3.Name = "label3";
            label3.Size = new Size(48, 20);
            label3.TabIndex = 6;
            label3.Text = "Mô tả";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 105);
            label2.Name = "label2";
            label2.Size = new Size(111, 20);
            label2.TabIndex = 5;
            label2.Text = "Tên nhóm hàng";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 34);
            label1.Name = "label1";
            label1.Size = new Size(49, 20);
            label1.TabIndex = 4;
            label1.Text = "Mã ID";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(dgvCategories);
            groupBox2.Location = new Point(12, 81);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(501, 338);
            groupBox2.TabIndex = 11;
            groupBox2.TabStop = false;
            groupBox2.Text = "Danh sách Nhóm hàng";
            // 
            // dgvCategories
            // 
            dgvCategories.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCategories.Location = new Point(16, 26);
            dgvCategories.Name = "dgvCategories";
            dgvCategories.RowHeadersWidth = 51;
            dgvCategories.Size = new Size(451, 306);
            dgvCategories.TabIndex = 0;
            dgvCategories.CellClick += dgvCategories_CellClick;
            dgvCategories.CellContentClick += dgvCategories_CellClick;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(txtKeyword);
            groupBox3.Controls.Add(btnSearch);
            groupBox3.Controls.Add(btnLoad);
            groupBox3.Location = new Point(12, 12);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(797, 62);
            groupBox3.TabIndex = 12;
            groupBox3.TabStop = false;
            groupBox3.Text = "Tìm kiếm";
            // 
            // FormCategoryManagement
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(939, 450);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(btnDelete);
            Controls.Add(btnUpdate);
            Controls.Add(btnAdd);
            Name = "FormCategoryManagement";
            Text = "FormCategoryManagement";
            Load += FormCategoryManagement_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvCategories).EndInit();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private TextBox txtId;
        private TextBox txtCategoryName;
        private TextBox txtDescription;
        private TextBox txtKeyword;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnSearch;
        private Button btnLoad;
        private Button btnDelete;
        private GroupBox groupBox1;
        private Label label2;
        private Label label1;
        private Label label3;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private DataGridView dgvCategories;
    }
}