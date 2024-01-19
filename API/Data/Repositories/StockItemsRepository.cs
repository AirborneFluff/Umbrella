using API.Entities;
using API.Helpers;
using API.Interfaces;
using API.Utilities.Params;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories;

public sealed class StockItemsRepository : IStockItemsRepository
{
    private readonly DataContext _context;
    private readonly string _partitionKey;

    public StockItemsRepository(DataContext context, string partitionKey)
    {
        _context = context;
        _partitionKey = partitionKey;
    }

    public Task<int> Count()
    {
        return _context.StockItems
            .WithPartitionKey(_partitionKey)
            .CountAsync();
    }

    public Task<StockItem?> GetByPartCode(string partCode)
    {
        return _context.StockItems
            .WithPartitionKey(_partitionKey)
            .FirstOrDefaultAsync(item => item.PartCode == partCode);
    }

    public Task<StockItem?> GetById(string id)
    {
        return _context.StockItems
            .WithPartitionKey(_partitionKey)
            .FirstOrDefaultAsync(item => item.Id == id);
    }

    public void Add(StockItem stockItem)
    {
        stockItem.OrganisationId = _partitionKey;
        _context.StockItems.Add(stockItem);
    }

    public void Remove(StockItem stockItem)
    {
        _context.StockItems.Remove(stockItem);
    }

    public async Task<PagedList<StockItem>> GetPagedList(StockItemParams stockParams)
    {
        var query = _context.StockItems
            .WithPartitionKey(_partitionKey)
            .AsQueryable();

        if (stockParams.SearchTerm is not null)
        {
            //todo Try find solution to case sensitivity
            query = query.Where(item => item.PartCode.ToLower().Contains(stockParams.SearchTerm.ToLower()));
        }
        
        if (stockParams.Category is not null)
        {
            query = query
                .Where(item => item.Category != null)
                .Where(item => item.Category!.ToLower().Contains(stockParams.Category.ToLower()));
        }
        
        return await PagedList<StockItem>.CreateAsync(query, stockParams.PageNumber, stockParams.PageSize);
    }

    public async Task<Dictionary<string, int>> GetCategories()
    {
        var metadata = await _context.StockMetadata
            .WithPartitionKey(_partitionKey)
            .SingleOrDefaultAsync();

        return metadata?.UniqueCategories ?? new Dictionary<string, int>();
    }
}