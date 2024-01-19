using API.Entities;
using API.Helpers;

namespace API.Interfaces;

public interface IUnitOfWork
{
    public IStockItemsService StockItems { get; }
    public IStockSuppliersRepository StockSuppliers { get; }
    public IStockMetadataRepository StockMetadata { get; }

    Task<OperationResult> SaveChangesAsync();
}