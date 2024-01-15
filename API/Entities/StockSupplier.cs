namespace API.Entities;

public class StockSupplier
{
    public required string PartitionKey { get; set; }
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Name { get; set; }
    public string? Website { get; set; }
}