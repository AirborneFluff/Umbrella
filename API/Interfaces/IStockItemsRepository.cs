using API.Entities;
using API.Helpers;
using API.Utilities.Params;

namespace API.Interfaces;

public interface IStockItemsRepository
{
    Task<StockItem?> GetByPartCode(string partCode);
    void Add(StockItem stockItem);
    void Remove(StockItem stockItem);

    Task<PagedList<StockItem>> GetPagedList(PagedSearchParams stockParams);
    Task<List<string>> GetCategories();
}