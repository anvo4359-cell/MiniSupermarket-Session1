namespace MiniSupermarket.WinForms
{
    partial class FormQuickReport
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private Panel pnlHeader;
        private Label lblTitle;
        private Label lblDate;
        private DateTimePicker dtpReportDate;
        private Button btnRunReport;

        private Panel pnlOrders;
        private Panel pnlRevenue;
        private Panel pnlBestSeller;

        private Label lblOrdersTitle;
        private Label lblRevenueTitle;
        private Label lblBestSellerTitle;

        private Label lblTotalOrders;
        private Label lblTotalRevenue;
        private Label lblBestSeller;

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
            lblDate = new Label();
            dtpReportDate = new DateTimePicker();
            btnRunReport = new Button();

            pnlOrders = new Panel();
            pnlRevenue = new Panel();
            pnlBestSeller = new Panel();

            lblOrdersTitle = new Label();
            lblRevenueTitle = new Label();
            lblBestSellerTitle = new Label();

            lblTotalOrders = new Label();
            lblTotalRevenue = new Label();
            lblBestSeller = new Label();

            pnlHeader.SuspendLayout();
            pnlOrders.SuspendLayout();
            pnlRevenue.SuspendLayout();
            pnlBestSeller.SuspendLayout();

            SuspendLayout();

            // =========================================================
            // FORM
            // =========================================================

            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 244, 238);
            ClientSize = new Size(1100, 650);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FormQuickReport";
            Text = "Báo cáo doanh thu";
            Load += FormQuickReport_Load;

            // =========================================================
            // HEADER
            // =========================================================

            pnlHeader.BackColor = Color.FromArgb(111, 78, 55);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(1100, 130);
            pnlHeader.TabIndex = 0;

            // TITLE

            lblTitle.AutoSize = true;
            lblTitle.Font = new Font(
                "Segoe UI",
                22F,
                FontStyle.Bold
            );
            lblTitle.ForeColor = Color.FromArgb(230, 194, 128);
            lblTitle.Location = new Point(35, 22);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(500, 41);
            lblTitle.Text = "BÁO CÁO DOANH THU";

            // DATE LABEL

            lblDate.AutoSize = true;
            lblDate.Font = new Font(
                "Segoe UI",
                10F,
                FontStyle.Regular
            );
            lblDate.ForeColor = Color.White;
            lblDate.Location = new Point(38, 82);
            lblDate.Name = "lblDate";
            lblDate.Text = "Ngày báo cáo:";

            // DATE PICKER

            dtpReportDate.Font = new Font(
                "Segoe UI",
                10F,
                FontStyle.Regular
            );
            dtpReportDate.Format = DateTimePickerFormat.Short;
            dtpReportDate.Location = new Point(140, 78);
            dtpReportDate.Name = "dtpReportDate";
            dtpReportDate.Size = new Size(145, 30);

            // RUN REPORT BUTTON

            btnRunReport.BackColor = Color.FromArgb(230, 194, 128);
            btnRunReport.FlatAppearance.BorderSize = 0;
            btnRunReport.FlatStyle = FlatStyle.Flat;
            btnRunReport.Font = new Font(
                "Segoe UI",
                10F,
                FontStyle.Bold
            );
            btnRunReport.ForeColor = Color.FromArgb(70, 45, 30);
            btnRunReport.Location = new Point(305, 76);
            btnRunReport.Name = "btnRunReport";
            btnRunReport.Size = new Size(145, 36);
            btnRunReport.Text = "Xem báo cáo";
            btnRunReport.UseVisualStyleBackColor = false;
            btnRunReport.Click += btnRunReport_Click;

            // ADD HEADER CONTROLS

            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Controls.Add(lblDate);
            pnlHeader.Controls.Add(dtpReportDate);
            pnlHeader.Controls.Add(btnRunReport);

            // =========================================================
            // PANEL 1 - TOTAL ORDERS
            // =========================================================

            pnlOrders.BackColor = Color.White;
            pnlOrders.BorderStyle = BorderStyle.FixedSingle;
            pnlOrders.Location = new Point(35, 170);
            pnlOrders.Name = "pnlOrders";
            pnlOrders.Size = new Size(315, 180);
            pnlOrders.TabIndex = 1;

            lblOrdersTitle.AutoSize = true;
            lblOrdersTitle.Font = new Font(
                "Segoe UI",
                11F,
                FontStyle.Bold
            );
            lblOrdersTitle.ForeColor = Color.FromArgb(111, 78, 55);
            lblOrdersTitle.Location = new Point(25, 22);
            lblOrdersTitle.Name = "lblOrdersTitle";
            lblOrdersTitle.Text = "TỔNG SỐ HÓA ĐƠN";

            lblTotalOrders.AutoSize = true;
            lblTotalOrders.Font = new Font(
                "Segoe UI",
                28F,
                FontStyle.Bold
            );
            lblTotalOrders.ForeColor = Color.FromArgb(111, 78, 55);
            lblTotalOrders.Location = new Point(25, 65);
            lblTotalOrders.Name = "lblTotalOrders";
            lblTotalOrders.Text = "0";

            pnlOrders.Controls.Add(lblOrdersTitle);
            pnlOrders.Controls.Add(lblTotalOrders);

            // =========================================================
            // PANEL 2 - TOTAL REVENUE
            // =========================================================

            pnlRevenue.BackColor = Color.White;
            pnlRevenue.BorderStyle = BorderStyle.FixedSingle;
            pnlRevenue.Location = new Point(390, 170);
            pnlRevenue.Name = "pnlRevenue";
            pnlRevenue.Size = new Size(315, 180);
            pnlRevenue.TabIndex = 2;

            lblRevenueTitle.AutoSize = true;
            lblRevenueTitle.Font = new Font(
                "Segoe UI",
                11F,
                FontStyle.Bold
            );
            lblRevenueTitle.ForeColor = Color.FromArgb(111, 78, 55);
            lblRevenueTitle.Location = new Point(25, 22);
            lblRevenueTitle.Name = "lblRevenueTitle";
            lblRevenueTitle.Text = "TỔNG DOANH THU";

            lblTotalRevenue.AutoSize = true;
            lblTotalRevenue.Font = new Font(
                "Segoe UI",
                24F,
                FontStyle.Bold
            );
            lblTotalRevenue.ForeColor = Color.FromArgb(111, 78, 55);
            lblTotalRevenue.Location = new Point(25, 68);
            lblTotalRevenue.Name = "lblTotalRevenue";
            lblTotalRevenue.Text = "0 VNĐ";

            pnlRevenue.Controls.Add(lblRevenueTitle);
            pnlRevenue.Controls.Add(lblTotalRevenue);

            // =========================================================
            // PANEL 3 - BEST SELLER
            // =========================================================

            pnlBestSeller.BackColor = Color.White;
            pnlBestSeller.BorderStyle = BorderStyle.FixedSingle;
            pnlBestSeller.Location = new Point(745, 170);
            pnlBestSeller.Name = "pnlBestSeller";
            pnlBestSeller.Size = new Size(315, 180);
            pnlBestSeller.TabIndex = 3;

            lblBestSellerTitle.AutoSize = true;
            lblBestSellerTitle.Font = new Font(
                "Segoe UI",
                11F,
                FontStyle.Bold
            );
            lblBestSellerTitle.ForeColor = Color.FromArgb(111, 78, 55);
            lblBestSellerTitle.Location = new Point(25, 22);
            lblBestSellerTitle.Name = "lblBestSellerTitle";
            lblBestSellerTitle.Text = "MẶT HÀNG BÁN CHẠY NHẤT";

            lblBestSeller.AutoSize = true;
            lblBestSeller.Font = new Font(
                "Segoe UI",
                18F,
                FontStyle.Bold
            );
            lblBestSeller.ForeColor = Color.FromArgb(111, 78, 55);
            lblBestSeller.Location = new Point(25, 72);
            lblBestSeller.Name = "lblBestSeller";
            lblBestSeller.Text = "Chưa có dữ liệu";

            pnlBestSeller.Controls.Add(lblBestSellerTitle);
            pnlBestSeller.Controls.Add(lblBestSeller);

            // =========================================================
            // ADD PANELS TO FORM
            // =========================================================

            Controls.Add(pnlBestSeller);
            Controls.Add(pnlRevenue);
            Controls.Add(pnlOrders);
            Controls.Add(pnlHeader);

            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();

            pnlOrders.ResumeLayout(false);
            pnlOrders.PerformLayout();

            pnlRevenue.ResumeLayout(false);
            pnlRevenue.PerformLayout();

            pnlBestSeller.ResumeLayout(false);
            pnlBestSeller.PerformLayout();

            ResumeLayout(false);
        }

        #endregion
    }
}