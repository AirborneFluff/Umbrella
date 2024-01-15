using System.Collections.ObjectModel;

namespace API.Entities;

public sealed class Organisation
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string OwnerId { get; set; }

    public AppUser? Owner { get; set; }
    public IEnumerable<OrganisationMember>? Members { get; set; }
}