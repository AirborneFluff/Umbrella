namespace API.Utilities.Models;

public sealed class QueryFilterParameter : QueryFilterOption
{
    public bool AllowMultiple { get; set; }
    public IEnumerable<QueryFilterOption> Options { get; set; }
}