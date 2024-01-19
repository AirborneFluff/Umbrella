using API.Entities.Metadata;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories;

public sealed class StockMetadataRepository : IStockMetadataRepository
{
    private readonly DataContext _context;
    private readonly string _partitionKey;

    public StockMetadataRepository(DataContext context, string partitionKey)
    {
        _context = context;
        _partitionKey = partitionKey;
    }


    public async Task AddCategoryMetadata(StockMetadata metadata)
    {
        metadata.OrganisationId = _partitionKey;
        _context.StockMetadata.Add(metadata);
    }
}