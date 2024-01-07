namespace API.Utilities.Params;

public sealed class StockItemParams : PagedSearchParams
{
    public string? Category { get; set; }
}