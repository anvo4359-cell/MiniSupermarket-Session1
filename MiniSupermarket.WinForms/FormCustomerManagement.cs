
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

        // ============================================================
        // CONSTRUCTOR
        // ============================================================

        public FormCustomerManagement()
        {
            InitializeComponent();

            // Gắn sự kiện cho các nút
            btnAdd.Click += btnAdd_Click;
            btnUpdate.Click += btnUpdate_Click;
            btnDelete.Click += btnDelete_Click;
            btnLoad.Click += btnLoad_Click;
            btnSearch.Click += btnSearch_Click;

            // Click dòng trong DataGridView
            dgvCustomers.CellClick += dgvCustomers_CellClick;

            // Khi Form được mở
            this.Load += FormCustomerManagement_Load;
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

                var customers =
                    await _client.GetFromJsonAsync<List<CustomerDto>>(
                        "customers"
                    );

                dgvCustomers.DataSource = customers;

                SetColumnHeaders();
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
            }

            if (dgvCustomers.Columns["CustomerName"] != null)
            {
                dgvCustomers.Columns["CustomerName"].HeaderText =
                    "Tên khách hàng";
            }

            if (dgvCustomers.Columns["Phone"] != null)
            {
                dgvCustomers.Columns["Phone"].HeaderText =
                    "Số điện thoại";
            }

            if (dgvCustomers.Columns["Address"] != null)
            {
                dgvCustomers.Columns["Address"].HeaderText =
                    "Địa chỉ";
            }

            if (dgvCustomers.Columns["Email"] != null)
            {
                dgvCustomers.Columns["Email"].HeaderText =
                    "Email";
            }
        }

        // ============================================================
        // NÚT TẢI LẠI
        // ============================================================

        private async void btnLoad_Click(
            object sender,
            EventArgs e)
        {
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
                else if (response.StatusCode ==
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
                    "Vui lòng chọn khách hàng cần sửa!",
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
                else if (response.StatusCode ==
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
                $"Bạn có chắc muốn xóa khách hàng ID = {id}?",
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
                else if (response.StatusCode ==
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
        // GET /api/customers/search?keyword=...
        // ============================================================

        private async void btnSearch_Click(
            object sender,
            EventArgs e)
        {
            string keyword = txtKeyword.Text.Trim();

            if (string.IsNullOrWhiteSpace(keyword))
            {
                await LoadDataAsync();
                return;
            }

            try
            {
                SetJwtToken();

                string url =
                    "customers/search?keyword=" +
                    Uri.EscapeDataString(keyword);

                var result =
                    await _client.GetFromJsonAsync<List<CustomerDto>>(
                        url
                    );

                dgvCustomers.DataSource = result;

                SetColumnHeaders();

                if (result == null || result.Count == 0)
                {
                    MessageBox.Show(
                        "Không tìm thấy khách hàng phù hợp!",
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

        // ============================================================
        // XÓA TRẮNG Ô NHẬP
        // ============================================================

        private void ClearInputs()
        {
            txtId.Text = "";
            txtCustomerName.Text = "";
            txtPhone.Text = "";
            txtAddress.Text = "";
            txtEmail.Text = "";
        }

        // ============================================================
        // EVENT KHÁC
        // ============================================================

        private void groupBox1_Enter(
            object sender,
            EventArgs e)
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
    }
}