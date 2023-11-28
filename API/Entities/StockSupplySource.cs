using MongoDB.Bson;

namespace API.Entities;

public sealed class StockSupplySource
{
    public required ObjectId SupplierId { get; set; }
    public required StockSupplier Supplier { get; set; }
    public required string Sku { get; set; }
    public double PackSize { get; set; }
    public int MinimumOrderQuantity { get; set; }
    
    public List<StockPrice> Prices { get; set; } = new List<StockPrice>();
}