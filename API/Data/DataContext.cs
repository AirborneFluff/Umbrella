using API.Entities;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore.Extensions;

namespace API.Data;

public sealed class DataContext : DbContext
{
    private readonly string _databaseName;
    private readonly MongoClient _client;
    public DbSet<StockItem> StockItems { get; set; }
    
    public DataContext(IConfiguration config)
    {
        _client = new MongoClient(config["MongoDatabase:ConnectionString"]);
        var dbName = config["MongoDatabase:DatabaseName"];
        _databaseName = dbName ?? throw new Exception("MongoDb name not configured");
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMongoDB(_client, _databaseName);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<StockItem>().ToCollection("stockItems");
    }
}