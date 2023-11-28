using API.Entities;
using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;

namespace API.Data;

public sealed class DataContext : DbContext
{
    public DbSet<StockItem> StockItems => Set<StockItem>();
    public DbSet<StockSupplier> StockSuppliers => Set<StockSupplier>();
    
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<StockItem>().ToCollection("stockItems");
        modelBuilder.Entity<StockSupplier>().ToCollection("stockSuppliers");
    }
}