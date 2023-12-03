using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories;

public sealed class StockSuppliersRepository : IStockSuppliersRepository
{
    private readonly DataContext _context;

    public StockSuppliersRepository(DataContext context)
    {
        _context = context;
    }
    
    public Task<StockSupplier?> GetById(Guid id)
    {
        return _context.StockSuppliers.FirstOrDefaultAsync(item => item.Id == id);
    }

    public void Add(StockSupplier stockSupplier)
    {
        _context.StockSuppliers.Add(stockSupplier);
    }

    public void Remove(StockSupplier stockSupplier)
    {
        _context.StockSuppliers.Remove(stockSupplier);
    }
}