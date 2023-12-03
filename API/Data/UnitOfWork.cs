using API.Data.Repositories;
using API.Entities;
using API.Interfaces;

namespace API.Data;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly DataContext _context;
    
    public IDataRepository<StockItem> StockItems => new StockItemRepository(_context);

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public UnitOfWork(DataContext context)
    {
        _context = context;
    }
}