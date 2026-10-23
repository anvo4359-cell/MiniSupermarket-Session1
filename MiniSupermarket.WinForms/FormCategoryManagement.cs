// ================================================================
// ********** THÔNG TIN SINH VIÊN **********
// HỌ TÊN : VO THI THUY AN
// LỚP    : CCQ2411C
// MSSV   : 2124110097
// MÔN    : LẬP TRÌNH ỨNG DỤNG
// NGÀY   : 26/09/2026
// ================================================================
// MÔ TẢ:
// File quản lý giao diện nhóm hàng (Category) trên ứng dụng WinForms.
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
    public partial class FormCategoryManagement : Form
    {
        // ============================================================
        // KẾT NỐI WEB API
        // ============================================================

        private static readonly HttpClient _client = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5037/api/")
        };

        private System.Windows.Forms.Timer _searchTimer; // Timer chống gọi API liên tục khi đang gõ tìm kiếm
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

        public FormCategoryManagement()
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

            // Sự kiện click dòng trong DataGridView
            dgvCategories.CellClick -= dgvCategories_CellClick;
            dgvCategories.CellClick += dgvCategories_CellClick;

            // Sự kiện khi Form được mở
            this.Load -= FormCategoryManagement_Load;
            this.Load += FormCategoryManagement_Load;

            // Sự kiện tự động tìm kiếm khi gõ vào ô txtKeyword
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

        private async void FormCategoryManagement_Load(
            object sender,
            EventArgs e)
        {
            await LoadDataAsync();
        }

        // ============================================================
        // LẤY DANH SÁCH CATEGORY
        // GET /api/categories
        // ============================================================

        private async Task LoadDataAsync()
        {
            try
            {
                SetJwtToken();
                var result = await _client.GetFromJsonAsync<PagedResponseDto<CategoryDto>>(
                    $"categories/paged?pageNumber={_currentPage}&pageSize={PageSize}");
                if (result == null) return;
                _totalItems = result.TotalItems;
                _totalPages = Math.Max(1, result.TotalPages);
                if (_currentPage > _totalPages) { _currentPage = _totalPages; await LoadDataAsync(); return; }

                // Tự động sinh cột và gán dữ liệu từ DTO
                dgvCategories.AutoGenerateColumns = true;
                dgvCategories.DataSource = result.Items;

                SetCategoryColumnHeaders();
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
        // CẤU HÌNH CỘT BẢNG
        // ============================================================

        private void SetCategoryColumnHeaders()
        {
            if (dgvCategories.Columns["CategoryId"] != null)
            {
                dgvCategories.Columns["CategoryId"].HeaderText = "Mã ID";
                dgvCategories.Columns["CategoryId"].FillWeight = 50;
            }

            if (dgvCategories.Columns["CategoryName"] != null)
            {
                dgvCategories.Columns["CategoryName"].HeaderText = "Tên Nhóm hàng";
                dgvCategories.Columns["CategoryName"].FillWeight = 150;
            }

            if (dgvCategories.Columns["Description"] != null)
            {
                dgvCategories.Columns["Description"].HeaderText = "Mô Tả";
                dgvCategories.Columns["Description"].FillWeight = 250;
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
        // CLICK VÀO DÒNG TRÊN BẢNG
        // ============================================================

        private void dgvCategories_CellClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            try
            {
                DataGridViewRow row =
                    dgvCategories.Rows[e.RowIndex];

                txtId.Text =
                    row.Cells["CategoryId"]?.Value?.ToString() ?? "";

                txtCategoryName.Text =
                    row.Cells["CategoryName"]?.Value?.ToString() ?? "";

                txtDescription.Text =
                    row.Cells["Description"]?.Value?.ToString() ?? "";
            }
            catch
            {
                // Không làm gì nếu click vào dòng không hợp lệ
            }
        }

        // ============================================================
        // THÊM MỚI
        // POST /api/categories
        // ============================================================

        private async void btnAdd_Click(
            object sender,
            EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCategoryName.Text))
            {
                MessageBox.Show(
                    "Vui lòng nhập tên nhóm hàng!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                txtCategoryName.Focus();
                return;
            }

            try
            {
                SetJwtToken();

                var newCat = new
                {
                    CategoryName = txtCategoryName.Text.Trim(),
                    Description = txtDescription.Text.Trim()
                };

                var response =
                    await _client.PostAsJsonAsync(
                        "categories",
                        newCat
                    );

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        "Thêm mới nhóm hàng thành công!",
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
                    MessageBox.Show(
                        "Thêm mới thất bại!\n\n" +
                        $"Mã lỗi: {(int)response.StatusCode}",
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
        // CẬP NHẬT
        // PUT /api/categories/{id}
        // ============================================================

        private async void btnUpdate_Click(
            object sender,
            EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtId.Text))
            {
                MessageBox.Show(
                    "Vui lòng chọn nhóm hàng cần sửa từ bảng!",
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

            if (string.IsNullOrWhiteSpace(txtCategoryName.Text))
            {
                MessageBox.Show(
                    "Vui lòng nhập tên nhóm hàng!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                txtCategoryName.Focus();
                return;
            }

            try
            {
                SetJwtToken();

                var updateCat = new
                {
                    CategoryId = id,
                    CategoryName = txtCategoryName.Text.Trim(),
                    Description = txtDescription.Text.Trim()
                };

                var response =
                    await _client.PutAsJsonAsync(
                        $"categories/{id}",
                        updateCat
                    );

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        "Cập nhật thành công!",
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
                    MessageBox.Show(
                        "Cập nhật thất bại!\n\n" +
                        $"Mã lỗi: {(int)response.StatusCode}",
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
        // XÓA
        // DELETE /api/categories/{id}
        // ============================================================

        private async void btnDelete_Click(
            object sender,
            EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtId.Text))
            {
                MessageBox.Show(
                    "Vui lòng chọn nhóm hàng cần xóa!",
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
                $"Bạn có chắc muốn xóa nhóm hàng ID = {id} không?",
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
                        $"categories/{id}"
                    );

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        "Xóa thành công!",
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
                    MessageBox.Show(
                        "Xóa thất bại!\n\n" +
                        $"Mã lỗi: {(int)response.StatusCode}",
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
        // ============================================================

        private async void btnSearch_Click(
            object sender,
            EventArgs e)
        {
            _searchTimer.Stop();
            await PerformSearchAsync();
        }

        // Tự động tìm kiếm ngay khi gõ phím
        private void txtKeyword_TextChanged(object sender, EventArgs e)
        {
            _searchTimer.Stop();
            _searchTimer.Start();
        }

        // Hàm chung xử lý gọi API tìm kiếm
        private async Task PerformSearchAsync()
        {
            try
            {
                SetJwtToken();
                string keyword = txtKeyword.Text.Trim();
                _currentPage = 1;
                string url = $"categories/paged?pageNumber=1&pageSize={PageSize}&keyword={Uri.EscapeDataString(keyword)}";
                var result = await _client.GetFromJsonAsync<PagedResponseDto<CategoryDto>>(url);
                if (result == null) return;
                _totalItems = result.TotalItems;
                _totalPages = Math.Max(1, result.TotalPages);

                dgvCategories.AutoGenerateColumns = true;
                dgvCategories.DataSource = result.Items;

                SetCategoryColumnHeaders();
                UpdatePagingControls();
            }
            catch
            {
                // Bỏ qua lỗi ngầm trong lúc đang gõ phím liên tục
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
            dgvCategories.Parent?.Controls.Add(panel);
            panel.BringToFront();
        }

        private void UpdatePagingControls()
        {
            if (_lblPaging == null) return;
            _lblPaging.Text = $"Trang {_currentPage}/{_totalPages} - Tổng {_totalItems} nhóm hàng";
            _btnPrev.Enabled = _currentPage > 1;
            _btnNext.Enabled = _currentPage < _totalPages;
        }

        // ============================================================
        // XÓA TRẮNG Ô NHẬP
        // ============================================================

        private void btnClear_Click(
            object sender,
            EventArgs e)
        {
            ClearInputs();
            txtKeyword.Clear();
        }

        private void ClearInputs()
        {
            txtId.Text = "";
            txtCategoryName.Text = "";
            txtDescription.Text = "";
            txtCategoryName.Focus();
        }

        private void txtCategoryName_TextChanged(object sender, EventArgs e)
        {

        }
    }

    // ================================================================
    // DTO NHẬN DỮ LIỆU TỪ WEB API
    // ================================================================

    public class CategoryDto
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; } = string.Empty;

        public string? Description { get; set; }
    }
}