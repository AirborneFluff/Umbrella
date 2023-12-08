using System.Net;
using API.Data.Repositories;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly DataContext _context;
    
    public IStockItemsRepository StockItems => new StockItemsRepository(_context);
    public IStockSuppliersRepository StockSuppliers => new StockSuppliersRepository(_context);

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

    public UnitOfWork(DataContext context)
    {
        _context = context;
    }
}