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

    public async Task<StockMetadata> GetAsync()
    {
        var meta = await _context.StockMetadata
            .WithPartitionKey(_partitionKey)
            .SingleOrDefaultAsync();

        if (meta is not null) return meta;
        
        meta = new StockMetadata(_partitionKey);
        _context.StockMetadata.Add(meta);

        return meta;
    }
}