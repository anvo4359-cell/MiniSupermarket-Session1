//********** HOTEN: VO THI THUY AN
//********** LOP: CCQ2411C
//********** MSSV: 2124110097
//********** MON: LAP TRINH UNG DUNG
//********** DE TAI: HAPPY MART - POS THU NGAN TICH HOP QUAN LY KHO

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    public partial class FormPOS : Form
    {
        private readonly List<ProductItem> products = new List<ProductItem>();
        private readonly List<CustomerItem> customers = new List<CustomerItem>();
        private readonly List<CartItem> cart = new List<CartItem>();
        private int _productPage = 1;
        private const int ProductPageSize = 5;
        private int _productTotalPages = 1;
        private int _productTotalItems;
        private bool _isLoadingProducts;
        private Button _btnPrevProducts = null!;
        private Button _btnNextProducts = null!;
        private Label _lblProductPaging = null!;

        // Getter hỗ trợ tên biến dgvCart trong code logic
        private DataGridView dgvCart => DataGridViewCart;

        public FormPOS()
        {
            InitializeComponent();

            txtSearch.TextChanged += txtSearch_TextChanged;
            btnAdd.Click += btnAdd_Click;
            btnRemove.Click += btnRemove_Click;
            btnCheckout.Click += btnCheckout_Click;

            ConfigureGridColumns();
            InitializeProductPagingControls();
            UpdateCartDisplay();
        }

        private void ConfigureGridColumns()
        {
            if (dgvProducts.Columns.Contains("colProdPrice"))
            {
                dgvProducts.Columns["colProdPrice"].DefaultCellStyle.Format = "N0";
                dgvProducts.Columns["colProdPrice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            if (dgvCart.Columns.Contains("colCartQty"))
            {
                dgvCart.Columns["colCartQty"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            if (dgvCart.Columns.Contains("colCartPrice"))
            {
                dgvCart.Columns["colCartPrice"].DefaultCellStyle.Format = "N0";
                dgvCart.Columns["colCartPrice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            if (dgvCart.Columns.Contains("colCartAmount"))
            {
                dgvCart.Columns["colCartAmount"].DefaultCellStyle.Format = "N0";
                dgvCart.Columns["colCartAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            dgvProducts.SelectionChanged -= dgvProducts_SelectionChanged;
            dgvProducts.SelectionChanged += dgvProducts_SelectionChanged;
            dgvProducts.CellFormatting -= dgvProducts_CellFormatting;
            dgvProducts.CellFormatting += dgvProducts_CellFormatting;
        }

        private void dgvProducts_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvProducts.Rows.Count)
                return;

            if (dgvProducts.Rows[e.RowIndex].Tag is ProductItem product && product.StockQuantity <= 10)
            {
                e.CellStyle.BackColor = product.StockQuantity == 0
                    ? Color.LightCoral
                    : Color.Moccasin;
                e.CellStyle.ForeColor = Color.DarkRed;
            }
        }

        private async void FormPOS_Load(object sender, EventArgs e)
        {
            try
            {
                await LoadProductsAsync();
                await LoadCustomersAsync();

                if (products.Count == 0)
                {
                    MessageBox.Show(
                        "Chưa có sản phẩm để hiển thị. Hãy kiểm tra API và dữ liệu SQL Server.",
                        "Happy Mart",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể tải dữ liệu bán hàng.\n\n" + ex.Message,
                    "Lỗi kết nối",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private async Task LoadProductsAsync()
        {
            if (_isLoadingProducts) return;
            _isLoadingProducts = true;
            UpdateProductPagingControls();
            try
            {
                string keyword = txtSearch.Text.Trim();
                string url = $"products/paged?pageNumber={_productPage}&pageSize={ProductPageSize}&keyword={Uri.EscapeDataString(keyword)}";
                using HttpRequestMessage request = new(HttpMethod.Get, url);
                ApiClientService.ApplyToken(request);
                using HttpResponseMessage response = await ApiClientService.Client.SendAsync(request);
                string json = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                    throw new Exception($"API sản phẩm trả về {(int)response.StatusCode}");

                using JsonDocument document = JsonDocument.Parse(json);
                JsonElement root = document.RootElement;
                _productTotalItems = root.GetProperty("totalItems").GetInt32();
                _productTotalPages = Math.Max(1, root.GetProperty("totalPages").GetInt32());
                int apiPage = root.GetProperty("currentPage").GetInt32();
                if (apiPage != _productPage && apiPage <= _productTotalPages)
                    _productPage = apiPage;
                JsonElement array = root.GetProperty("items");

                products.Clear();
                foreach (JsonElement item in array.EnumerateArray())
                {
                    int id = ReadInt(item, "ProductId", "Id", "productId", "id");
                    string name = ReadString(item, "ProductName", "Name", "productName", "name");

                    if (id <= 0 || string.IsNullOrWhiteSpace(name)) continue;

                    decimal price = ReadDecimal(item, "SellingPrice", "Price", "UnitPrice", "sellingPrice", "price");
                    int stock = ReadInt(item, "StockQuantity", "Quantity", "Stock", "stockQuantity", "quantity");

                    products.Add(new ProductItem { Id = id, Name = name, Price = price, StockQuantity = stock });
                }

                DisplayProducts(products);
            }
            finally
            {
                _isLoadingProducts = false;
                UpdateProductPagingControls();
            }
        }

        private void DisplayProducts(IEnumerable<ProductItem> list)
        {
            dgvProducts.Rows.Clear();
            foreach (ProductItem product in list)
            {
                int rowIndex = dgvProducts.Rows.Add(product.Id, product.Name, product.Price, product.StockQuantity);
                dgvProducts.Rows[rowIndex].Tag = product;
            }
            lblStock.Text = "Tổng sản phẩm: " + dgvProducts.Rows.Count;
        }

        private void InitializeProductPagingControls()
        {
            Panel panel = new()
            {
                Height = 42,
                Dock = DockStyle.Bottom,
                BackColor = Color.White
            };

            _btnPrevProducts = new Button
            {
                Text = "◀ Trang trước",
                Width = 110,
                Height = 28,
                Left = 8,
                Top = 7
            };
            _btnNextProducts = new Button
            {
                Text = "Trang sau ▶",
                Width = 110,
                Height = 28,
                Top = 7
            };
            _lblProductPaging = new Label
            {
                TextAlign = ContentAlignment.MiddleCenter,
                Height = 28,
                Top = 7
            };

            _btnPrevProducts.Click += async (_, _) =>
            {
                if (_productPage > 1)
                {
                    _productPage--;
                    await LoadProductsAsync();
                }
            };
            _btnNextProducts.Click += async (_, _) =>
            {
                if (_productPage < _productTotalPages)
                {
                    _productPage++;
                    await LoadProductsAsync();
                }
            };

            panel.Controls.AddRange(new Control[]
            {
                _btnPrevProducts,
                _btnNextProducts,
                _lblProductPaging
            });
            panel.Resize += (_, _) =>
            {
                _btnNextProducts.Left = panel.ClientSize.Width - _btnNextProducts.Width - 8;
                _lblProductPaging.Left = _btnPrevProducts.Right + 8;
                _lblProductPaging.Width = Math.Max(
                    50,
                    _btnNextProducts.Left - _lblProductPaging.Left - 8);
            };

            pnlProducts.Controls.Add(panel);
            panel.BringToFront();
            UpdateProductPagingControls();
        }

        private void UpdateProductPagingControls()
        {
            if (_lblProductPaging == null)
                return;

            _lblProductPaging.Text =
                $"Trang {_productPage}/{Math.Max(1, _productTotalPages)} - Tổng {_productTotalItems} sản phẩm";
            _btnPrevProducts.Enabled = !_isLoadingProducts && _productPage > 1;
            _btnNextProducts.Enabled = !_isLoadingProducts && _productPage < _productTotalPages;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            _productPage = 1;
            _ = LoadProductsAsync();
        }

        private void dgvProducts_SelectionChanged(object sender, EventArgs e)
        {
            ProductItem product = GetSelectedProduct();
            lblStock.Text = product == null ? "Chọn sản phẩm để xem tồn kho" : $"Tồn kho hiện tại: {product.StockQuantity}";
        }

        private ProductItem GetSelectedProduct()
        {
            return dgvProducts.CurrentRow?.Tag as ProductItem;
        }

        private async Task LoadCustomersAsync()
        {
            using HttpRequestMessage request = new(HttpMethod.Get, "customers/paged?pageNumber=1&pageSize=50");
            ApiClientService.ApplyToken(request);
            using HttpResponseMessage response = await ApiClientService.Client.SendAsync(request);
            string json = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode) return;

            using JsonDocument document = JsonDocument.Parse(json);
            JsonElement customerRoot = document.RootElement;
            customers.Clear();

            foreach (JsonElement item in customerRoot.GetProperty("items").EnumerateArray())
            {
                int id = ReadInt(item, "CustomerId", "Id", "customerId", "id");
                string name = ReadString(item, "CustomerName", "FullName", "Name", "customerName", "fullName", "name");

                if (id > 0 && !string.IsNullOrWhiteSpace(name) && !customers.Any(c => c.Id == id))
                {
                    customers.Add(new CustomerItem { Id = id, Name = name });
                }
            }

            cboCustomer.DataSource = null;
            cboCustomer.DisplayMember = nameof(CustomerItem.Name);
            cboCustomer.ValueMember = nameof(CustomerItem.Id);
            cboCustomer.DataSource = customers;

            if (customers.Count > 0) cboCustomer.SelectedIndex = 0;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ProductItem product = GetSelectedProduct();
            if (product == null)
            {
                MessageBox.Show("Bạn hãy chọn một sản phẩm trước.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int quantity = (int)nudQuantity.Value;
            CartItem existing = cart.FirstOrDefault(item => item.ProductId == product.Id);
            int quantityInCart = existing?.Quantity ?? 0;

            if (quantityInCart + quantity > product.StockQuantity)
            {
                MessageBox.Show($"Sản phẩm \"{product.Name}\" không đủ tồn kho ({product.StockQuantity}).", "Không đủ hàng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (existing != null) existing.Quantity += quantity;
            else cart.Add(new CartItem { ProductId = product.Id, ProductName = product.Name, Quantity = quantity, UnitPrice = product.Price });

            UpdateCartDisplay();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (dgvCart.CurrentRow?.Tag is CartItem selected)
            {
                cart.RemoveAll(item => item.ProductId == selected.ProductId);
                UpdateCartDisplay();
            }
        }

        private void UpdateCartDisplay()
        {
            dgvCart.Rows.Clear();
            foreach (CartItem item in cart)
            {
                int rowIndex = dgvCart.Rows.Add(item.ProductId, item.ProductName, item.Quantity, item.UnitPrice, item.Quantity * item.UnitPrice);
                dgvCart.Rows[rowIndex].Tag = item;
            }

            decimal total = cart.Sum(item => item.Quantity * item.UnitPrice);
            lblTotal.Text = "Tổng tiền: " + total.ToString("N0", CultureInfo.GetCultureInfo("vi-VN")) + " đ";
        }

        private async void btnCheckout_Click(object sender, EventArgs e)
        {
            if (cart.Count == 0 || cboCustomer.SelectedItem is not CustomerItem customer)
            {
                MessageBox.Show("Vui lòng chọn khách hàng và kiểm tra giỏ hàng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal total = cart.Sum(item => item.Quantity * item.UnitPrice);
            if (MessageBox.Show($"Khách hàng: {customer.Name}\nTổng tiền: {total:N0} đ\n\nXác nhận thanh toán?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            btnCheckout.Enabled = false;
            try
            {
                var requestBody = new
                {
                    CustomerId = customer.Id,
                    CashierUsername = SessionManager.CurrentUsername ?? "admin",
                    RewardPointsToUse = 0,
                    Items = cart.Select(item => new { ProductId = item.ProductId, Quantity = item.Quantity, UnitPrice = item.UnitPrice }).ToList()
                };

                using var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
                using HttpRequestMessage checkoutRequest = new(HttpMethod.Post, "checkout")
                {
                    Content = content
                };
                ApiClientService.ApplyToken(checkoutRequest);
                using HttpResponseMessage response = await ApiClientService.Client.SendAsync(checkoutRequest);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Thanh toán thành công!", "Happy Mart", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cart.Clear();
                    UpdateCartDisplay();
                    await LoadProductsAsync();
                }
                else
                {
                    string message = await response.Content.ReadAsStringAsync();
                    MessageBox.Show(
                        "Thanh toán không thành công. Tồn kho và dữ liệu hóa đơn đã được bảo toàn.\n\n" + message,
                        "Happy Mart",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnCheckout.Enabled = true;
            }
        }

        private static IEnumerable<JsonElement> FindObjects(JsonElement root)
        {
            if (root.ValueKind == JsonValueKind.Array)
            {
                foreach (JsonElement item in root.EnumerateArray())
                {
                    if (item.ValueKind == JsonValueKind.Object) yield return item;
                }
            }
            else if (root.ValueKind == JsonValueKind.Object)
            {
                yield return root;
            }
        }

        private static string ReadString(JsonElement element, params string[] names)
        {
            foreach (string name in names)
            {
                if (element.TryGetProperty(name, out JsonElement val) && val.ValueKind == JsonValueKind.String)
                    return val.GetString() ?? "";
            }
            return "";
        }

        private static int ReadInt(JsonElement element, params string[] names)
        {
            foreach (string name in names)
            {
                if (element.TryGetProperty(name, out JsonElement val) && val.ValueKind == JsonValueKind.Number && val.TryGetInt32(out int res))
                    return res;
            }
            return 0;
        }

        private static decimal ReadDecimal(JsonElement element, params string[] names)
        {
            foreach (string name in names)
            {
                if (element.TryGetProperty(name, out JsonElement val) && val.ValueKind == JsonValueKind.Number && val.TryGetDecimal(out decimal res))
                    return res;
            }
            return 0;
        }

        private sealed class ProductItem { public int Id { get; set; } public string Name { get; set; } = ""; public decimal Price { get; set; } public int StockQuantity { get; set; } }
        private sealed class CustomerItem { public int Id { get; set; } public string Name { get; set; } = ""; public override string ToString() => Name; }
        private sealed class CartItem { public int ProductId { get; set; } public string ProductName { get; set; } = ""; public int Quantity { get; set; } public decimal UnitPrice { get; set; } }
    }
}