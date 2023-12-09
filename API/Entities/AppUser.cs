using Microsoft.AspNetCore.Identity;

namespace API.Entities;

public sealed class AppUser : IdentityUser
{
    public override required string Email { get; set; }
    public ulong Permissions { get; set; } = 0;
}