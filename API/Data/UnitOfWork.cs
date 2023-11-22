using API.Data.Repositories;
using API.Entities;
using API.Interfaces;
using MongoDB.Driver;

namespace API.Data;

public sealed class UnitOfWork : IUnitOfWork
{
    public IDataRepository<StockItem> StockItems { get; }
    public IDataRepository<ServiceItem> ServiceItems { get; } = null!;
    public IDataRepository<SalesTransaction> SalesTransactions { get; } = null!;
    public IDataRepository<SalesOrder> SalesOrders { get; } = null!;

    public UnitOfWork(IConfiguration config)
    {
        var mongoClient = new MongoClient(config["MongoDatabase:ConnectionString"]);
        var mongoDatabase = mongoClient.GetDatabase(config["DatabaseName"]);

        StockItems = new StockItemsRepository(mongoDatabase.GetCollection<StockItem>(nameof(StockItem)));
    }
    
    
    public Task<bool> SaveChangesAsync()
    {
        throw new NotImplementedException();
    }
}