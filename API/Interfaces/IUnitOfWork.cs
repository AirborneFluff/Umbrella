using API.Entities;

namespace API.Interfaces;

public interface IUnitOfWork
{
    public IDataRepository<StockItem> StockItems { get; }
    public IDataRepository<ServiceItem> ServiceItems { get; }
    public IDataRepository<SalesTransaction> SalesTransactions { get; }
    public IDataRepository<SalesOrder> SalesOrders { get; }
}