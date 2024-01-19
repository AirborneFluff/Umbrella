using API.Entities.Metadata;

namespace API.Interfaces;

public interface IStockMetadataRepository
{
    Task<StockMetadata> GetAsync();
}