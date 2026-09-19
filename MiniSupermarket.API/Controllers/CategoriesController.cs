//**********HOTEN: VO THI THUY AN
//**********lOP: CCQ2411C
//**********MSSV: 2124110097
//**********MON: LAP TRINH UNG DUNG

using Microsoft.AspNetCore.Authorization; // Thêm thư viện chứa thuộc tính [Authorize]
using Microsoft.AspNetCore.Mvc;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")] // Định tuyến cơ sở: /api/categories
    [ApiController]
    [Authorize] // 🔒 Bắt buộc phải có JWT Token hợp lệ mới gọi được tất cả các API trong Controller này
    public class CategoriesController : ControllerBase
    {
        // Dữ liệu mẫu lưu tạm trên bộ nhớ RAM (In-Memory) phục vụ kiểm thử khi chưa có Database
        private static readonly List<Category> _categories = new() {
            new Category { CategoryId = 1, CategoryName = "Bánh kẹo & Đồ ăn vặt", Description = "Snack, bánh quy, kẹo dẻo" },
            new Category { CategoryId = 2, CategoryName = "Nước giải khát & Trà", Description = "Nước ngọt, nước khoáng, trà" },
            new Category { CategoryId = 3, CategoryName = "Sữa & Sản phẩm từ sữa", Description = "Sữa tươi, sữa chua, phô mai" },
            new Category { CategoryId = 4, CategoryName = "Mì gói & Thực phẩm ăn liền", Description = "Mì ăn liền, phở khô, cháo gói" },
            new Category { CategoryId = 5, CategoryName = "Gia vị & Dầu ăn", Description = "Nước mắm, hạt nêm, dầu thực vật" }
        };

        // 1. READ: Lấy toàn bộ danh sách nhóm hàng (GET /api/categories)
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_categories);
        }

        // 2. READ: Lấy chi tiết một nhóm hàng theo ID (GET /api/categories/{id})
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var cat = _categories.FirstOrDefault(c => c.CategoryId == id);
            if (cat == null)
            {
                return NotFound(new { message = "Không tìm thấy nhóm hàng!" });
            }
            return Ok(cat);
        }

        // 3. SEARCH: Tìm kiếm nhóm hàng theo từ khóa qua Query String (GET /api/categories/search?keyword=...)
        [HttpGet("search")]
        public IActionResult Search([FromQuery] string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return BadRequest(new { message = "Vui lòng nhập từ khóa!" });
            }
            var result = _categories
                .Where(c => c.CategoryName.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                .ToList();
            return Ok(result);
        }

        // 4. CREATE: Thêm mới nhóm hàng (POST /api/categories)
        [HttpPost]
        public IActionResult Create([FromBody] Category newCat)
        {
            if (string.IsNullOrWhiteSpace(newCat.CategoryName))
            {
                return BadRequest(new { message = "Tên không được trống!" });
            }
            newCat.CategoryId = _categories.Count > 0 ? _categories.Max(c => c.CategoryId) + 1 : 1;
            _categories.Add(newCat);

            return CreatedAtAction(nameof(GetById), new { id = newCat.CategoryId }, newCat);
        }
        // 5. Kiểm tra quyền Admin (Chỉ tài khoản có Role = Admin mới được gọi)
        [HttpGet("admin-dashboard")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAdminDashboard()
        {
            return Ok(new { message = "Chào mừng Admin! Bạn có toàn quyền quản lý hệ thống siêu thị mini." });
        }

        // 6. Kiểm tra quyền chung cho nhân viên (Cả Admin và Cashier đều gọi được)
        [HttpGet("staff-pos")]
        [Authorize(Roles = "Admin,Cashier")]
        public IActionResult GetStaffPos()
        {
            return Ok(new { message = "Màn hình POS Thu ngân sẵn sàng phục vụ bán hàng." });
        }

        // 7. UPDATE: Cập nhật thông tin nhóm hàng (PUT /api/categories/{id})
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Category updateCat)
        {
            var cat = _categories.FirstOrDefault(c => c.CategoryId == id);
            if (cat == null)
            {
                return NotFound(new { message = "Không tìm thấy nhóm hàng cần sửa!" });
            }
            cat.CategoryName = updateCat.CategoryName;
            cat.Description = updateCat.Description;

            return NoContent();
        }

        // 8. DELETE: Xóa nhóm hàng theo ID (DELETE /api/categories/{id})
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var cat = _categories.FirstOrDefault(c => c.CategoryId == id);
            if (cat == null)
            {
                return NotFound(new { message = "Không tìm thấy nhóm hàng cần xóa!" });
            }
            _categories.Remove(cat);
            return NoContent();
        }
    }
}