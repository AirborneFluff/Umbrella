namespace API.Utilities.Models;

public sealed class QueryFilterOption
{
    public required string Title { get; set; }
    public List<QueryFilterOption> Children { get; set; } = new List<QueryFilterOption>();
    public QueryFilterParam? Parameter { get; set; }
}