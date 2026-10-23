// ================================================================
// ********** THÔNG TIN SINH VIÊN **********
// HỌ TÊN : VO THI THUY AN
// LỚP    : CCQ2411C
// MSSV   : 2124110097
// MÔN    : LẬP TRÌNH ỨNG DỤNG
// NGÀY   : 26/09/2026
// ================================================================
// MÔ TẢ:
// File quản lý báo cáo doanh thu & kho hàng trên WinForms.
// Giao diện đã chuẩn hóa tone màu Dark Navy & Metallic Gold.
// ================================================================

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    public partial class FormQuickReport : Form
    {
        // BỘ MÀU CHUẨN ĐỒ ÁN HAPPY MART (DARK NAVY & METALLIC GOLD)
        private readonly Color primaryNavy = Color.FromArgb(11, 30, 54);     // #0B1E36 - Xanh Navy đậm chủ đạo
        private readonly Color accentGold = Color.FromArgb(212, 175, 55);    // #D4AF37 - Vàng Gold điểm nhấn
        private readonly Color creamBg = Color.FromArgb(244, 246, 249);      // #F4F6F9 - Nền sáng hiện đại
        private readonly Color textDark = Color.FromArgb(40, 40, 40);

        private Label lblOrdersValue;
        private Label lblRevenueValue;
        private Label lblProductsValue;
        private Label lblAverageValue;
        private Label lblStatus;

        //private DateTimePicker dtpReportDate;
        private Button btnReport;
        private Button btnExport;

        private DataGridView dgvBestSeller;
        private DataGridView dgvInvoices;
        private readonly List<(string Label, decimal Value)> chartData = new();
        private Panel pnlChart;
        private Label chartPlaceholder;

        public FormQuickReport()
        {
            BuildDashboard();
            Load += FormQuickReport_Load;
        }

        private void BuildDashboard()
        {
            SuspendLayout();

            Text = "Happy Mart POS - Báo Cáo Doanh Thu & Kho Hàng";
            Name = "FormQuickReport";
            BackColor = creamBg;
            ForeColor = primaryNavy;
            Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            MinimumSize = new Size(950, 650);
            Size = new Size(1120, 780);
            AutoScaleMode = AutoScaleMode.Font;
            StartPosition = FormStartPosition.CenterScreen;

            // ==========================================
            // 1. HEADER KHU VỰC TIÊU ĐỀ & BỘ LỌC NGÀY
            // ==========================================
            Panel header = new Panel
            {
                Dock = DockStyle.Top,
                Height = 135,
                BackColor = primaryNavy,
                Padding = new Padding(25, 15, 25, 10)
            };

            Label title = new Label
            {
                Text = "BÁO CÁO DOANH THU",
                Font = new Font("Segoe UI", 20F, FontStyle.Bold),
                ForeColor = accentGold,
                AutoSize = true,
                Location = new Point(22, 12)
            };

            Label subtitle = new Label
            {
                Text = "HAPPY MART  |  THỐNG KÊ HOẠT ĐỘNG BÁN HÀNG & KHO HÀNG",
                Font = new Font("Segoe UI", 8.5F, FontStyle.Regular),
                ForeColor = Color.FromArgb(180, 200, 220),
                AutoSize = true,
                Location = new Point(25, 52)
            };

            Label lblDate = new Label
            {
                Text = "Ngày báo cáo:",
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(25, 92),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            };

            dtpReportDate = new DateTimePicker
            {
                Format = DateTimePickerFormat.Short,
                Value = DateTime.Today,
                Width = 130,
                Location = new Point(125, 88),
                Font = new Font("Segoe UI", 9.5F)
            };

            btnReport = new Button
            {
                Text = "Xem báo cáo",
                BackColor = accentGold,
                ForeColor = primaryNavy,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Size = new Size(130, 32),
                Location = new Point(270, 86),
                Cursor = Cursors.Hand
            };
            btnReport.FlatAppearance.BorderSize = 0;
            btnReport.Click += btnRunReport_Click;

            btnExport = new Button
            {
                Text = "Xuất sản phẩm CSV",
                BackColor = Color.FromArgb(70, 160, 95),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Size = new Size(160, 32),
                Location = new Point(415, 86),
                Cursor = Cursors.Hand
            };
            btnExport.FlatAppearance.BorderSize = 0;
            btnExport.Click += btnExport_Click;

            header.Controls.AddRange(new Control[]
            {
                title, subtitle, lblDate, dtpReportDate, btnReport, btnExport
            });

            // ==========================================
            // 2. KHU VỰC NỘI DUNG CUỘN (BODY)
            // ==========================================
            FlowLayoutPanel body = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                WrapContents = false,
                FlowDirection = FlowDirection.TopDown,
                BackColor = creamBg,
                Padding = new Padding(20, 15, 20, 20)
            };

            // 4 THẺ THỐNG KÊ (KPI CARDS)
            FlowLayoutPanel cards = new FlowLayoutPanel
            {
                Width = 1040,
                Height = 115,
                WrapContents = false,
                Margin = new Padding(0, 0, 0, 15),
                BackColor = creamBg
            };

            Panel cardOrders = CreateCard("TỔNG SỐ HÓA ĐƠN", "0", out lblOrdersValue);
            Panel cardRevenue = CreateCard("TỔNG DOANH THU", "0 VNĐ", out lblRevenueValue);
            Panel cardProducts = CreateCard("SẢN PHẨM ĐÃ BÁN", "0", out lblProductsValue);
            Panel cardAverage = CreateCard("TRUNG BÌNH / HÓA ĐƠN", "0 VNĐ", out lblAverageValue);

            cards.Controls.AddRange(new Control[]
            {
                cardOrders, cardRevenue, cardProducts, cardAverage
            });

            // HÀNG GIỮA: BIỂU ĐỒ & SẢN PHẨM BÁN CHẠY
            TableLayoutPanel middle = new TableLayoutPanel
            {
                Width = 1040,
                Height = 280,
                ColumnCount = 2,
                RowCount = 1,
                Margin = new Padding(0, 0, 0, 15),
                BackColor = creamBg
            };

            middle.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 58F));
            middle.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 42F));
            middle.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            // Cột Biểu đồ doanh thu
            Panel chartCard = CreateSection("BIỂU ĐỒ DOANH THU");
            chartCard.Dock = DockStyle.Fill;
            chartCard.Margin = new Padding(0, 0, 8, 0);

            pnlChart = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(10)
            };

            chartPlaceholder = new Label
            {
                Text = "Đang tải dữ liệu biểu đồ...",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.Gray,
                Font = new Font("Segoe UI", 9.5F)
            };

            pnlChart.Controls.Add(chartPlaceholder);
            pnlChart.Paint += PnlChart_Paint;
            chartCard.Controls.Add(pnlChart);

            // Cột Sản phẩm bán chạy
            Panel bestSellerCard = CreateSection("SẢN PHẨM BÁN CHẠY");
            bestSellerCard.Dock = DockStyle.Fill;
            bestSellerCard.Margin = new Padding(8, 0, 0, 0);

            dgvBestSeller = CreateGrid();
            dgvBestSeller.Columns.Add("ProductName", "Tên sản phẩm");
            dgvBestSeller.Columns.Add("QuantitySold", "Đã bán");
            dgvBestSeller.Columns.Add("Revenue", "Doanh thu");
            dgvBestSeller.Columns["ProductName"].FillWeight = 130;
            dgvBestSeller.Columns["QuantitySold"].FillWeight = 60;
            dgvBestSeller.Columns["Revenue"].FillWeight = 90;

            bestSellerCard.Controls.Add(dgvBestSeller);

            middle.Controls.Add(chartCard, 0, 0);
            middle.Controls.Add(bestSellerCard, 1, 0);

            // BẢNG CHI TIẾT HÓA ĐƠN POS
            Panel invoiceCard = CreateSection("CHI TIẾT HÓA ĐƠN");
            invoiceCard.Width = 1040;
            invoiceCard.Height = 250;
            invoiceCard.Margin = new Padding(0, 0, 0, 10);

            dgvInvoices = CreateGrid();
            dgvInvoices.Columns.Add("OrderId", "Mã hóa đơn");
            dgvInvoices.Columns.Add("OrderDate", "Ngày bán");
            dgvInvoices.Columns.Add("Cashier", "Thu ngân");
            dgvInvoices.Columns.Add("Customer", "Khách hàng");
            dgvInvoices.Columns.Add("Total", "Tổng tiền");

            dgvInvoices.Columns["OrderId"].FillWeight = 85;
            dgvInvoices.Columns["OrderDate"].FillWeight = 110;
            dgvInvoices.Columns["Cashier"].FillWeight = 95;
            dgvInvoices.Columns["Customer"].FillWeight = 100;
            dgvInvoices.Columns["Total"].FillWeight = 100;

            invoiceCard.Controls.Add(dgvInvoices);

            // TRẠNG THÁI HỆ THỐNG
            lblStatus = new Label
            {
                Text = "Hệ thống POS Happy Mart sẵn sàng.",
                AutoSize = false,
                Width = 1040,
                Height = 30,
                ForeColor = primaryNavy,
                Font = new Font("Segoe UI", 9F, FontStyle.Italic),
                TextAlign = ContentAlignment.MiddleLeft,
                Margin = new Padding(0, 0, 0, 5)
            };

            body.Controls.Add(cards);
            body.Controls.Add(middle);
            body.Controls.Add(invoiceCard);
            body.Controls.Add(lblStatus);

            Controls.Add(body);
            Controls.Add(header);

            ResumeLayout(true);
        }

        private Panel CreateCard(string heading, string initialValue, out Label valueLabel)
        {
            Panel card = new Panel
            {
                Size = new Size(250, 105),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(0, 0, 13, 0),
                Padding = new Padding(12)
            };

            Panel accent = new Panel
            {
                Dock = DockStyle.Left,
                Width = 5,
                BackColor = accentGold
            };

            Label headingLabel = new Label
            {
                Text = heading,
                AutoSize = false,
                Location = new Point(15, 10),
                Size = new Size(225, 20),
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                ForeColor = primaryNavy
            };

            valueLabel = new Label
            {
                Text = initialValue,
                AutoSize = false,
                Location = new Point(15, 38),
                Size = new Size(225, 50),
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = primaryNavy,
                AutoEllipsis = true
            };

            card.Controls.Add(headingLabel);
            card.Controls.Add(valueLabel);
            card.Controls.Add(accent);

            return card;
        }

        private Panel CreateSection(string heading)
        {
            Panel section = new Panel
            {
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(10)
            };

            Label title = new Label
            {
                Text = heading,
                Dock = DockStyle.Top,
                Height = 32,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = primaryNavy,
                TextAlign = ContentAlignment.MiddleLeft
            };

            section.Controls.Add(title);
            return section;
        }

        private DataGridView CreateGrid()
        {
            return new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                EnableHeadersVisualStyles = false,
                ColumnHeadersHeight = 36,
                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = primaryNavy,
                    ForeColor = accentGold,
                    Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                    Alignment = DataGridViewContentAlignment.MiddleLeft,
                    SelectionBackColor = primaryNavy
                },
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Font = new Font("Segoe UI", 9F),
                    ForeColor = textDark,
                    BackColor = Color.White,
                    SelectionBackColor = Color.FromArgb(232, 240, 254),
                    SelectionForeColor = primaryNavy
                },
                AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.FromArgb(248, 249, 251),
                    SelectionBackColor = Color.FromArgb(232, 240, 254),
                    SelectionForeColor = primaryNavy
                },
                AutoGenerateColumns = false
            };
        }

        private void PnlChart_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            if (chartData == null || chartData.Count == 0)
            {
                chartPlaceholder.Visible = true;
                return;
            }

            chartPlaceholder.Visible = false;

            int padding = 20;
            int width = pnlChart.ClientSize.Width - (padding * 2);
            int height = pnlChart.ClientSize.Height - (padding * 2) - 30;

            if (width <= 0 || height <= 0) return;

            int count = chartData.Count;
            float maxColWidth = 60f;
            float colWidth = width / (float)count;
            if (colWidth > maxColWidth) colWidth = maxColWidth;

            decimal maxVal = 1m;
            foreach (var item in chartData)
            {
                if (item.Value > maxVal) maxVal = item.Value;
            }

            for (int i = 0; i < count; i++)
            {
                var item = chartData[i];
                float colH = (float)(item.Value / maxVal) * (height - 20);
                if (colH < 6 && item.Value > 0) colH = 6;

                float totalSlotWidth = width / (float)count;
                float startX = padding + (i * totalSlotWidth) + (totalSlotWidth - colWidth) / 2f;
                float y = padding + height - colH;

                using (Brush brush = new SolidBrush(primaryNavy))
                {
                    g.FillRectangle(brush, startX, y, colWidth, colH);
                }

                using (Pen pen = new Pen(accentGold, 3))
                {
                    g.DrawLine(pen, startX, y, startX + colWidth, y);
                }

                using (Font font = new Font("Segoe UI", 8F, FontStyle.Bold))
                using (Brush textBrush = new SolidBrush(primaryNavy))
                {
                    StringFormat sf = new StringFormat
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Near,
                        Trimming = StringTrimming.EllipsisCharacter
                    };
                    g.DrawString(item.Label, font, textBrush, new RectangleF(startX - 20, padding + height + 5, colWidth + 40, 25), sf);
                }
            }
        }

        private async void FormQuickReport_Load(object sender, EventArgs e)
        {
            await LoadReportAsync();
        }

        private async void btnRunReport_Click(object sender, EventArgs e)
        {
            await LoadReportAsync();
        }

        private async void btnExport_Click(object? sender, EventArgs e)
        {
            try
            {
                using var request = new HttpRequestMessage(HttpMethod.Get, "reports/products.csv");
                ApiClientService.ApplyToken(request);
                using HttpResponseMessage response = await ApiClientService.Client.SendAsync(request);

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    ApiClientService.ClearSession();
                    MessageBox.Show("Phiên đăng nhập đã hết hạn. Vui lòng đăng nhập lại.", "Happy Mart", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Không thể xuất danh sách sản phẩm từ API.", "Happy Mart", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using var dialog = new SaveFileDialog
                {
                    Filter = "CSV files (*.csv)|*.csv",
                    FileName = $"happymart-products-{DateTime.Today:yyyyMMdd}.csv"
                };

                if (dialog.ShowDialog(this) != DialogResult.OK)
                    return;

                await File.WriteAllBytesAsync(dialog.FileName, await response.Content.ReadAsByteArrayAsync());
                MessageBox.Show("Đã xuất báo cáo sản phẩm thành công.", "Happy Mart", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (HttpRequestException)
            {
                MessageBox.Show("Không thể kết nối API để xuất báo cáo.", "Happy Mart", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async Task LoadReportAsync()
        {
            btnReport.Enabled = false;
            btnReport.Text = "Đang tải...";

            lblOrdersValue.Text = "Đang tải...";
            lblRevenueValue.Text = "Đang tải...";
            lblProductsValue.Text = "Đang tải...";
            lblAverageValue.Text = "Đang tải...";
            lblStatus.Text = "Đang kết nối API báo cáo...";

            dgvInvoices.Rows.Clear();
            dgvBestSeller.Rows.Clear();
            chartData.Clear();
            pnlChart.Invalidate();

            try
            {
                string date = dtpReportDate.Value.ToString("yyyy-MM-dd");
                using var request = new HttpRequestMessage(
                    HttpMethod.Get,
                    $"reports/daily?date={Uri.EscapeDataString(date)}");
                ApiClientService.ApplyToken(request);

                using HttpResponseMessage response = await ApiClientService.Client.SendAsync(request);
                string json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    ApiClientService.ClearSession();
                    throw new UnauthorizedAccessException("Phiên làm việc hết hạn hoặc chưa đăng nhập.");
                }

                response.EnsureSuccessStatusCode();

                using JsonDocument document = JsonDocument.Parse(json);
                JsonElement root = document.RootElement;

                if (root.TryGetProperty("success", out JsonElement success) && success.ValueKind == JsonValueKind.False)
                {
                    string message = root.TryGetProperty("message", out JsonElement errorMessage)
                        ? errorMessage.GetString() ?? "API báo cáo thất bại."
                        : "API báo cáo thất bại.";
                    throw new Exception(message);
                }

                CultureInfo vi = CultureInfo.GetCultureInfo("vi-VN");

                int totalOrders = root.GetProperty("totalOrders").GetInt32();
                decimal totalRevenue = root.GetProperty("totalRevenue").GetDecimal();

                lblOrdersValue.Text = totalOrders.ToString("N0", vi);
                lblRevenueValue.Text = totalRevenue.ToString("N0", vi) + " VNĐ";

                decimal average = totalOrders > 0 ? totalRevenue / totalOrders : 0m;
                lblAverageValue.Text = average.ToString("N0", vi) + " VNĐ";

                int totalProductsSold = 0;
                JsonElement productSales = root.GetProperty("productSales");

                foreach (JsonElement product in productSales.EnumerateArray())
                {
                    string productName = product.GetProperty("productName").GetString() ?? "Sản phẩm";
                    int quantity = product.GetProperty("quantitySold").GetInt32();
                    decimal revenue = product.GetProperty("revenue").GetDecimal();

                    totalProductsSold += quantity;

                    dgvBestSeller.Rows.Add(
                        productName,
                        quantity.ToString("N0", vi),
                        revenue.ToString("N0", vi) + " VNĐ"
                    );

                    string shortName = productName.Length > 12 ? productName.Substring(0, 10) + "..." : productName;
                    chartData.Add((shortName, revenue));
                }

                lblProductsValue.Text = totalProductsSold.ToString("N0", vi);

                JsonElement orders = root.GetProperty("orders");

                foreach (JsonElement order in orders.EnumerateArray())
                {
                    string orderCode = order.GetProperty("orderCode").ValueKind == JsonValueKind.Null
                        ? order.GetProperty("orderId").ToString()
                        : order.GetProperty("orderCode").GetString() ?? order.GetProperty("orderId").ToString();

                    string orderDate = order.GetProperty("createdDate").GetDateTime().ToString("dd/MM/yyyy HH:mm");

                    string cashier = order.GetProperty("cashierUsername").ValueKind == JsonValueKind.Null
                        ? "—"
                        : order.GetProperty("cashierUsername").GetString() ?? "—";

                    decimal finalAmount = order.GetProperty("finalAmount").GetDecimal();

                    dgvInvoices.Rows.Add(
                        orderCode,
                        orderDate,
                        cashier,
                        "Khách lẻ",
                        finalAmount.ToString("N0", vi) + " VNĐ"
                    );
                }

                if (chartData.Count == 0)
                {
                    chartData.Add((date, totalRevenue));
                }

                pnlChart.Invalidate();

                lblStatus.Text = $"Đã tải báo cáo ngày {dtpReportDate.Value:dd/MM/yyyy}: {totalOrders} hóa đơn, xuất kho {totalProductsSold} sản phẩm.";
            }
            catch (UnauthorizedAccessException ex)
            {
                lblOrdersValue.Text = "—";
                lblRevenueValue.Text = "—";
                lblProductsValue.Text = "—";
                lblAverageValue.Text = "—";
                lblStatus.Text = ex.Message;
                MessageBox.Show(ex.Message, "Phiên đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                lblOrdersValue.Text = "—";
                lblRevenueValue.Text = "—";
                lblProductsValue.Text = "—";
                lblAverageValue.Text = "—";

                dgvInvoices.Rows.Clear();
                dgvBestSeller.Rows.Clear();
                chartData.Clear();
                pnlChart.Invalidate();

                lblStatus.Text = "Không thể tải báo cáo.";

                MessageBox.Show(
                    "Không thể tải báo cáo doanh thu.\n\nChi tiết: " + ex.Message,
                    "Happy Mart POS System",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            finally
            {
                btnReport.Enabled = true;
                btnReport.Text = "Xem báo cáo";
            }
        }
    }
}