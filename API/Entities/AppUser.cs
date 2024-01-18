using Microsoft.AspNetCore.Identity;

namespace API.Entities;

public sealed class AppUser : IdentityUser
{
    public override required string Email { get; set; }
    public required string OrganisationId { get; set; }
    public Organisation? Organisation { get; set; }
}