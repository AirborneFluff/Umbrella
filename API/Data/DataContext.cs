using API.Entities;
using API.Entities.Metadata;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public sealed class DataContext : DbContext
{
    public DbSet<StockItem> StockItems => Set<StockItem>();
    public DbSet<StockSupplier> StockSuppliers => Set<StockSupplier>();
    public DbSet<StockMetadata> StockMetadata => Set<StockMetadata>();

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<StockItem>()
            .ToContainer("umbrellaStock")
            .HasKey(item => item.Id);
        
        modelBuilder.Entity<StockSupplier>()
            .ToContainer("umbrellaStock")
            .HasKey(item => item.Id);


        modelBuilder.Entity<StockMetadata>()
            .ToContainer("umbrellaStock")
            .HasKey(m => m.OrganisationId);

        modelBuilder.Entity<StockItem>()
            .HasPartitionKey(s => s.OrganisationId);
        
        modelBuilder.Entity<StockSupplier>()
            .HasPartitionKey(s => s.OrganisationId);
        
        modelBuilder.Entity<StockMetadata>()
            .HasPartitionKey(s => s.OrganisationId);
    }
}