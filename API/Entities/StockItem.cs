namespace API.Entities;

public sealed class StockItem
{
    public required string OrganisationId { get; set; }
    
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string PartCode { get; set; }
    public required string Description { get; set; }
    public string? Location { get; set; }
    public string? Category { get; set; }
    
    public ICollection<StockSupplySource> SupplySources { get; set; } = new List<StockSupplySource>();
}