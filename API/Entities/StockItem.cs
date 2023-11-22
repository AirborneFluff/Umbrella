namespace API.Entities;

public sealed class StockItem : SalesTransactionItem
{
    public string? Location { get; set; }
}