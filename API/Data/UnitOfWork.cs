using API.Data.Repositories;
using API.Data.Services;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    private readonly string _partitionKey;
    
    public IStockItemsService StockItems => new StockItemsService(_context, _partitionKey, _mapper);
    public IStockSuppliersRepository StockSuppliers => new StockSuppliersRepository(_context, _partitionKey);
    public IStockMetadataRepository StockMetadata => new StockMetadataRepository(_context, _partitionKey);

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

    public UnitOfWork(DataContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
        _partitionKey = httpContextAccessor.HttpContext!.User.GetOrganisationId();
    }
    
    public UnitOfWork(DataContext context, string partitionKey, IMapper mapper)
    {
        _context = context;
        _partitionKey = partitionKey;
        _mapper = mapper;
    }
}