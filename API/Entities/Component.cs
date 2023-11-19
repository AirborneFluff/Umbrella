namespace API.Entities;

public sealed class ProductComponent
{
    public long Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
}