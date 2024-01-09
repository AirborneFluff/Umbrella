namespace API.Utilities.Params;

public class PagedSearchParams : PaginationParams
{
    public string? SearchTerm { get; set; }
}