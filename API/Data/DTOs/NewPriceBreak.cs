using System.ComponentModel.DataAnnotations;

namespace API.Data.DTOs;

public sealed class NewPriceBreak
{
    [Required]
    public decimal UnitCost { get; set; }
    public int MinimumQuantity { get; set; } = 1;
}