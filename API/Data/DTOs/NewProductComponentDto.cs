using System.ComponentModel.DataAnnotations;

namespace API.Data.DTOs;

public sealed class NewProductComponentDto
{
    [Required]
    public required string Title { get; set; }
    [Required]
    public required string Description { get; set; }
}