using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public sealed class DataContext : DbContext
{
    public DbSet<StockItem> StockItems => Set<StockItem>();

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<StockItem>()
            .ToContainer("StockItems")
            .HasKey(item => item.PartCode);
    }
}