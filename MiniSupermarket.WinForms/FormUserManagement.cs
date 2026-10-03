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

        public FormUserManagement()
        {
            InitializeComponent();
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

                var response = await _client.GetAsync("User");

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

                string json =
                    await response.Content.ReadAsStringAsync();

                var users =
                    JsonSerializer.Deserialize<List<UserDto>>(
                        json,
                        new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        }
                    );

                dgvUsers.DataSource = users;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi kết nối Web API:\n\n" +
                    ex.Message,
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

        private void dgvUsers_CellClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row =
                dgvUsers.Rows[e.RowIndex];

            txtUserId.Text =
                row.Cells["UserId"].Value?.ToString() ?? "";

            txtUsername.Text =
                row.Cells["Username"].Value?.ToString() ?? "";

            txtPassword.Text =
                row.Cells["Password"].Value?.ToString() ?? "";

            cboRole.Text =
                row.Cells["Role"].Value?.ToString() ?? "";

            txtFullName.Text =
                row.Cells["FullName"].Value?.ToString() ?? "";
        }

        private async void btnAdd_Click(
            object sender,
            EventArgs e)
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

                var response =
                    await _client.PostAsJsonAsync(
                        "User",
                        user
                    );

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
                    string error =
                        await response.Content.ReadAsStringAsync();

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

        private async void btnUpdate_Click(
            object sender,
            EventArgs e)
        {
            if (!int.TryParse(
                txtUserId.Text,
                out int userId))
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

                var response =
                    await _client.PutAsJsonAsync(
                        $"User/{userId}",
                        user
                    );

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
                    string error =
                        await response.Content.ReadAsStringAsync();

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

        private async void btnDelete_Click(
            object sender,
            EventArgs e)
        {
            if (!int.TryParse(
                txtUserId.Text,
                out int userId))
            {
                MessageBox.Show(
                    "Vui lòng chọn tài khoản cần xóa!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            DialogResult result =
                MessageBox.Show(
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

                var response =
                    await _client.DeleteAsync(
                        $"User/{userId}"
                    );

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
                    string error =
                        await response.Content.ReadAsStringAsync();

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

        private async void btnSearch_Click(
            object sender,
            EventArgs e)
        {
            try
            {
                SetToken();

                string keyword =
                    txtSearch.Text.Trim();

                string url = string.IsNullOrWhiteSpace(keyword)
                    ? "User"
                    : $"User/search?keyword={Uri.EscapeDataString(keyword)}";

                var response =
                    await _client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        "Không thể tìm kiếm tài khoản!",
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    return;
                }

                string json =
                    await response.Content.ReadAsStringAsync();

                var users =
                    JsonSerializer.Deserialize<List<UserDto>>(
                        json,
                        new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        }
                    );

                dgvUsers.DataSource = users;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi tìm kiếm:\n\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
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

            public string Username { get; set; }
                = string.Empty;

            public string Password { get; set; }
                = string.Empty;

            public string Role { get; set; }
                = string.Empty;

            public string FullName { get; set; }
                = string.Empty;
        }
    }
}