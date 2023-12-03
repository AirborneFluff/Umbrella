using API.Entities;
using API.Interfaces;

namespace API.Data.Repositories;

public sealed class StockItemRepository : IDataRepository<StockItem>
{
    private readonly DataContext _context;

    public StockItemRepository(DataContext context)
    {
        _context = context;
    }
    
    public async Task<StockItem?> GetById(string id)
    {
        return await _context.StockItems.FindAsync(Guid.Parse("id"));
    }

    public async Task<StockItem?> GetById(Guid id)
    {
        return await _context.StockItems.FindAsync(id);
    }

    public void Add(StockItem entity)
    {
        _context.StockItems.Add(entity);
    }

    public void Remove(StockItem entity)
    {
        _context.StockItems.Remove(entity);
    }
}