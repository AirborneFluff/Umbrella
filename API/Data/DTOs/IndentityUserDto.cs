using API.Data.Records;

namespace API.Data.DTOs;

public sealed class IdentityUserDto
{
    public required string Email { get; set; }
    public required IEnumerable<UserClaim> Claims { get; set; }
}