using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    public partial class FormCategoryManagement : Form
    {
        // Kết nối đến Web API
        private static readonly HttpClient _client = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7297/api/")
        };

        public FormCategoryManagement()
        {
            InitializeComponent();

            btnAdd.Click += btnAdd_Click;
            btnUpdate.Click += btnUpdate_Click;
            btnDelete.Click += btnDelete_Click;
            btnLoad.Click += btnLoad_Click;

            dgvCategories.CellClick += dgvCategories_CellClick;

            this.Load += FormCategoryManagement_Load;
        }

        // =========================
        // FORM LOAD
        // =========================
        private async void FormCategoryManagement_Load(object sender, EventArgs e)
        {
            await LoadDataAsync();
        }

        // =========================
        // LẤY DANH SÁCH CATEGORY
        // =========================
        private async Task LoadDataAsync()
        {
            try
            {
                var categories =
                    await _client.GetFromJsonAsync<List<CategoryDto>>("categories");

                dgvCategories.DataSource = categories;

                // Đặt tiêu đề cột
                if (dgvCategories.Columns["CategoryId"] != null)
                    dgvCategories.Columns["CategoryId"].HeaderText = "Mã ID";

                if (dgvCategories.Columns["CategoryName"] != null)
                    dgvCategories.Columns["CategoryName"].HeaderText = "Tên Nhóm hàng";

                if (dgvCategories.Columns["Description"] != null)
                    dgvCategories.Columns["Description"].HeaderText = "Mô Tả";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể kết nối đến Web API!\n\n" + ex.Message,
                    "Lỗi kết nối",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // =========================
        // NÚT TẢI LẠI
        // =========================
        private async void btnLoad_Click(object sender, EventArgs e)
        {
            await LoadDataAsync();
        }

        // =========================
        // CLICK VÀO DÒNG TRÊN BẢNG
        // =========================
        // =========================
        // CLICK VÀO DÒNG TRÊN BẢNG
        // =========================
        private void dgvCategories_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            try
            {
                DataGridViewRow row = dgvCategories.Rows[e.RowIndex];

                txtId.Text = row.Cells["CategoryId"]?.Value?.ToString() ?? "";
                txtCategoryName.Text = row.Cells["CategoryName"]?.Value?.ToString() ?? "";
                txtDescription.Text = row.Cells["Description"]?.Value?.ToString() ?? "";
            }
            catch
            {
                // Không làm gì nếu click vào dòng không hợp lệ
            }
        }

        // =========================
        // THÊM MỚI
        // =========================
        private async void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCategoryName.Text))
            {
                MessageBox.Show(
                    "Vui lòng nhập tên nhóm hàng!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            try
            {
                var newCat = new
                {
                    CategoryName = txtCategoryName.Text.Trim(),
                    Description = txtDescription.Text.Trim()
                };

                var response =
                    await _client.PostAsJsonAsync("categories", newCat);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        "Thêm mới thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    await LoadDataAsync();
                    ClearInputs();
                }
                else
                {
                    MessageBox.Show(
                        "Thêm mới thất bại!",
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi kết nối API!\n\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // =========================
        // CẬP NHẬT
        // =========================
        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtId.Text))
            {
                MessageBox.Show(
                    "Vui lòng chọn nhóm hàng cần sửa!",
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
                return;
            }

            try
            {
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
                else
                {
                    MessageBox.Show(
                        "Cập nhật thất bại!",
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi kết nối API!\n\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // =========================
        // XÓA
        // =========================
        private async void btnDelete_Click(object sender, EventArgs e)
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
                $"Bạn có chắc muốn xóa nhóm hàng ID = {id}?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm != DialogResult.Yes)
                return;

            try
            {
                var response =
                    await _client.DeleteAsync($"categories/{id}");

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
                else
                {
                    MessageBox.Show(
                        "Xóa thất bại!",
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi kết nối API!\n\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // =========================
        // TÌM KIẾM
        // =========================
        private async void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtKeyword.Text.Trim();

            if (string.IsNullOrWhiteSpace(keyword))
            {
                await LoadDataAsync();
                return;
            }

            try
            {
                string url =
                    $"categories/search?keyword={Uri.EscapeDataString(keyword)}";

                var result =
                    await _client.GetFromJsonAsync<List<CategoryDto>>(url);

                dgvCategories.DataSource = result;

                if (result == null || result.Count == 0)
                {
                    MessageBox.Show(
                        "Không tìm thấy nhóm hàng phù hợp!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi tìm kiếm!\n\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // =========================
        // XÓA TRẮNG Ô NHẬP
        // =========================
        private void ClearInputs()
        {
            txtId.Text = "";
            txtCategoryName.Text = "";
            txtDescription.Text = "";
        }

        // =========================
        // EVENT KHÁC
        // =========================
        private void groupBox1_Enter(object sender, EventArgs e)
        {
        }
    }

    // DTO nhận dữ liệu từ Web API
    public class CategoryDto
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; } = string.Empty;

        public string? Description { get; set; }
    }
}