namespace API.Entities;

public sealed class StockSupplier
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Website { get; set; }
}