using API.Data.Records;

namespace API.Data.DTOs;

public sealed class AppUserDto
{
    public required string Id { get; set; }
    public required string Email { get; set; }
    public required ulong Permissions { get; set; }
    public required string PermissionsHash { get; set; }
}