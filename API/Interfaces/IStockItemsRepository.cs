using API.Entities;

namespace API.Interfaces;

public interface IStockItemsRepository
{
    Task<StockItem?> GetByPartCode(string partCode);
    void Add(StockItem stockItem);
    void Remove(StockItem stockItem);
    void Update(StockItem stockItem);
}