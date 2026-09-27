
// ================================================================
// ********** THÔNG TIN SINH VIÊN **********
// HỌ TÊN : VO THI THUY AN
// LỚP    : CCQ2411C
// MSSV   : 2124110097
// MÔN    : LẬP TRÌNH ỨNG DỤNG
// ================================================================
// MÔ TẢ:
// Controller xử lý chức năng quản lý khách hàng.
// - GET danh sách khách hàng
// - GET khách hàng theo ID
// - SEARCH khách hàng
// - POST thêm khách hàng
// - PUT cập nhật khách hàng
// - DELETE xóa khách hàng
// ================================================================

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
    public class CustomersController : ControllerBase
    {
        private readonly SupermarketDbContext _context;

        public CustomersController(SupermarketDbContext context)
        {
            _context = context;
        }

        // ==================================================
        // 1. GET: Lấy toàn bộ khách hàng
        // GET /api/customers
        // ==================================================
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetAll()
        {
            var customers = await _context.Customers
                .AsNoTracking()
                .ToListAsync();

            return Ok(customers);
        }

        // ==================================================
        // 2. GET: Lấy khách hàng theo ID
        // GET /api/customers/{id}
        // ==================================================
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetById(int id)
        {
            var customer = await _context.Customers
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CustomerId == id);

            if (customer == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy khách hàng!"
                });
            }

            return Ok(customer);
        }

        // ==================================================
        // 3. SEARCH: Tìm kiếm khách hàng
        // GET /api/customers/search?keyword=...
        // ==================================================
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Customer>>> Search(
            [FromQuery] string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return BadRequest(new
                {
                    message = "Vui lòng nhập từ khóa!"
                });
            }

            var customers = await _context.Customers
                .AsNoTracking()
                .Where(c =>
                    EF.Functions.Like(
                        c.CustomerName,
                        $"%{keyword}%")
                    ||
                    EF.Functions.Like(
                        c.Phone,
                        $"%{keyword}%")
                    ||
                    EF.Functions.Like(
                        c.Email,
                        $"%{keyword}%"))
                .ToListAsync();

            return Ok(customers);
        }

        // ==================================================
        // 4. CREATE: Thêm khách hàng
        // POST /api/customers
        // ==================================================
        [HttpPost]
        public async Task<ActionResult<Customer>> Create(
            [FromBody] Customer customer)
        {
            if (string.IsNullOrWhiteSpace(customer.CustomerName))
            {
                return BadRequest(new
                {
                    message = "Tên khách hàng không được để trống!"
                });
            }

            if (string.IsNullOrWhiteSpace(customer.Phone))
            {
                return BadRequest(new
                {
                    message = "Số điện thoại không được để trống!"
                });
            }

            if (string.IsNullOrWhiteSpace(customer.Address))
            {
                return BadRequest(new
                {
                    message = "Địa chỉ không được để trống!"
                });
            }

            if (string.IsNullOrWhiteSpace(customer.Email))
            {
                return BadRequest(new
                {
                    message = "Email không được để trống!"
                });
            }

            _context.Customers.Add(customer);

            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetById),
                new { id = customer.CustomerId },
                customer);
        }

        // ==================================================
        // 5. UPDATE: Cập nhật khách hàng
        // PUT /api/customers/{id}
        // ==================================================
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] Customer updateCustomer)
        {
            if (string.IsNullOrWhiteSpace(updateCustomer.CustomerName))
            {
                return BadRequest(new
                {
                    message = "Tên khách hàng không được để trống!"
                });
            }

            if (string.IsNullOrWhiteSpace(updateCustomer.Phone))
            {
                return BadRequest(new
                {
                    message = "Số điện thoại không được để trống!"
                });
            }

            if (string.IsNullOrWhiteSpace(updateCustomer.Address))
            {
                return BadRequest(new
                {
                    message = "Địa chỉ không được để trống!"
                });
            }

            if (string.IsNullOrWhiteSpace(updateCustomer.Email))
            {
                return BadRequest(new
                {
                    message = "Email không được để trống!"
                });
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.CustomerId == id);

            if (customer == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy khách hàng cần sửa!"
                });
            }

            customer.CustomerName = updateCustomer.CustomerName;
            customer.Phone = updateCustomer.Phone;
            customer.Address = updateCustomer.Address;
            customer.Email = updateCustomer.Email;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // ==================================================
        // 6. DELETE: Xóa khách hàng
        // DELETE /api/customers/{id}
        // ==================================================
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.CustomerId == id);

            if (customer == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy khách hàng cần xóa!"
                });
            }

            _context.Customers.Remove(customer);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

