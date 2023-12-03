using API.Entities;
using API.Helpers;

namespace API.Interfaces;

public interface IUnitOfWork
{
    public IStockItemsRepository StockItems { get; }
    public IStockSuppliersRepository StockSuppliers { get; }

    Task<OperationResult> SaveChangesAsync();
}