using API.Entities;
using API.Helpers;
using API.Utilities.Params;

namespace API.Interfaces;

public interface IStockItemsRepository
{
    Task<int> Count();
    Task<StockItem?> GetByPartCode(string partCode);
    Task<StockItem?> GetById(string Id);
    void Add(StockItem stockItem);
    void Remove(StockItem stockItem);

    Task<PagedList<StockItem>> GetPagedList(StockItemParams stockParams);
    Task<Dictionary<string, int>> GetCategories();
}