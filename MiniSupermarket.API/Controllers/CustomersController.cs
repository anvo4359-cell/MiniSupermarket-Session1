
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
using MiniSupermarket.API.Models.DTOs;

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

        [HttpGet("paged")]
        public async Task<ActionResult<PagedResult<CustomerItemDto>>> GetPaged(
            [FromQuery] string? keyword = null,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 5)
        {
            if (pageNumber < 1)
                return BadRequest(new { message = "Số trang phải lớn hơn hoặc bằng 1!" });

            if (pageSize < 1 || pageSize > 50)
                return BadRequest(new { message = "Kích thước trang phải từ 1 đến 50!" });

            var query = _context.Customers
                .AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                keyword = keyword.Trim();
                query = query.Where(customer =>
                    customer.CustomerName.Contains(keyword) ||
                    customer.Phone.Contains(keyword) ||
                    customer.Email.Contains(keyword));
            }

            int totalItems = await query.CountAsync();
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            var items = await query
                .OrderBy(customer => customer.CustomerId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(customer => new CustomerItemDto
                {
                    CustomerId = customer.CustomerId,
                    CustomerName = customer.CustomerName,
                    Phone = customer.Phone,
                    Address = customer.Address,
                    Email = customer.Email,
                    RewardPoints = customer.RewardPoints
                })
                .ToListAsync();

            return Ok(new PagedResult<CustomerItemDto>
            {
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = totalPages,
                Items = items
            });
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
        [Authorize(Roles = "Admin,Cashier")]
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
        [Authorize(Roles = "Admin,Cashier")]
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
        [Authorize(Roles = "Admin")]
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

