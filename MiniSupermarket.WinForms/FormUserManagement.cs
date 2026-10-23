// ================================================================
// ********** THÔNG TIN SINH VIÊN **********
// HỌ TÊN : VO THI THUY AN
// LỚP    : CCQ2411C
// MSSV   : 2124110097
// MÔN    : LẬP TRÌNH ỨNG DỤNG
// NGÀY   : 26/09/2026
// ================================================================

using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace MiniSupermarket.WinForms
{
    public partial class FormUserManagement : Form
    {
        private static readonly HttpClient _client = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5037/api/")
        };

        private int _currentPage = 1;
        private const int PageSize = 5;
        private int _totalPages = 1;
        private int _totalItems;
        private Button _btnPrev = null!;
        private Button _btnNext = null!;
        private Label _lblPaging = null!;

        public FormUserManagement()
        {
            InitializeComponent();
            InitializePagingControls();
        }

        private async void FormUserManagement_Load(object sender, EventArgs e)
        {
            await LoadUsersAsync();
        }

        private async Task LoadUsersAsync()
        {
            try
            {
                SetToken();

                var response = await _client.GetAsync(
                    $"User/paged?pageNumber={_currentPage}&pageSize={PageSize}");

                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        "Không thể tải danh sách tài khoản!\n\n" +
                        $"Server trả về: {response.StatusCode}",
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    return;
                }

                string json = await response.Content.ReadAsStringAsync();

                var result = JsonSerializer.Deserialize<PagedResponseDto<UserDto>>(
                    json,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }
                );

                if (result == null) return;
                _totalItems = result.TotalItems;
                _totalPages = Math.Max(1, result.TotalPages);
                if (_currentPage > _totalPages) { _currentPage = _totalPages; await LoadUsersAsync(); return; }
                dgvUsers.DataSource = result.Items;
                UpdatePagingControls();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi kết nối Web API:\n\n" + ex.Message,
                    "Lỗi hệ thống",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void SetToken()
        {
            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(
                    "Bearer",
                    SessionManager.JwtToken
                );
        }

        private void dgvUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row = dgvUsers.Rows[e.RowIndex];

            txtUserId.Text = row.Cells["UserId"].Value?.ToString() ?? "";
            txtUsername.Text = row.Cells["Username"].Value?.ToString() ?? "";
            txtPassword.Clear();
            cboRole.Text = row.Cells["Role"].Value?.ToString() ?? "";
            txtFullName.Text = row.Cells["FullName"].Value?.ToString() ?? "";
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            try
            {
                SetToken();

                var user = new
                {
                    Username = txtUsername.Text.Trim(),
                    Password = txtPassword.Text.Trim(),
                    Role = cboRole.Text.Trim(),
                    FullName = txtFullName.Text.Trim()
                };

                var response = await _client.PostAsJsonAsync("User", user);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        "Thêm tài khoản thành công!",
                        "Happy Mart",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    ClearInput();
                    await LoadUsersAsync();
                }
                else
                {
                    string error = await response.Content.ReadAsStringAsync();

                    MessageBox.Show(
                        $"Không thể thêm tài khoản!\n\n{error}",
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi:\n\n" + ex.Message,
                    "Lỗi hệ thống",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtUserId.Text, out int userId))
            {
                MessageBox.Show(
                    "Vui lòng chọn tài khoản cần sửa!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            if (!ValidateInput())
                return;

            try
            {
                SetToken();

                var user = new
                {
                    UserId = userId,
                    Username = txtUsername.Text.Trim(),
                    Password = txtPassword.Text.Trim(),
                    Role = cboRole.Text.Trim(),
                    FullName = txtFullName.Text.Trim()
                };

                var response = await _client.PutAsJsonAsync($"User/{userId}", user);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        "Cập nhật tài khoản thành công!",
                        "Happy Mart",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    ClearInput();
                    await LoadUsersAsync();
                }
                else
                {
                    string error = await response.Content.ReadAsStringAsync();

                    MessageBox.Show(
                        $"Không thể cập nhật!\n\n{error}",
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi:\n\n" + ex.Message,
                    "Lỗi hệ thống",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtUserId.Text, out int userId))
            {
                MessageBox.Show(
                    "Vui lòng chọn tài khoản cần xóa!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa tài khoản này?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result != DialogResult.Yes)
                return;

            try
            {
                SetToken();

                var response = await _client.DeleteAsync($"User/{userId}");

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        "Xóa tài khoản thành công!",
                        "Happy Mart",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    ClearInput();
                    await LoadUsersAsync();
                }
                else
                {
                    string error = await response.Content.ReadAsStringAsync();

                    MessageBox.Show(
                        $"Không thể xóa tài khoản!\n\n{error}",
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi:\n\n" + ex.Message,
                    "Lỗi hệ thống",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private async void btnClear_Click(object sender, EventArgs e)
        {
            ClearInput();
            txtSearch.Clear();
            await LoadUsersAsync();
        }

        // Sự kiện khi bấm nút Tìm kiếm thủ công
        private async void btnSearch_Click(object sender, EventArgs e)
        {
            await PerformSearchAsync();
        }

        // Tự động tìm kiếm ngay khi người dùng gõ phím vào ô tìm kiếm
        private async void txtSearch_TextChanged(object sender, EventArgs e)
        {
            await PerformSearchAsync();
        }

        // Hàm xử lý gọi API tìm kiếm chung
        private async Task PerformSearchAsync()
        {
            try
            {
                SetToken();

                string keyword = txtSearch.Text.Trim();

                _currentPage = 1;
                string url = $"User/paged?pageNumber=1&pageSize={PageSize}&keyword={Uri.EscapeDataString(keyword)}";

                var response = await _client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return;
                }

                string json = await response.Content.ReadAsStringAsync();

                var result = JsonSerializer.Deserialize<PagedResponseDto<UserDto>>(
                    json,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }
                );

                if (result == null) return;
                _totalItems = result.TotalItems;
                _totalPages = Math.Max(1, result.TotalPages);
                dgvUsers.DataSource = result.Items;
                UpdatePagingControls();
            }
            catch
            {
                // Bỏ qua lỗi ngầm khi đang gõ nhanh liên tục để tránh hiện thông báo dồn dập
            }
        }

        private void InitializePagingControls()
        {
            Panel panel = new() { Height = 42, Dock = DockStyle.Bottom, BackColor = Color.White };
            _btnPrev = new Button { Text = "◀ Trang trước", Width = 110, Height = 28, Left = 8, Top = 7 };
            _btnNext = new Button { Text = "Trang sau ▶", Width = 110, Height = 28, Top = 7 };
            _lblPaging = new Label { TextAlign = ContentAlignment.MiddleCenter, Top = 7, Height = 28 };
            _btnPrev.Click += async (_, _) => { if (_currentPage > 1) { _currentPage--; await LoadUsersAsync(); } };
            _btnNext.Click += async (_, _) => { if (_currentPage < _totalPages) { _currentPage++; await LoadUsersAsync(); } };
            panel.Controls.AddRange(new Control[] { _btnPrev, _btnNext, _lblPaging });
            panel.Resize += (_, _) => { _btnNext.Left = panel.Width - _btnNext.Width - 8; _lblPaging.Left = _btnPrev.Right + 8; _lblPaging.Width = _btnNext.Left - _lblPaging.Left - 8; };
            dgvUsers.Parent?.Controls.Add(panel);
            panel.BringToFront();
        }

        private void UpdatePagingControls()
        {
            if (_lblPaging == null) return;
            _lblPaging.Text = $"Trang {_currentPage}/{_totalPages} - Tổng {_totalItems} tài khoản";
            _btnPrev.Enabled = _currentPage > 1;
            _btnNext.Enabled = _currentPage < _totalPages;
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text) ||
                string.IsNullOrWhiteSpace(cboRole.Text) ||
                string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show(
                    "Vui lòng nhập đầy đủ thông tin!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return false;
            }

            return true;
        }

        private void ClearInput()
        {
            txtUserId.Clear();
            txtUsername.Clear();
            txtPassword.Clear();
            cboRole.SelectedIndex = -1;
            txtFullName.Clear();
        }

        private class UserDto
        {
            public int UserId { get; set; }
            public string Username { get; set; } = string.Empty;
            public string Role { get; set; } = string.Empty;
            public string FullName { get; set; } = string.Empty;
        }

        private void dgvUsers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}