namespace API.Data.DTOs;

public sealed class UpdateUserDto
{
    public required ulong Permissions { get; set; } = 0;
}