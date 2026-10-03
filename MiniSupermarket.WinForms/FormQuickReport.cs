namespace MiniSupermarket.WinForms
{
    public partial class FormQuickReport : Form
    {
        public FormQuickReport()
        {
            InitializeComponent();

            dtpReportDate.Value = DateTime.Now;

            lblTotalOrders.Text = "0";
            lblTotalRevenue.Text = "0 VNĐ";
            lblBestSeller.Text = "Chưa có dữ liệu";
        }

        private void btnRunReport_Click(object sender, EventArgs e)
        {
            lblTotalOrders.Text = "0";
            lblTotalRevenue.Text = "0 VNĐ";
            lblBestSeller.Text = "Chưa có dữ liệu";

            MessageBox.Show(
                $"Chưa có dữ liệu bán hàng cho ngày {dtpReportDate.Value:dd/MM/yyyy}.",
                "Báo cáo Happy Mart",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private void FormQuickReport_Load(object sender, EventArgs e)
        {
        }
    }
}