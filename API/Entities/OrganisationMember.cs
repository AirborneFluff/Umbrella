namespace API.Entities;

public class OrganisationMember
{
    public required string MemberId { get; set; }
    public required string OrganisationId { get; set; }
    
    public Organisation? Organisation { get; set; }
    public AppUser? Member { get; set; }
}