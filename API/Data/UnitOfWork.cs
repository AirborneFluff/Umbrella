using API.Data.Repositories;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly DataContext _context;
    private readonly string _partitionKey;

    public IStockItemsRepository StockItems => new StockItemsRepository(_context, _partitionKey);
    public IStockSuppliersRepository StockSuppliers => new StockSuppliersRepository(_context, _partitionKey);

    public async Task<OperationResult> SaveChangesAsync()
    {
        try
        {
            await _context.SaveChangesAsync();
            return OperationResult.SuccessResult();
        }
        catch (DbUpdateException e)
        {
            return OperationResult.ExceptionResult(e.InnerException!);
        }
    }

    public UnitOfWork(DataContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _partitionKey = httpContextAccessor.HttpContext!.User.GetOrganisationId();
    }
}