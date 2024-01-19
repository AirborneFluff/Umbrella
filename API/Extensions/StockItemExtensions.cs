using API.Entities;
using API.Entities.Metadata;

namespace API.Extensions;

public static class StockItemExtensions
{
    public static StockMetadata CreateInitialMetadata(this StockItem item)
    {
        var categories = new Dictionary<string, int>();
        if (item.Category is not null) categories.Add(item.Category, 1);

        return new StockMetadata(item.OrganisationId)
        {
            UniqueCategories = categories
        };
    }
}