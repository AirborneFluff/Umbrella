using API.Entities;

namespace API.Interfaces;

public interface IStockSuppliersRepository
{
    Task<StockSupplier?> GetById(int id);
    void Add(StockSupplier stockSupplier);
    void Remove(StockSupplier stockSupplier);
}