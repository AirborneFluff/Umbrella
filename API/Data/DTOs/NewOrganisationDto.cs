using System.ComponentModel.DataAnnotations;

namespace API.Data.DTOs;

public sealed class NewOrganisationDto
{
    [Required]
    [StringLength(32, MinimumLength = 3)]
    public required string OrganisationName { get; set; }
    
    [Required]
    public required string Email { get; set; }
    
    [Required]
    [StringLength(32, MinimumLength = 6)]
    public required string Password { get; set; }
}