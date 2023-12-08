using System.ComponentModel.DataAnnotations;

namespace API.Data.DTOs;

public sealed class StockSupplySourceDto
{
    [Required]
    public Guid SupplierId { get; set; }
    [Required]
    public required string SupplierSKU { get; set; }

    public string StockUnits { get; set; } = string.Empty;
    public decimal PackSize { get; set; } = 1;
    public decimal MinimumOrderQuantity { get; set; } = 1;
}