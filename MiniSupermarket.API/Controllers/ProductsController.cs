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
    public class ProductsController : ControllerBase
    {
        private readonly SupermarketDbContext _context;

        public ProductsController(SupermarketDbContext context)
        {
            _context = context;
        }

        // ==================================================
        // 1. GET: Lấy toàn bộ sản phẩm
        // GET /api/products
        // ==================================================
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .AsNoTracking()
                .ToListAsync();

            return Ok(products);
        }

        // ==================================================
        // 2. GET: Lấy sản phẩm theo ID
        // GET /api/products/{id}
        // ==================================================
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy sản phẩm!"
                });
            }

            return Ok(product);
        }

        // ==================================================
        // 3. SEARCH: Tìm kiếm sản phẩm
        // GET /api/products/search?keyword=...
        // ==================================================
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Product>>> Search(
            [FromQuery] string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return BadRequest(new
                {
                    message = "Vui lòng nhập từ khóa!"
                });
            }

            var products = await _context.Products
                .Include(p => p.Category)
                .AsNoTracking()
                .Where(p =>
                    EF.Functions.Like(
                        p.ProductName,
                        $"%{keyword}%")
                    ||
                    EF.Functions.Like(
                        p.Barcode,
                        $"%{keyword}%"))
                .ToListAsync();

            return Ok(products);
        }

        // ==================================================
        // 4. CREATE: Thêm sản phẩm
        // POST /api/products
        // ==================================================
        [HttpPost]
        public async Task<ActionResult<Product>> Create(
            [FromBody] Product product)
        {
            if (string.IsNullOrWhiteSpace(product.Barcode))
            {
                return BadRequest(new
                {
                    message = "Mã vạch không được để trống!"
                });
            }

            if (string.IsNullOrWhiteSpace(product.ProductName))
            {
                return BadRequest(new
                {
                    message = "Tên sản phẩm không được để trống!"
                });
            }

            if (product.Price < 0)
            {
                return BadRequest(new
                {
                    message = "Giá sản phẩm không được âm!"
                });
            }

            if (product.StockQuantity < 0)
            {
                return BadRequest(new
                {
                    message = "Số lượng tồn kho không được âm!"
                });
            }

            var categoryExists = await _context.Categories
                .AnyAsync(c => c.CategoryId == product.CategoryId);

            if (!categoryExists)
            {
                return BadRequest(new
                {
                    message = "CategoryId không tồn tại!"
                });
            }

            _context.Products.Add(product);

            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetById),
                new { id = product.ProductId },
                product);
        }

        // ==================================================
        // 5. UPDATE: Cập nhật sản phẩm
        // PUT /api/products/{id}
        // ==================================================
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] Product updateProduct)
        {
            if (string.IsNullOrWhiteSpace(updateProduct.Barcode))
            {
                return BadRequest(new
                {
                    message = "Mã vạch không được để trống!"
                });
            }

            if (string.IsNullOrWhiteSpace(updateProduct.ProductName))
            {
                return BadRequest(new
                {
                    message = "Tên sản phẩm không được để trống!"
                });
            }

            if (updateProduct.Price < 0)
            {
                return BadRequest(new
                {
                    message = "Giá sản phẩm không được âm!"
                });
            }

            if (updateProduct.StockQuantity < 0)
            {
                return BadRequest(new
                {
                    message = "Số lượng tồn kho không được âm!"
                });
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy sản phẩm cần sửa!"
                });
            }

            var categoryExists = await _context.Categories
                .AnyAsync(c => c.CategoryId == updateProduct.CategoryId);

            if (!categoryExists)
            {
                return BadRequest(new
                {
                    message = "CategoryId không tồn tại!"
                });
            }

            product.Barcode = updateProduct.Barcode;
            product.ProductName = updateProduct.ProductName;
            product.Price = updateProduct.Price;
            product.StockQuantity = updateProduct.StockQuantity;
            product.CategoryId = updateProduct.CategoryId;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // ==================================================
        // 6. DELETE: Xóa sản phẩm
        // DELETE /api/products/{id}
        // ==================================================
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy sản phẩm cần xóa!"
                });
            }

            _context.Products.Remove(product);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}