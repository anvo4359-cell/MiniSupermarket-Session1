// ================================================================
// ********** THÔNG TIN SINH VIÊN **********
// HỌ TÊN : VO THI THUY AN
// LỚP    : CCQ2411C
// MSSV   : 2124110097
// MÔN    : LẬP TRÌNH ỨNG DỤNG
// NGÀY   : 26/09/2026
// ================================================================
// MÔ TẢ:
// File quản lý giao diện khách hàng (Customer) trên WinForms.
// Kết nối Web API bằng HttpClient + JWT.
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
    public partial class FormCustomerManagement : Form
    {
        // ============================================================
        // KẾT NỐI WEB API
        // ============================================================

        private static readonly HttpClient _client = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5037/api/")
        };

        private System.Windows.Forms.Timer _searchTimer; // Timer chống gọi API liên tục khi gõ tìm kiếm
        private int _currentPage = 1;
        private const int PageSize = 5;
        private int _totalPages = 1;
        private int _totalItems;
        private Button _btnPrev = null!;
        private Button _btnNext = null!;
        private Label _lblPaging = null!;

        // ============================================================
        // CONSTRUCTOR
        // ============================================================

        public FormCustomerManagement()
        {
            InitializeComponent();
            InitializePagingControls();

            // Gắn sự kiện cho các nút
            btnAdd.Click -= btnAdd_Click;
            btnAdd.Click += btnAdd_Click;

            btnUpdate.Click -= btnUpdate_Click;
            btnUpdate.Click += btnUpdate_Click;

            btnDelete.Click -= btnDelete_Click;
            btnDelete.Click += btnDelete_Click;

            btnClear.Click -= btnClear_Click;
            btnClear.Click += btnClear_Click;

            btnLoad.Click -= btnLoad_Click;
            btnLoad.Click += btnLoad_Click;

            btnSearch.Click -= btnSearch_Click;
            btnSearch.Click += btnSearch_Click;

            // Click dòng trong DataGridView
            dgvCustomers.CellClick -= dgvCustomers_CellClick;
            dgvCustomers.CellClick += dgvCustomers_CellClick;

            // Khi Form được mở
            this.Load -= FormCustomerManagement_Load;
            this.Load += FormCustomerManagement_Load;

            // Đăng ký sự kiện tự động tìm kiếm khi gõ phím vào ô txtKeyword
            if (txtKeyword != null)
            {
                txtKeyword.TextChanged -= txtKeyword_TextChanged;
                txtKeyword.TextChanged += txtKeyword_TextChanged;
            }

            // Khởi tạo Timer debounce tìm kiếm (300ms)
            _searchTimer = new System.Windows.Forms.Timer();
            _searchTimer.Interval = 300;
            _searchTimer.Tick += async (s, e) =>
            {
                _searchTimer.Stop();
                await PerformSearchAsync();
            };
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

        private async void FormCustomerManagement_Load(
            object sender,
            EventArgs e)
        {
            await LoadDataAsync();
        }

        // ============================================================
        // LẤY DANH SÁCH CUSTOMER
        // GET /api/customers
        // ============================================================

        private async Task LoadDataAsync()
        {
            try
            {
                SetJwtToken();
                var result = await _client.GetFromJsonAsync<PagedResponseDto<CustomerDto>>(
                    $"customers/paged?pageNumber={_currentPage}&pageSize={PageSize}");
                if (result == null) return;
                _totalItems = result.TotalItems;
                _totalPages = Math.Max(1, result.TotalPages);
                if (_currentPage > _totalPages) { _currentPage = _totalPages; await LoadDataAsync(); return; }

                // Bật tự động sinh cột và liên kết danh sách DTO
                dgvCustomers.AutoGenerateColumns = true;
                dgvCustomers.DataSource = result.Items;

                SetColumnHeaders();
                UpdatePagingControls();
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(
                    "Không thể kết nối đến Web API!\n\n" +
                    ex.Message,
                    "Lỗi kết nối",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Có lỗi xảy ra!\n\n" +
                    ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // ============================================================
        // ĐẶT TÊN CỘT
        // ============================================================

        private void SetColumnHeaders()
        {
            if (dgvCustomers.Columns["CustomerId"] != null)
            {
                dgvCustomers.Columns["CustomerId"].HeaderText = "Mã KH";
                dgvCustomers.Columns["CustomerId"].FillWeight = 50;
            }

            if (dgvCustomers.Columns["CustomerName"] != null)
            {
                dgvCustomers.Columns["CustomerName"].HeaderText = "Tên khách hàng";
                dgvCustomers.Columns["CustomerName"].FillWeight = 150;
            }

            if (dgvCustomers.Columns["Phone"] != null)
            {
                dgvCustomers.Columns["Phone"].HeaderText = "Số điện thoại";
                dgvCustomers.Columns["Phone"].FillWeight = 90;
            }

            if (dgvCustomers.Columns["Address"] != null)
            {
                dgvCustomers.Columns["Address"].HeaderText = "Địa chỉ";
                dgvCustomers.Columns["Address"].FillWeight = 200;
            }

            if (dgvCustomers.Columns["Email"] != null)
            {
                dgvCustomers.Columns["Email"].HeaderText = "Email";
                dgvCustomers.Columns["Email"].FillWeight = 180;
            }
        }

        // ============================================================
        // NÚT TẢI LẠI
        // ============================================================

        private async void btnLoad_Click(
            object sender,
            EventArgs e)
        {
            txtKeyword.Clear();
            await LoadDataAsync();
        }

        // ============================================================
        // CLICK DÒNG TRONG DATAGRIDVIEW
        // ============================================================

        private void dgvCustomers_CellClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            try
            {
                DataGridViewRow row =
                    dgvCustomers.Rows[e.RowIndex];

                txtId.Text =
                    row.Cells["CustomerId"]?.Value?.ToString() ?? "";

                txtCustomerName.Text =
                    row.Cells["CustomerName"]?.Value?.ToString() ?? "";

                txtPhone.Text =
                    row.Cells["Phone"]?.Value?.ToString() ?? "";

                txtAddress.Text =
                    row.Cells["Address"]?.Value?.ToString() ?? "";

                txtEmail.Text =
                    row.Cells["Email"]?.Value?.ToString() ?? "";
            }
            catch
            {
                // Không làm gì nếu click dòng không hợp lệ
            }
        }

        // ============================================================
        // THÊM CUSTOMER
        // POST /api/customers
        // ============================================================

        private async void btnAdd_Click(
            object sender,
            EventArgs e)
        {
            if (!ValidateInput())
                return;

            try
            {
                SetJwtToken();

                var newCustomer = new
                {
                    CustomerName = txtCustomerName.Text.Trim(),
                    Phone = txtPhone.Text.Trim(),
                    Address = txtAddress.Text.Trim(),
                    Email = txtEmail.Text.Trim()
                };

                var response =
                    await _client.PostAsJsonAsync(
                        "customers",
                        newCustomer
                    );

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        "Thêm khách hàng thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    await LoadDataAsync();
                    ClearInputs();
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
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
                        "Thêm khách hàng thất bại!\n\n" +
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
        // CẬP NHẬT CUSTOMER
        // PUT /api/customers/{id}
        // ============================================================

        private async void btnUpdate_Click(
            object sender,
            EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtId.Text))
            {
                MessageBox.Show(
                    "Vui lòng chọn khách hàng cần sửa từ bảng!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            if (!int.TryParse(txtId.Text, out int id))
            {
                MessageBox.Show(
                    "Mã ID không hợp lệ!",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            if (!ValidateInput())
                return;

            try
            {
                SetJwtToken();

                var updateCustomer = new
                {
                    CustomerId = id,
                    CustomerName = txtCustomerName.Text.Trim(),
                    Phone = txtPhone.Text.Trim(),
                    Address = txtAddress.Text.Trim(),
                    Email = txtEmail.Text.Trim()
                };

                var response =
                    await _client.PutAsJsonAsync(
                        $"customers/{id}",
                        updateCustomer
                    );

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        "Cập nhật khách hàng thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    await LoadDataAsync();
                    ClearInputs();
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
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
        // XÓA CUSTOMER
        // DELETE /api/customers/{id}
        // ============================================================

        private async void btnDelete_Click(
            object sender,
            EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtId.Text))
            {
                MessageBox.Show(
                    "Vui lòng chọn khách hàng cần xóa!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            if (!int.TryParse(txtId.Text, out int id))
            {
                MessageBox.Show(
                    "Mã ID không hợp lệ!",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            DialogResult confirm = MessageBox.Show(
                $"Bạn có chắc muốn xóa khách hàng ID = {id} không?",
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
                        $"customers/{id}"
                    );

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        "Xóa khách hàng thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    await LoadDataAsync();
                    ClearInputs();
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
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
                        "Xóa thất bại!\n\n" +
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
        // TÌM KIẾM CUSTOMER
        // ============================================================

        private async void btnSearch_Click(
            object sender,
            EventArgs e)
        {
            _searchTimer.Stop();
            await PerformSearchAsync();
        }

        // Tự động tìm kiếm ngay khi gõ phím vào ô txtKeyword
        private void txtKeyword_TextChanged(object sender, EventArgs e)
        {
            _searchTimer.Stop();
            _searchTimer.Start();
        }

        // Hàm chung gọi API tìm kiếm
        private async Task PerformSearchAsync()
        {
            try
            {
                SetJwtToken();
                string keyword = txtKeyword.Text.Trim();
                _currentPage = 1;
                string url = $"customers/paged?pageNumber=1&pageSize={PageSize}&keyword={Uri.EscapeDataString(keyword)}";
                var paged = await _client.GetFromJsonAsync<PagedResponseDto<CustomerDto>>(url);
                if (paged == null) return;
                _totalItems = paged.TotalItems;
                _totalPages = Math.Max(1, paged.TotalPages);

                dgvCustomers.AutoGenerateColumns = true;
                dgvCustomers.DataSource = paged.Items;

                SetColumnHeaders();
                UpdatePagingControls();
            }
            catch
            {
                // Bỏ qua lỗi ngầm khi đang gõ phím nhanh liên tục
            }
        }

        private void InitializePagingControls()
        {
            Panel panel = new() { Height = 42, Dock = DockStyle.Bottom, BackColor = Color.White };
            _btnPrev = new Button { Text = "◀ Trang trước", Width = 110, Height = 28, Left = 8, Top = 7 };
            _btnNext = new Button { Text = "Trang sau ▶", Width = 110, Height = 28, Top = 7 };
            _lblPaging = new Label { TextAlign = ContentAlignment.MiddleCenter, Top = 7, Height = 28 };
            _btnPrev.Click += async (_, _) => { if (_currentPage > 1) { _currentPage--; await LoadDataAsync(); } };
            _btnNext.Click += async (_, _) => { if (_currentPage < _totalPages) { _currentPage++; await LoadDataAsync(); } };
            panel.Controls.AddRange(new Control[] { _btnPrev, _btnNext, _lblPaging });
            panel.Resize += (_, _) => { _btnNext.Left = panel.Width - _btnNext.Width - 8; _lblPaging.Left = _btnPrev.Right + 8; _lblPaging.Width = _btnNext.Left - _lblPaging.Left - 8; };
            dgvCustomers.Parent?.Controls.Add(panel);
            panel.BringToFront();
        }

        private void UpdatePagingControls()
        {
            if (_lblPaging == null) return;
            _lblPaging.Text = $"Trang {_currentPage}/{_totalPages} - Tổng {_totalItems} khách hàng";
            _btnPrev.Enabled = _currentPage > 1;
            _btnNext.Enabled = _currentPage < _totalPages;
        }

        // ============================================================
        // XÓA TRẮNG Ô NHẬP (LÀM MỚI)
        // ============================================================

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearInputs();
            txtKeyword.Clear();
        }

        private void ClearInputs()
        {
            txtId.Text = "";
            txtCustomerName.Text = "";
            txtPhone.Text = "";
            txtAddress.Text = "";
            txtEmail.Text = "";
            txtCustomerName.Focus();
        }

        // ============================================================
        // KIỂM TRA DỮ LIỆU NHẬP
        // ============================================================

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtCustomerName.Text))
            {
                MessageBox.Show(
                    "Vui lòng nhập tên khách hàng!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                txtCustomerName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                MessageBox.Show(
                    "Vui lòng nhập số điện thoại!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                txtPhone.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                MessageBox.Show(
                    "Vui lòng nhập địa chỉ!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                txtAddress.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show(
                    "Vui lòng nhập email!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                txtEmail.Focus();
                return false;
            }

            return true;
        }

        private void dgvCustomers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }

    // ================================================================
    // DTO NHẬN DỮ LIỆU TỪ WEB API
    // ================================================================

    public class CustomerDto
    {
        public int CustomerId { get; set; }

        public string CustomerName { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
        public int RewardPoints { get; set; }
    }

    public class PagedResponseDto<T>
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public List<T> Items { get; set; } = new();
    }
}