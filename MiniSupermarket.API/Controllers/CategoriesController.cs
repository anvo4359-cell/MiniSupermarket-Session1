//**********HOTEN: VO THI THUY AN
//**********LOP: CCQ2411C
//**********MSSV: 2124110097
//**********MON: LAP TRINH UNG DUNG

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly SupermarketDbContext _context;

        public CategoriesController(SupermarketDbContext context)
        {
            _context = context;
        }

        // ==================================================
        // 1. READ: Lấy toàn bộ danh sách nhóm hàng
        // GET /api/categories
        // ==================================================
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetAll()
        {
            var categories = await _context.Categories
                .AsNoTracking()
                .ToListAsync();

            return Ok(categories);
        }

        // ==================================================
        // 2. READ: Lấy chi tiết một nhóm hàng theo ID
        // GET /api/categories/{id}
        // ==================================================
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetById(int id)
        {
            var category = await _context.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CategoryId == id);

            if (category == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy nhóm hàng!"
                });
            }

            return Ok(category);
        }

        // ==================================================
        // 3. SEARCH: Tìm kiếm theo từ khóa
        // GET /api/categories/search?keyword=...
        // ==================================================
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Category>>> Search(
            [FromQuery] string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return BadRequest(new
                {
                    message = "Vui lòng nhập từ khóa!"
                });
            }

            var result = await _context.Categories
                .AsNoTracking()
                .Where(c => EF.Functions.Like(
                    c.CategoryName,
                    $"%{keyword}%"))
                .ToListAsync();

            return Ok(result);
        }

        // ==================================================
        // 4. CREATE: Thêm mới nhóm hàng
        // POST /api/categories
        // ==================================================
        [HttpPost]
        public async Task<ActionResult<Category>> Create(
            [FromBody] Category newCat)
        {
            if (string.IsNullOrWhiteSpace(newCat.CategoryName))
            {
                return BadRequest(new
                {
                    message = "Tên không được trống!"
                });
            }

            _context.Categories.Add(newCat);

            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetById),
                new { id = newCat.CategoryId },
                newCat);
        }

        // ==================================================
        // 5. Kiểm tra quyền Admin
        // GET /api/categories/admin-dashboard
        // ==================================================
        [HttpGet("admin-dashboard")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAdminDashboard()
        {
            return Ok(new
            {
                message = "Chào mừng Admin! Bạn có toàn quyền quản lý hệ thống siêu thị mini."
            });
        }

        // ==================================================
        // 6. Kiểm tra quyền nhân viên
        // GET /api/categories/staff-pos
        // ==================================================
        [HttpGet("staff-pos")]
        [Authorize(Roles = "Admin,Cashier")]
        public IActionResult GetStaffPos()
        {
            return Ok(new
            {
                message = "Màn hình POS Thu ngân sẵn sàng phục vụ bán hàng."
            });
        }

        // ==================================================
        // 7. UPDATE: Cập nhật nhóm hàng
        // PUT /api/categories/{id}
        // ==================================================
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] Category updateCat)
        {
            if (string.IsNullOrWhiteSpace(updateCat.CategoryName))
            {
                return BadRequest(new
                {
                    message = "Tên không được trống!"
                });
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.CategoryId == id);

            if (category == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy nhóm hàng cần sửa!"
                });
            }

            category.CategoryName = updateCat.CategoryName;
            category.Description = updateCat.Description;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // ==================================================
        // 8. DELETE: Xóa nhóm hàng
        // DELETE /api/categories/{id}
        // ==================================================
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.CategoryId == id);

            if (category == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy nhóm hàng cần xóa!"
                });
            }

            _context.Categories.Remove(category);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}