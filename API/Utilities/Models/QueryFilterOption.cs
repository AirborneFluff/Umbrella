namespace API.Utilities.Models;

public sealed class QueryFilterOption
{
    public required string Title { get; set; }
    public IEnumerable<QueryFilterOption> Children { get; set; } = Enumerable.Empty<QueryFilterOption>();
    public QueryFilterParam? Parameter { get; set; }
}