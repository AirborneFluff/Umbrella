namespace API.Entities.Metadata;

public class StockMetadata
{
    public Dictionary<string, int> UniqueCategories { get; set; } = new();
    public string OrganisationId { get; set; }

    public StockMetadata(string organisationId)
    {
        OrganisationId = organisationId;
    }
}