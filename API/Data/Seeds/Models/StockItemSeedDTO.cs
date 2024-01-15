using API.Entities;

namespace API.Data.Seeds.Models;

public class StockItemSeedDTO
{
    public required string PartCode { get; set; }
    public required string Description { get; set; }
    public string? Location { get; set; }
    public string? Category { get; set; }
    
    public ICollection<StockSupplySource> SupplySources { get; set; } = new List<StockSupplySource>();
}