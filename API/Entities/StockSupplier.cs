namespace API.Entities;

public sealed class StockSupplier : MongoEntity
{
    public required string Name { get; set; }
    public string? Website { get; set; }
}