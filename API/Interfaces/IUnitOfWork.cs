using API.Entities;

namespace API.Interfaces;

public interface IUnitOfWork
{
    public IDataRepository<StockItem> StockItems { get; }

    Task<bool> SaveChangesAsync();
}