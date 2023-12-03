using API.Interfaces;

namespace API.Entities;

public sealed class StockItem : BaseEntity, ISalesProduct
{
    public required string SKU { get; set; }
    public required string Description { get; set; }
    public decimal UnitCost { get; set; }
    
    public required string PartNumber { get; set; }
    public string? Location { get; set; }
    
    public ICollection<StockSupplySource> SupplySources { get; set; } = new List<StockSupplySource>();
}