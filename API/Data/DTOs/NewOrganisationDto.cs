using System.ComponentModel.DataAnnotations;

namespace API.Data.DTOs;

public sealed class NewOrganisationDto
{
    [Required]
    public required string OrganisationName { get; set; }
    
    [Required]
    public required string Email { get; set; }
    [Required]
    public required string Password { get; set; }
}