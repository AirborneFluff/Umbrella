using API.Data.Repositories;
using API.Entities;
using API.Interfaces;
using MongoDB.Driver;

namespace API.Data;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly IMongoDatabase _database;

    public IDataRepository<StockItem> StockItems => new StockItemsRepository(_database);
    public IDataRepository<ServiceItem> ServiceItems { get; } = null!;
    public IDataRepository<SalesTransaction> SalesTransactions { get; } = null!;
    public IDataRepository<SalesOrder> SalesOrders { get; } = null!;

    public UnitOfWork(IConfiguration config)
    {
        var mongoClient = new MongoClient(config["MongoDatabase:ConnectionString"]);
        _database = mongoClient.GetDatabase(config["MongoDatabase:DatabaseName"]);
    }
}