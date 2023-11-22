namespace API.Data.DTOs;

public sealed class UserUpdateDto
{
    public required string Id { get; set; }
    public required string Email { get; set; }
    public required IEnumerable<string> Roles { get; set; }
}