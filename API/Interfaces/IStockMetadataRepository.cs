using API.Entities.Metadata;

namespace API.Interfaces;

public interface IStockMetadataRepository
{
    public Task AddCategoryMetadata(StockMetadata metadata);
}