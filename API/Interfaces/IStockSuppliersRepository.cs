using API.Entities;

namespace API.Interfaces;

public interface IStockSuppliersRepository
{
    Task<StockSupplier?> GetById(Guid id);
    void Add(StockSupplier stockSupplier);
    void Remove(StockSupplier stockSupplier);
}