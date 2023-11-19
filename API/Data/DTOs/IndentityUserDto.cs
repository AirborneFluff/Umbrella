using API.Data.Records;

namespace API.Data.DTOs;

public sealed class IdentityUserDto
{
    public required string Email { get; set; }
    public required List<UserClaim> Claims { get; set; }
}