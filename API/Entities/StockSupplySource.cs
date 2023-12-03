namespace API.Entities;

public sealed class StockSupplySource
{
    public required string SupplierName { get; set; }
    public required string SupplierSKU { get; set; }
    public ICollection<PriceBreak> Prices { get; set; } = new List<PriceBreak>();
}