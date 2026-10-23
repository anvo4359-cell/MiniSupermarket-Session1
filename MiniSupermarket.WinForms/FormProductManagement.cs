using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace MiniSupermarket.WinForms
{
    public partial class FormProductManagement : Form
    {
        private static readonly HttpClient _client = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5037/api/")
        };

        private readonly JsonSerializerOptions _jsonOptions =
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

        private List<CategoryDto> _categories = new List<CategoryDto>();

        private int _currentPage = 1;
        private int _pageSize = 5;
        private int _totalPages = 1;
        private int _totalItems;
        private string _currentKeyword = string.Empty;

        private bool _isLoadingProducts;
        private bool _isChangingKeyword;

        private Panel _pagingPanel;
        private Button _btnPrev;
        private Button _btnNext;
        private Label _lblPaging;

        public FormProductManagement()
        {
            InitializeComponent();

            InitializePagingControls();

            Load -= FormProductManagement_Load;
            Load += FormProductManagement_Load;

            dgvProducts.CellClick -= dgvProducts_CellClick;
            dgvProducts.CellClick += dgvProducts_CellClick;
            dgvProducts.CellFormatting -= dgvProducts_CellFormatting;
            dgvProducts.CellFormatting += dgvProducts_CellFormatting;

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

            txtKeyword.TextChanged -= txtKeyword_TextChanged;
            txtKeyword.TextChanged += txtKeyword_TextChanged;
        }

        private void dgvProducts_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvProducts.Rows.Count ||
                !dgvProducts.Columns.Contains("StockQuantity"))
                return;

            object? value = dgvProducts.Rows[e.RowIndex].Cells["StockQuantity"].Value;
            if (value is not null && int.TryParse(value.ToString(), out int stock) && stock <= 10)
            {
                e.CellStyle.BackColor = stock == 0 ? Color.LightCoral : Color.Moccasin;
                e.CellStyle.ForeColor = Color.DarkRed;
            }
        }

        #region Phân trang

        private void InitializePagingControls()
        {
            _pagingPanel = new Panel
            {
                Name = "pnlPaging",
                Height = 46,
                Dock = DockStyle.Bottom,
                Padding = new Padding(8),
                BackColor = Color.White
            };

            _btnPrev = new Button
            {
                Name = "btnPrev",
                Text = "◀ Trang trước",
                Width = 110,
                Height = 30,
                Left = 8,
                Top = 7,
                Enabled = false,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(52, 73, 94),
                ForeColor = Color.White,
                Cursor = Cursors.Hand
            };

            _btnNext = new Button
            {
                Name = "btnNext",
                Text = "Trang sau ▶",
                Width = 110,
                Height = 30,
                Top = 7,
                Enabled = false,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(52, 73, 94),
                ForeColor = Color.White,
                Cursor = Cursors.Hand
            };

            _lblPaging = new Label
            {
                Name = "lblPaging",
                Text = "Trang 1/1 - Tổng 0 sản phẩm",
                AutoSize = false,
                Height = 30,
                Top = 7,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.FromArgb(70, 70, 70)
            };

            _btnPrev.Click += btnPrev_Click;
            _btnNext.Click += btnNext_Click;

            _pagingPanel.Controls.Add(_btnPrev);
            _pagingPanel.Controls.Add(_lblPaging);
            _pagingPanel.Controls.Add(_btnNext);
            _pagingPanel.Resize += (sender, e) => ArrangePagingControls();

            // Giữ nguyên dgvProducts trong đúng parent do Designer tạo.
            // Chỉ thêm thanh phân trang vào parent hiện tại.
            Control parent = dgvProducts.Parent;
            if (parent != null)
            {
                parent.SuspendLayout();

                if (!parent.Controls.Contains(_pagingPanel))
                    parent.Controls.Add(_pagingPanel);

                _pagingPanel.Dock = DockStyle.Bottom;
                parent.Controls.SetChildIndex(_pagingPanel, 0);

                parent.ResumeLayout(true);
                parent.PerformLayout();
            }

            ArrangePagingControls();
            UpdatePagingControls();
        }

        private void ArrangePagingControls()
        {
            if (_pagingPanel == null || _btnPrev == null ||
                _btnNext == null || _lblPaging == null)
                return;

            int panelWidth = _pagingPanel.ClientSize.Width;

            _btnPrev.Left = 8;
            _btnNext.Left = Math.Max(8, panelWidth - _btnNext.Width - 8);
            _lblPaging.Left = _btnPrev.Right + 8;
            _lblPaging.Width = Math.Max(50, _btnNext.Left - _lblPaging.Left - 8);
        }

        private void UpdatePagingControls()
        {
            if (_btnPrev == null || _btnNext == null || _lblPaging == null)
                return;

            _totalPages = Math.Max(1, _totalPages);
            _currentPage = Math.Max(1, _currentPage);

            _btnPrev.Enabled = !_isLoadingProducts && _currentPage > 1;
            _btnNext.Enabled = !_isLoadingProducts && _currentPage < _totalPages;
            _lblPaging.Text =
                $"Trang {_currentPage}/{_totalPages} - Tổng {_totalItems} sản phẩm";
        }

        private async void btnPrev_Click(object sender, EventArgs e)
        {
            if (_isLoadingProducts || _currentPage <= 1)
                return;

            _currentPage--;
            await LoadProductsAsync();
        }

        private async void btnNext_Click(object sender, EventArgs e)
        {
            if (_isLoadingProducts || _currentPage >= _totalPages)
                return;

            _currentPage++;
            await LoadProductsAsync();
        }

        #endregion

        #region Kết nối API

        private void SetJwtToken()
        {
            string token = SessionManager.JwtToken ?? string.Empty;
            _client.DefaultRequestHeaders.Authorization =
                string.IsNullOrWhiteSpace(token)
                    ? null
                    : new AuthenticationHeaderValue("Bearer", token);
        }

        private async void FormProductManagement_Load(object sender, EventArgs e)
        {
            await LoadCategoriesAsync();
            await LoadProductsAsync();
        }

        private async Task LoadCategoriesAsync()
        {
            try
            {
                SetJwtToken();

                using HttpResponseMessage response = await _client.GetAsync("categories");
                string json = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                    throw new Exception($"API trả về lỗi {(int)response.StatusCode}.\n{json}");

                _categories =
                    JsonSerializer.Deserialize<List<CategoryDto>>(json, _jsonOptions)
                    ?? new List<CategoryDto>();

                cboCategory.DataSource = null;
                cboCategory.Items.Clear();
                cboCategory.DisplayMember = nameof(CategoryDto.CategoryName);
                cboCategory.ValueMember = nameof(CategoryDto.CategoryId);

                if (_categories.Count == 0)
                {
                    cboCategory.Enabled = false;
                    MessageBox.Show(
                        "API chưa có nhóm hàng. Hãy kiểm tra GET /api/categories.",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                cboCategory.Enabled = true;
                cboCategory.DataSource = _categories;
                cboCategory.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                cboCategory.DataSource = null;
                cboCategory.Items.Clear();
                cboCategory.Enabled = false;

                MessageBox.Show(
                    "Không thể tải danh mục!\n\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadProductsAsync()
        {
            if (_isLoadingProducts)
                return;

            _isLoadingProducts = true;
            UpdatePagingControls();

            try
            {
                SetJwtToken();

                string url =
                    $"products/paged?pageNumber={_currentPage}&pageSize={_pageSize}";

                if (!string.IsNullOrWhiteSpace(_currentKeyword))
                    url += "&keyword=" + Uri.EscapeDataString(_currentKeyword);

                using HttpResponseMessage response = await _client.GetAsync(url);
                string json = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                    throw new Exception($"API trả về lỗi {(int)response.StatusCode}.\n{json}");

                PagedResultDto<ProductDto>? result =
                    JsonSerializer.Deserialize<PagedResultDto<ProductDto>>(json, _jsonOptions);

                if (result == null)
                    throw new Exception("Không đọc được dữ liệu phân trang từ API.");

                _pageSize = result.PageSize > 0 ? result.PageSize : 5;
                _totalItems = Math.Max(0, result.TotalItems);
                _totalPages = Math.Max(1, result.TotalPages);

                // Nếu trang được yêu cầu không còn hợp lệ, yêu cầu lại trang cuối.
                int apiPage = Math.Max(1, result.CurrentPage);
                if (apiPage > _totalPages)
                {
                    _currentPage = _totalPages;
                    url = $"products/paged?pageNumber={_currentPage}&pageSize={_pageSize}";
                    if (!string.IsNullOrWhiteSpace(_currentKeyword))
                        url += "&keyword=" + Uri.EscapeDataString(_currentKeyword);

                    using HttpResponseMessage retryResponse = await _client.GetAsync(url);
                    string retryJson = await retryResponse.Content.ReadAsStringAsync();

                    if (!retryResponse.IsSuccessStatusCode)
                        throw new Exception(
                            $"API trả về lỗi {(int)retryResponse.StatusCode}.\n{retryJson}");

                    result = JsonSerializer.Deserialize<PagedResultDto<ProductDto>>(
                        retryJson, _jsonOptions);

                    if (result == null)
                        throw new Exception("Không đọc được dữ liệu trang hợp lệ từ API.");

                    _totalItems = Math.Max(0, result.TotalItems);
                    _totalPages = Math.Max(1, result.TotalPages);
                    _pageSize = result.PageSize > 0 ? result.PageSize : _pageSize;
                }
                else
                {
                    _currentPage = apiPage;
                }

                // Gán lại dữ liệu vào bảng hiện có, không di chuyển hoặc tạo lại bảng.
                dgvProducts.AutoGenerateColumns = true;
                dgvProducts.DataSource = null;
                dgvProducts.DataSource = result.Items ?? new List<ProductDto>();

                SetProductColumnHeaders();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể tải danh sách sản phẩm!\n\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _isLoadingProducts = false;
                UpdatePagingControls();
            }
        }

        #endregion

        #region Cấu hình bảng sản phẩm

        private void SetProductColumnHeaders()
        {
            if (dgvProducts.Columns["ProductId"] != null)
            {
                dgvProducts.Columns["ProductId"].HeaderText = "Mã ID";
                dgvProducts.Columns["ProductId"].FillWeight = 50;
            }

            if (dgvProducts.Columns["Barcode"] != null)
            {
                dgvProducts.Columns["Barcode"].HeaderText = "Mã vạch";
                dgvProducts.Columns["Barcode"].FillWeight = 100;
            }

            if (dgvProducts.Columns["ProductName"] != null)
            {
                dgvProducts.Columns["ProductName"].HeaderText = "Tên sản phẩm";
                dgvProducts.Columns["ProductName"].FillWeight = 180;
            }

            if (dgvProducts.Columns["Price"] != null)
            {
                dgvProducts.Columns["Price"].HeaderText = "Đơn giá (VNĐ)";
                dgvProducts.Columns["Price"].FillWeight = 90;
                dgvProducts.Columns["Price"].DefaultCellStyle.Format = "N0";
            }

            if (dgvProducts.Columns["StockQuantity"] != null)
            {
                dgvProducts.Columns["StockQuantity"].HeaderText = "Tồn kho";
                dgvProducts.Columns["StockQuantity"].FillWeight = 70;
            }

            if (dgvProducts.Columns["CategoryId"] != null)
            {
                dgvProducts.Columns["CategoryId"].HeaderText = "Mã danh mục";
                dgvProducts.Columns["CategoryId"].FillWeight = 80;
            }

            if (dgvProducts.Columns["Category"] != null)
                dgvProducts.Columns["Category"].Visible = false;

            if (dgvProducts.Columns["CategoryName"] != null)
                dgvProducts.Columns["CategoryName"].Visible = false;

            dgvProducts.ReadOnly = true;
            dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProducts.MultiSelect = false;
            dgvProducts.AllowUserToAddRows = false;
            dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void dgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvProducts.Rows.Count)
                return;

            DataGridViewRow row = dgvProducts.Rows[e.RowIndex];

            txtId.Text = GetCellText(row, "ProductId");
            txtBarcode.Text = GetCellText(row, "Barcode");
            txtProductName.Text = GetCellText(row, "ProductName");
            txtPrice.Text = GetCellText(row, "Price");
            txtStockQuantity.Text = GetCellText(row, "StockQuantity");

            string categoryText = GetCellText(row, "CategoryId");
            if (int.TryParse(categoryText, out int categoryId))
                cboCategory.SelectedValue = categoryId;
        }

        private string GetCellText(DataGridViewRow row, string columnName)
        {
            if (!dgvProducts.Columns.Contains(columnName))
                return string.Empty;

            object value = row.Cells[columnName].Value;
            if (value == null || value == DBNull.Value)
                return string.Empty;

            return value.ToString() ?? string.Empty;
        }

        #endregion

        #region Thêm sản phẩm

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInput(out decimal price, out int stock))
                return;

            if (cboCategory.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn nhóm hàng!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int categoryId = Convert.ToInt32(cboCategory.SelectedValue);

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

                using HttpResponseMessage response =
                    await _client.PostAsJsonAsync("products", newProduct);

                if (!response.IsSuccessStatusCode)
                {
                    string error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show("Thêm sản phẩm thất bại!\n\n" + error,
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show("Thêm sản phẩm thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClearInputs();
                _currentPage = 1;
                await LoadProductsAsync();
            }
            catch (Exception ex)
            {
                ShowApiError(ex);
            }
        }

        #endregion

        #region Cập nhật sản phẩm

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtId.Text, out int id))
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateInput(out decimal price, out int stock))
                return;

            if (cboCategory.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn nhóm hàng!");
                return;
            }

            int categoryId = Convert.ToInt32(cboCategory.SelectedValue);

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

                using HttpResponseMessage response =
                    await _client.PutAsJsonAsync($"products/{id}", updateProduct);

                if (!response.IsSuccessStatusCode)
                {
                    string error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show("Cập nhật thất bại!\n\n" + error,
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show("Cập nhật sản phẩm thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClearInputs();
                await LoadProductsAsync();
            }
            catch (Exception ex)
            {
                ShowApiError(ex);
            }
        }

        #endregion

        #region Xóa sản phẩm

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtId.Text, out int id))
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                $"Bạn có chắc muốn xóa sản phẩm ID = {id}?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
                return;

            try
            {
                SetJwtToken();

                using HttpResponseMessage response = await _client.DeleteAsync($"products/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    string error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show("Xóa sản phẩm thất bại!\n\n" + error,
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show("Xóa sản phẩm thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClearInputs();

                if (_currentPage > 1 && dgvProducts.Rows.Count <= 1)
                    _currentPage--;

                await LoadProductsAsync();
            }
            catch (Exception ex)
            {
                ShowApiError(ex);
            }
        }

        #endregion

        #region Tìm kiếm và tải lại

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            await PerformSearchAsync();
        }

        private async void txtKeyword_TextChanged(object sender, EventArgs e)
        {
            if (_isChangingKeyword)
                return;

            await PerformSearchAsync();
        }

        private async Task PerformSearchAsync()
        {
            if (_isLoadingProducts)
                return;

            _currentKeyword = txtKeyword.Text.Trim();
            _currentPage = 1;
            await LoadProductsAsync();
        }

        private async void btnLoad_Click(object sender, EventArgs e)
        {
            _isChangingKeyword = true;
            txtKeyword.Clear();
            _isChangingKeyword = false;

            _currentKeyword = string.Empty;
            _currentPage = 1;

            await LoadCategoriesAsync();
            await LoadProductsAsync();
        }

        private async void btnClear_Click(object sender, EventArgs e)
        {
            ClearInputs();

            _isChangingKeyword = true;
            txtKeyword.Clear();
            _isChangingKeyword = false;

            _currentKeyword = string.Empty;
            _currentPage = 1;

            await LoadProductsAsync();
        }

        #endregion

        #region Kiểm tra dữ liệu và tiện ích

        private void ClearInputs()
        {
            txtId.Clear();
            txtBarcode.Clear();
            txtProductName.Clear();
            txtPrice.Clear();
            txtStockQuantity.Clear();

            if (cboCategory.Items.Count > 0)
                cboCategory.SelectedIndex = 0;

            txtBarcode.Focus();
        }

        private bool ValidateInput(out decimal price, out int stock)
        {
            price = 0;
            stock = 0;

            if (string.IsNullOrWhiteSpace(txtBarcode.Text))
            {
                MessageBox.Show("Vui lòng nhập mã vạch!");
                txtBarcode.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtProductName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên sản phẩm!");
                txtProductName.Focus();
                return false;
            }

            string priceText = txtPrice.Text.Trim();

            bool validPrice = decimal.TryParse(
                priceText, NumberStyles.Number, CultureInfo.CurrentCulture, out price);

            if (!validPrice)
            {
                validPrice = decimal.TryParse(
                    priceText, NumberStyles.Number, CultureInfo.InvariantCulture, out price);
            }

            if (!validPrice || price < 0)
            {
                MessageBox.Show("Giá sản phẩm phải là số không âm!");
                txtPrice.Focus();
                return false;
            }

            if (!int.TryParse(txtStockQuantity.Text.Trim(), out stock) || stock < 0)
            {
                MessageBox.Show("Tồn kho phải là số nguyên không âm!");
                txtStockQuantity.Focus();
                return false;
            }

            return true;
        }

        private void ShowApiError(Exception ex)
        {
            MessageBox.Show(
                "Có lỗi khi kết nối hoặc xử lý API!\n\n" + ex.Message,
                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // Giữ lại vì Designer có thể đang đăng ký sự kiện này.
        private void pnlProductCard_Paint(object sender, PaintEventArgs e)
        {
        }

        // Giữ lại vì Designer có thể đang đăng ký sự kiện này.
        private void dgvProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        #endregion
    }

    public class ProductDto
    {
        public int ProductId { get; set; }
        public string Barcode { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public CategoryDto? Category { get; set; }
    }

    public class PagedResultDto<T>
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public List<T> Items { get; set; } = new List<T>();
    }
}
