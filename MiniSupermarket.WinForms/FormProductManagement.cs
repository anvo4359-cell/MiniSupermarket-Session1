
// ================================================================
// ********** THÔNG TIN SINH VIÊN **********
// HỌ TÊN : VO THI THUY AN
// LỚP    : CCQ2411C
// MSSV   : 2124110097
// MÔN    : LẬP TRÌNH ỨNG DỤNG
// NGÀY   : 26/09/2026
// ================================================================
// MÔ TẢ:
// File xử lý chức năng quản lý sản phẩm (Product) trên WinForms.
//
// Chức năng:
// - Hiển thị danh sách sản phẩm
// - Tìm kiếm sản phẩm
// - Thêm sản phẩm
// - Sửa sản phẩm
// - Xóa sản phẩm
// - Chọn nhóm hàng bằng ComboBox
// - Sử dụng JWT Token khi gọi Web API
//
// LƯU Ý:
// Giao diện được tạo trong:
// FormProductManagement.Designer.cs
// ================================================================

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    public partial class FormProductManagement : Form
    {
        // ============================================================
        // KẾT NỐI WEB API
        // ============================================================

        private static readonly HttpClient _client = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5037/api/")
        };

        // ============================================================
        // DANH SÁCH CATEGORY
        // ============================================================

        private List<CategoryDto> _categories = new List<CategoryDto>();

        // ============================================================
        // CONSTRUCTOR
        // ============================================================

        public FormProductManagement()
        {
            InitializeComponent();

            // Form Load
            this.Load -= FormProductManagement_Load;
            this.Load += FormProductManagement_Load;

            // DataGridView
            dgvProducts.CellClick -= dgvProducts_CellClick;
            dgvProducts.CellClick += dgvProducts_CellClick;

            // Button
            btnAdd.Click -= btnAdd_Click;
            btnAdd.Click += btnAdd_Click;

            btnUpdate.Click -= btnUpdate_Click;
            btnUpdate.Click += btnUpdate_Click;

            btnDelete.Click -= btnDelete_Click;
            btnDelete.Click += btnDelete_Click;

            btnClear.Click -= btnClear_Click;
            btnClear.Click += btnClear_Click;

            btnSearch.Click -= btnSearch_Click;
            btnSearch.Click += btnSearch_Click;

            btnLoad.Click -= btnLoad_Click;
            btnLoad.Click += btnLoad_Click;
        }

        // ============================================================
        // CẤU HÌNH JWT TOKEN
        // ============================================================

        private void SetJwtToken()
        {
            if (!string.IsNullOrWhiteSpace(SessionManager.JwtToken))
            {
                _client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(
                        "Bearer",
                        SessionManager.JwtToken
                    );
            }
            else
            {
                _client.DefaultRequestHeaders.Authorization = null;
            }
        }

        // ============================================================
        // FORM LOAD
        // ============================================================

        private async void FormProductManagement_Load(
            object sender,
            EventArgs e)
        {
            await LoadCategoriesAsync();
            await LoadProductsAsync();
        }

        // ============================================================
        // LOAD CATEGORY
        // GET /api/categories
        // ============================================================

        private async Task LoadCategoriesAsync()
        {
            try
            {
                SetJwtToken();

                var categories =
                    await _client.GetFromJsonAsync<List<CategoryDto>>(
                        "categories"
                    );

                _categories =
                    categories ?? new List<CategoryDto>();

                cboCategory.DataSource = null;

                if (_categories.Count > 0)
                {
                    cboCategory.DataSource = _categories;
                    cboCategory.DisplayMember = "CategoryName";
                    cboCategory.ValueMember = "CategoryId";
                    cboCategory.SelectedIndex = 0;
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(
                    "Không thể tải danh sách nhóm hàng!\n\n" +
                    ex.Message,
                    "Lỗi kết nối",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Có lỗi khi tải nhóm hàng!\n\n" +
                    ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // ============================================================
        // LOAD PRODUCT
        // GET /api/products
        // ============================================================

        private async Task LoadProductsAsync()
        {
            try
            {
                SetJwtToken();

                var products =
                    await _client.GetFromJsonAsync<List<ProductDto>>(
                        "products"
                    );

                dgvProducts.DataSource = products;

                SetProductColumnHeaders();
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(
                    "Không thể tải danh sách sản phẩm!\n\n" +
                    ex.Message,
                    "Lỗi kết nối",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Có lỗi khi tải sản phẩm!\n\n" +
                    ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // ============================================================
        // ĐẶT TÊN CỘT DATAGRIDVIEW
        // ============================================================

        private void SetProductColumnHeaders()
        {
            if (dgvProducts.Columns["ProductId"] != null)
            {
                dgvProducts.Columns["ProductId"].HeaderText = "Mã ID";
            }

            if (dgvProducts.Columns["Barcode"] != null)
            {
                dgvProducts.Columns["Barcode"].HeaderText = "Mã vạch";
            }

            if (dgvProducts.Columns["ProductName"] != null)
            {
                dgvProducts.Columns["ProductName"].HeaderText =
                    "Tên sản phẩm";
            }

            if (dgvProducts.Columns["Price"] != null)
            {
                dgvProducts.Columns["Price"].HeaderText = "Giá";
            }

            if (dgvProducts.Columns["StockQuantity"] != null)
            {
                dgvProducts.Columns["StockQuantity"].HeaderText =
                    "Tồn kho";
            }

            if (dgvProducts.Columns["CategoryId"] != null)
            {
                dgvProducts.Columns["CategoryId"].HeaderText =
                    "Mã nhóm hàng";
            }

            // Không hiển thị object Category trong bảng
            if (dgvProducts.Columns["Category"] != null)
            {
                dgvProducts.Columns["Category"].Visible = false;
            }
        }

        // ============================================================
        // NÚT TẢI LẠI
        // ============================================================

        private async void btnLoad_Click(
            object sender,
            EventArgs e)
        {
            await LoadCategoriesAsync();
            await LoadProductsAsync();
        }

        // ============================================================
        // CLICK VÀO DÒNG SẢN PHẨM
        // ============================================================

        private void dgvProducts_CellClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            try
            {
                DataGridViewRow row =
                    dgvProducts.Rows[e.RowIndex];

                // Mã ID
                txtId.Text =
                    row.Cells["ProductId"]?.Value?.ToString() ?? "";

                // Mã vạch
                txtBarcode.Text =
                    row.Cells["Barcode"]?.Value?.ToString() ?? "";

                // Tên sản phẩm
                txtProductName.Text =
                    row.Cells["ProductName"]?.Value?.ToString() ?? "";

                // Giá
                txtPrice.Text =
                    row.Cells["Price"]?.Value?.ToString() ?? "";

                // Tồn kho
                txtStockQuantity.Text =
                    row.Cells["StockQuantity"]?.Value?.ToString() ?? "";

                // Nhóm hàng
                object? categoryValue =
                    row.Cells["CategoryId"]?.Value;

                if (categoryValue != null &&
                    int.TryParse(
                        categoryValue.ToString(),
                        out int categoryId))
                {
                    try
                    {
                        cboCategory.SelectedValue = categoryId;
                    }
                    catch
                    {
                        // Bỏ qua nếu category không tồn tại
                    }
                }
            }
            catch
            {
                // Bỏ qua nếu dòng không hợp lệ
            }
        }

        // ============================================================
        // THÊM SẢN PHẨM
        // POST /api/products
        // ============================================================

        private async void btnAdd_Click(
            object sender,
            EventArgs e)
        {
            if (!ValidateInput(
                    out decimal price,
                    out int stock))
            {
                return;
            }

            if (cboCategory.SelectedValue == null)
            {
                MessageBox.Show(
                    "Vui lòng chọn nhóm hàng!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            int categoryId;

            try
            {
                categoryId =
                    Convert.ToInt32(
                        cboCategory.SelectedValue
                    );
            }
            catch
            {
                MessageBox.Show(
                    "Nhóm hàng không hợp lệ!",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            try
            {
                SetJwtToken();

                var newProduct = new
                {
                    Barcode = txtBarcode.Text.Trim(),
                    ProductName = txtProductName.Text.Trim(),
                    Price = price,
                    StockQuantity = stock,
                    CategoryId = categoryId
                };

                var response =
                    await _client.PostAsJsonAsync(
                        "products",
                        newProduct
                    );

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        "Thêm sản phẩm thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    await LoadProductsAsync();
                    ClearInputs();
                }
                else if (
                    response.StatusCode ==
                    System.Net.HttpStatusCode.Unauthorized)
                {
                    MessageBox.Show(
                        "Phiên đăng nhập đã hết hạn hoặc JWT Token không hợp lệ!",
                        "401 - Unauthorized",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
                else
                {
                    string error =
                        await response.Content.ReadAsStringAsync();

                    MessageBox.Show(
                        "Thêm sản phẩm thất bại!\n\n" +
                        $"Mã lỗi: {(int)response.StatusCode}\n" +
                        error,
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi kết nối API!\n\n" +
                    ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // ============================================================
        // SỬA SẢN PHẨM
        // PUT /api/products/{id}
        // ============================================================

        private async void btnUpdate_Click(
            object sender,
            EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtId.Text))
            {
                MessageBox.Show(
                    "Vui lòng chọn sản phẩm cần sửa!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            if (!int.TryParse(
                    txtId.Text,
                    out int id))
            {
                MessageBox.Show(
                    "Mã ID không hợp lệ!",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            if (!ValidateInput(
                    out decimal price,
                    out int stock))
            {
                return;
            }

            if (cboCategory.SelectedValue == null)
            {
                MessageBox.Show(
                    "Vui lòng chọn nhóm hàng!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            int categoryId;

            try
            {
                categoryId =
                    Convert.ToInt32(
                        cboCategory.SelectedValue
                    );
            }
            catch
            {
                MessageBox.Show(
                    "Nhóm hàng không hợp lệ!",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            try
            {
                SetJwtToken();

                var updateProduct = new
                {
                    ProductId = id,
                    Barcode = txtBarcode.Text.Trim(),
                    ProductName = txtProductName.Text.Trim(),
                    Price = price,
                    StockQuantity = stock,
                    CategoryId = categoryId
                };

                var response =
                    await _client.PutAsJsonAsync(
                        $"products/{id}",
                        updateProduct
                    );

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        "Cập nhật sản phẩm thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    await LoadProductsAsync();
                    ClearInputs();
                }
                else if (
                    response.StatusCode ==
                    System.Net.HttpStatusCode.Unauthorized)
                {
                    MessageBox.Show(
                        "Phiên đăng nhập đã hết hạn hoặc JWT Token không hợp lệ!",
                        "401 - Unauthorized",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
                else
                {
                    string error =
                        await response.Content.ReadAsStringAsync();

                    MessageBox.Show(
                        "Cập nhật thất bại!\n\n" +
                        $"Mã lỗi: {(int)response.StatusCode}\n" +
                        error,
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi kết nối API!\n\n" +
                    ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // ============================================================
        // XÓA SẢN PHẨM
        // DELETE /api/products/{id}
        // ============================================================

        private async void btnDelete_Click(
            object sender,
            EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtId.Text))
            {
                MessageBox.Show(
                    "Vui lòng chọn sản phẩm cần xóa!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            if (!int.TryParse(
                    txtId.Text,
                    out int id))
            {
                MessageBox.Show(
                    "Mã ID không hợp lệ!",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            DialogResult confirm =
                MessageBox.Show(
                    $"Bạn có chắc muốn xóa sản phẩm ID = {id}?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

            if (confirm != DialogResult.Yes)
                return;

            try
            {
                SetJwtToken();

                var response =
                    await _client.DeleteAsync(
                        $"products/{id}"
                    );

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        "Xóa sản phẩm thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    await LoadProductsAsync();
                    ClearInputs();
                }
                else if (
                    response.StatusCode ==
                    System.Net.HttpStatusCode.Unauthorized)
                {
                    MessageBox.Show(
                        "Phiên đăng nhập đã hết hạn hoặc JWT Token không hợp lệ!",
                        "401 - Unauthorized",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
                else
                {
                    string error =
                        await response.Content.ReadAsStringAsync();

                    MessageBox.Show(
                        "Xóa sản phẩm thất bại!\n\n" +
                        $"Mã lỗi: {(int)response.StatusCode}\n" +
                        error,
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi kết nối API!\n\n" +
                    ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // ============================================================
        // TÌM KIẾM
        // GET /api/products/search?keyword=...
        // ============================================================

        private async void btnSearch_Click(
            object sender,
            EventArgs e)
        {
            string keyword =
                txtKeyword.Text.Trim();

            // Không nhập từ khóa -> tải toàn bộ
            if (string.IsNullOrWhiteSpace(keyword))
            {
                await LoadProductsAsync();
                return;
            }

            try
            {
                SetJwtToken();

                string url =
                    "products/search?keyword=" +
                    Uri.EscapeDataString(keyword);

                var products =
                    await _client.GetFromJsonAsync<List<ProductDto>>(
                        url
                    );

                dgvProducts.DataSource = products;

                SetProductColumnHeaders();

                if (products == null ||
                    products.Count == 0)
                {
                    MessageBox.Show(
                        "Không tìm thấy sản phẩm phù hợp!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(
                    "Lỗi tìm kiếm!\n\n" +
                    ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi tìm kiếm!\n\n" +
                    ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // ============================================================
        // XÓA TRẮNG
        // ============================================================

        private void btnClear_Click(
            object sender,
            EventArgs e)
        {
            ClearInputs();
        }

        private void ClearInputs()
        {
            txtId.Text = "";
            txtBarcode.Text = "";
            txtProductName.Text = "";
            txtPrice.Text = "";
            txtStockQuantity.Text = "";

            if (cboCategory.Items.Count > 0)
            {
                cboCategory.SelectedIndex = 0;
            }

            txtBarcode.Focus();
        }

        // ============================================================
        // KIỂM TRA DỮ LIỆU
        // ============================================================

        private bool ValidateInput(
            out decimal price,
            out int stock)
        {
            price = 0;
            stock = 0;

            // ----------------------------
            // Mã vạch
            // ----------------------------

            if (string.IsNullOrWhiteSpace(
                    txtBarcode.Text))
            {
                MessageBox.Show(
                    "Vui lòng nhập mã vạch!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                txtBarcode.Focus();
                return false;
            }

            // ----------------------------
            // Tên sản phẩm
            // ----------------------------

            if (string.IsNullOrWhiteSpace(
                    txtProductName.Text))
            {
                MessageBox.Show(
                    "Vui lòng nhập tên sản phẩm!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                txtProductName.Focus();
                return false;
            }

            // ----------------------------
            // Giá
            // ----------------------------

            if (!decimal.TryParse(
                    txtPrice.Text.Trim(),
                    out price))
            {
                MessageBox.Show(
                    "Giá sản phẩm phải là số!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                txtPrice.Focus();
                return false;
            }

            if (price < 0)
            {
                MessageBox.Show(
                    "Giá sản phẩm không được âm!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                txtPrice.Focus();
                return false;
            }

            // ----------------------------
            // Tồn kho
            // ----------------------------

            if (!int.TryParse(
                    txtStockQuantity.Text.Trim(),
                    out stock))
            {
                MessageBox.Show(
                    "Số lượng tồn kho phải là số nguyên!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                txtStockQuantity.Focus();
                return false;
            }

            if (stock < 0)
            {
                MessageBox.Show(
                    "Số lượng tồn kho không được âm!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                txtStockQuantity.Focus();
                return false;
            }

            return true;
        }

        private void groupProductInfo_Enter(object sender, EventArgs e)
        {

        }
    }

    // ================================================================
    // DTO PRODUCT
    // ================================================================

    public class ProductDto
    {
        public int ProductId { get; set; }

        public string Barcode { get; set; } = string.Empty;

        public string ProductName { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int StockQuantity { get; set; }

        public int CategoryId { get; set; }

        public CategoryDto? Category { get; set; }
    }
}

