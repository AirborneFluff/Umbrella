using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories;

public sealed class StockSuppliersRepository : IStockSuppliersRepository
{
    private readonly DataContext _context;
    private readonly string _partitionKey;

    public StockSuppliersRepository(DataContext context, string partitionKey)
    {
        _context = context;
        _partitionKey = partitionKey;
    }
    
    public Task<StockSupplier?> GetById(Guid id)
    {
        return _context.StockSuppliers
            .WithPartitionKey(_partitionKey)
            .FirstOrDefaultAsync(item => item.Id == id);
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