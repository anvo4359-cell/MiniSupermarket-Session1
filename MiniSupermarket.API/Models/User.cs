//********** HOTEN: VO THI THUY AN
//********** LOP: CCQ2411C
//********** MSSV: 2124110097
//********** MON: LAP TRINH UNG DUNG
//********** NGAY THUCHIEN: 03/10/2026
//********** DE TAI: HAPPY MART - POS THU NGAN TICH HOP QUAN LY KHO

using System.ComponentModel.DataAnnotations;

namespace MiniSupermarket.API.Models
{
    /// <summary>
    /// Model tài khoản người dùng của hệ thống Happy Mart.
    /// Dùng để đăng nhập và phân quyền Admin / Cashier / Warehouse.
    /// </summary>
    public class User
    {
        // Khóa chính
        [Key]
        public int UserId { get; set; }

        // Tên tài khoản đăng nhập
        [Required]
        [MaxLength(50)]
        public string Username { get; set; } = string.Empty;

        // Mật khẩu
        [Required]
        [MaxLength(100)]
        public string Password { get; set; } = string.Empty;

        // Quyền người dùng: Admin / Cashier / Warehouse
        [Required]
        [MaxLength(20)]
        public string Role { get; set; } = string.Empty;

        // Họ tên nhân viên
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } = string.Empty;
    }
}