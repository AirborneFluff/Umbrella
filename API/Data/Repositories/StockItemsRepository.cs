using API.Entities;
using MongoDB.Driver;

namespace API.Data.Repositories;

public sealed class StockItemsRepository: DataRepository<StockItem>
{
    public StockItemsRepository(IMongoCollection<StockItem> collection) : base(collection)
    {
    }
}