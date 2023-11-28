namespace API.Entities;

public sealed class StockItem : SalesTransactionItem
{
    public string? Location { get; set; }
    public List<StockSupplySource> SupplySources { get; set; } = new List<StockSupplySource>();
}