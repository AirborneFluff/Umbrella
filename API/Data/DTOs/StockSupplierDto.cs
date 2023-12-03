using System.ComponentModel.DataAnnotations;

namespace API.Data.DTOs;

public sealed class StockSupplierDto
{
    [Required]
    public required string Name { get; set; }
    public string? Website { get; set; }
}