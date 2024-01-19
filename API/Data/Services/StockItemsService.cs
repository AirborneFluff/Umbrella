using System.Diagnostics;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using API.Utilities.Params;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Services;

public class StockItemsService : IStockItemsService
{
    private readonly IStockItemsRepository _repository;
    private readonly DataContext _context;

    public StockItemsService(IStockItemsRepository repository, DataContext context)
    {
        _repository = repository;
        _context = context;
    }

    public async Task Add(StockItem stockItem)
    {
        _repository.Add(stockItem);
        var metaData = await _context.StockMetadata.SingleOrDefaultAsync();
        if (metaData is null)
        {
            _context.StockMetadata.Add(stockItem.CreateInitialMetadata());
            return;
        }
        if (stockItem.Category is null) return;
        metaData.UniqueCategories.AddWithQuantity(stockItem.Category);
        
        _context.StockMetadata.Update(metaData);
    }

    public async Task Remove(StockItem stockItem)
    {
        _repository.Remove(stockItem);
        var metaData = await _context.StockMetadata.SingleOrDefaultAsync();
        if (metaData is null) return;
        if (stockItem.Category is null) return;
        
        metaData.UniqueCategories.RemoveWithQuantity(stockItem.Category);
        
        _context.StockMetadata.Update(metaData);
    }

    public Task<int> Count() => _repository.Count();

    public Task<StockItem?> GetByPartCode(string partCode) => _repository.GetByPartCode(partCode);

    public Task<StockItem?> GetById(string Id) => _repository.GetById(Id);

    public Task<PagedList<StockItem>> GetPagedList(StockItemParams stockParams) =>
        _repository.GetPagedList(stockParams);
    
    public Task<Dictionary<string, int>> GetCategories() => _repository.GetCategories();
}