using API.Entities;
using API.Helpers;
using API.Utilities.Params;

namespace API.Interfaces;

public interface IStockItemsService
{
    Task<int> Count();
    Task<StockItem?> GetByPartCode(string partCode);
    Task<StockItem?> GetById(string Id);
    Task Add(StockItem stockItem);
    Task Remove(StockItem stockItem);

    Task<PagedList<StockItem>> GetPagedList(StockItemParams stockParams);
    Task<Dictionary<string, int>> GetCategories();
}