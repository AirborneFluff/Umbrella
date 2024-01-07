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

    public async Task<PagedList<StockItem>> GetPagedList(PaginationParams stockParams)
    {
        var query = _context.StockItems.AsQueryable();
        return await PagedList<StockItem>.CreateAsync(query, stockParams.PageNumber, stockParams.PageSize);
    }
}