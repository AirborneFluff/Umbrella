namespace API.Entities;

public sealed class StockPrice
{
    public DateTime CreatedDate { get; set; }
    public decimal UnitPrice { get; set; }
    public int MinimumQuantity { get; set; }
}