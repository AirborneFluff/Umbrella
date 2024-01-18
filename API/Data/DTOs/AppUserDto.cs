namespace API.Data.DTOs;

public sealed class AppUserDto
{
    public required string Id { get; set; }
    public required string Email { get; set; }
    public required string OrganisationId { get; set; }
    public required string Permissions { get; set; }
}