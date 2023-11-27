using API.Data.Repositories;
using API.Entities;
using API.Interfaces;

namespace API.Data;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly DataContext _context;
    
    public IDataRepository<StockItem> StockItems => new DataRepository<StockItem>(_context);
    public IDataRepository<ServiceItem> ServiceItems => new DataRepository<ServiceItem>(_context);
    public IDataRepository<SalesTransaction> SalesTransactions => new DataRepository<SalesTransaction>(_context);
    public IDataRepository<SalesOrder> SalesOrders => new DataRepository<SalesOrder>(_context);
    public IDataRepository<StockSupplier> StockSuppliers => new DataRepository<StockSupplier>(_context);
    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public UnitOfWork(DataContext context)
    {
        _context = context;
    }
}