namespace API.Utilities.Models;

public sealed class QueryFilterParam
{
    public required string Param { get; set; }
    public bool Active { get; set; } = false;
}