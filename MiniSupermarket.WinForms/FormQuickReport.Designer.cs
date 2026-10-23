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
    partial class FormQuickReport
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubTitle;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.DateTimePicker dtpReportDate;
        private System.Windows.Forms.Button btnRunReport;

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
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubTitle = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.dtpReportDate = new System.Windows.Forms.DateTimePicker();
            this.btnRunReport = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(11, 30, 54);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.lblSubTitle);
            this.pnlHeader.Controls.Add(this.lblDate);
            this.pnlHeader.Controls.Add(this.dtpReportDate);
            this.pnlHeader.Controls.Add(this.btnRunReport);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1120, 130);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(212, 175, 55);
            this.lblTitle.Location = new System.Drawing.Point(22, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(262, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "BÁO CÁO DOANH THU";
            // 
            // lblSubTitle
            // 
            this.lblSubTitle.AutoSize = true;
            this.lblSubTitle.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblSubTitle.ForeColor = System.Drawing.Color.FromArgb(180, 200, 220);
            this.lblSubTitle.Location = new System.Drawing.Point(25, 48);
            this.lblSubTitle.Name = "lblSubTitle";
            this.lblSubTitle.Size = new System.Drawing.Size(260, 15);
            this.lblSubTitle.TabIndex = 1;
            this.lblSubTitle.Text = "HAPPY MART  |  THỐNG KÊ HOẠT ĐỘNG BÁN HÀNG";
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblDate.ForeColor = System.Drawing.Color.White;
            this.lblDate.Location = new System.Drawing.Point(25, 87);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(95, 17);
            this.lblDate.TabIndex = 2;
            this.lblDate.Text = "Ngày báo cáo:";
            // 
            // dtpReportDate
            // 
            this.dtpReportDate.CustomFormat = "dd/MM/yyyy";
            this.dtpReportDate.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpReportDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpReportDate.Location = new System.Drawing.Point(130, 83);
            this.dtpReportDate.Name = "dtpReportDate";
            this.dtpReportDate.Size = new System.Drawing.Size(145, 25);
            this.dtpReportDate.TabIndex = 3;
            // 
            // btnRunReport
            // 
            this.btnRunReport.BackColor = System.Drawing.Color.FromArgb(212, 175, 55);
            this.btnRunReport.FlatAppearance.BorderSize = 0;
            this.btnRunReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRunReport.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnRunReport.ForeColor = System.Drawing.Color.FromArgb(11, 30, 54);
            this.btnRunReport.Location = new System.Drawing.Point(290, 80);
            this.btnRunReport.Name = "btnRunReport";
            this.btnRunReport.Size = new System.Drawing.Size(130, 32);
            this.btnRunReport.TabIndex = 4;
            this.btnRunReport.Text = "Xem báo cáo";
            this.btnRunReport.UseVisualStyleBackColor = false;
            this.btnRunReport.Click += new System.EventHandler(this.btnRunReport_Click);
            // 
            // FormQuickReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(244, 246, 249);
            this.ClientSize = new System.Drawing.Size(1120, 780);
            this.ControlBox = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormQuickReport";
            this.Text = "Happy Mart POS - Báo Cáo Doanh Thu & Kho Hàng";
            this.Load += new System.EventHandler(this.FormQuickReport_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
    }
}