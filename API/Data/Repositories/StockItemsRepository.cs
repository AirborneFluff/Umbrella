using API.Entities;
using API.Helpers;
using API.Interfaces;
using API.Utilities.Params;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories;

public sealed class StockItemsRepository : IStockItemsRepository
{
    private readonly DataContext _context;

    public StockItemsRepository(DataContext context)
    {
        _context = context;
    }

    public Task<StockItem?> GetByPartCode(string partCode)
    {
        return _context.StockItems
            .FirstOrDefaultAsync(item => item.PartCode == partCode);
    }

    public void Add(StockItem stockItem)
    {
        _context.StockItems.Add(stockItem);
    }

    public void Remove(StockItem stockItem)
    {
        _context.StockItems.Remove(stockItem);
    }

    public async Task<PagedList<StockItem>> GetPagedList(StockItemParams stockParams)
    {
        var query = _context.StockItems.AsQueryable();

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

    public async Task<List<string>> GetCategories()
    {
        return await _context.StockItems
            .Where(item => item.Category != null)
            .Select(item => item.Category!)
            .Distinct()
            .ToListAsync();
    }
}