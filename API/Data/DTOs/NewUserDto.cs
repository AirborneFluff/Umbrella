using System.ComponentModel.DataAnnotations;
using API.Authentication;

namespace API.Data.DTOs;

public sealed class NewUserDto
{
    [Required]
    public required string Email { get; set; }
    [Required]
    public required string Password { get; set; }

    public ulong Permissions { get; set; } = 0;
}