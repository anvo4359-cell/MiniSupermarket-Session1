using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UserController : ControllerBase
    {
        private readonly SupermarketDbContext _context;

        public UserController(SupermarketDbContext context)
        {
            _context = context;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users
                .OrderBy(u => u.UserId)
                .ToListAsync();
        }

        // GET: api/User/1
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy tài khoản!"
                });
            }

            return user;
        }

        // POST: api/User
        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            if (string.IsNullOrWhiteSpace(user.Username) ||
                string.IsNullOrWhiteSpace(user.Password) ||
                string.IsNullOrWhiteSpace(user.Role) ||
                string.IsNullOrWhiteSpace(user.FullName))
            {
                return BadRequest(new
                {
                    message = "Vui lòng nhập đầy đủ thông tin tài khoản!"
                });
            }

            bool usernameExists = await _context.Users
                .AnyAsync(u => u.Username == user.Username);

            if (usernameExists)
            {
                return BadRequest(new
                {
                    message = "Tên tài khoản đã tồn tại!"
                });
            }

            string role = user.Role.Trim();

            if (role != "Admin" &&
                role != "Cashier" &&
                role != "Warehouse")
            {
                return BadRequest(new
                {
                    message = "Quyền không hợp lệ!"
                });
            }

            user.Username = user.Username.Trim();
            user.Password = user.Password.Trim();
            user.Role = role;
            user.FullName = user.FullName.Trim();

            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetUser),
                new { id = user.UserId },
                user
            );
        }

        // PUT: api/User/1
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(
            int id,
            User user)
        {
            if (id != user.UserId)
            {
                return BadRequest(new
                {
                    message = "ID tài khoản không khớp!"
                });
            }

            var existingUser = await _context.Users
                .FindAsync(id);

            if (existingUser == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy tài khoản!"
                });
            }

            bool usernameExists = await _context.Users
                .AnyAsync(u =>
                    u.Username == user.Username &&
                    u.UserId != id);

            if (usernameExists)
            {
                return BadRequest(new
                {
                    message = "Tên tài khoản đã tồn tại!"
                });
            }

            string role = user.Role.Trim();

            if (role != "Admin" &&
                role != "Cashier" &&
                role != "Warehouse")
            {
                return BadRequest(new
                {
                    message = "Quyền không hợp lệ!"
                });
            }

            existingUser.Username = user.Username.Trim();
            existingUser.Password = user.Password.Trim();
            existingUser.Role = role;
            existingUser.FullName = user.FullName.Trim();

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/User/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy tài khoản!"
                });
            }

            _context.Users.Remove(user);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/User/search?keyword=admin
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<User>>> SearchUsers(
            [FromQuery] string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return await _context.Users
                    .OrderBy(u => u.UserId)
                    .ToListAsync();
            }

            keyword = keyword.Trim();

            var users = await _context.Users
                .Where(u =>
                    u.Username.Contains(keyword) ||
                    u.FullName.Contains(keyword) ||
                    u.Role.Contains(keyword))
                .OrderBy(u => u.UserId)
                .ToListAsync();

            return users;
        }
    }
}