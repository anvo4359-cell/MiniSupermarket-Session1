using System.ComponentModel.DataAnnotations;

namespace MiniSupermarket.API.Models;

public sealed class LoginRequestDto
{
    [Required(ErrorMessage = "Tên tài khoản không được để trống.")]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "Mật khẩu không được để trống.")]
    public string Password { get; set; } = string.Empty;
}

public sealed record LoginResponseDto(
    string Token,
    string Username,
    string Role,
    string FullName,
    DateTime ExpiresAt);