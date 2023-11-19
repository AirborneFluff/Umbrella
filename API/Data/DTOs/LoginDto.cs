namespace API.Data.DTOs;

public sealed class LoginDto
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}