namespace API.Entities;

public sealed class StockSupplySource
{
    public required string SupplierName { get; set; }
    public required string SupplierSKU { get; set; }
    
    public required string StockUnits { get; set; }
    public decimal PackSize { get; set; }
    public decimal MinimumOrderQuantity { get; set; }
    
    public ICollection<PriceBreak> Prices { get; set; } = new List<PriceBreak>();
}