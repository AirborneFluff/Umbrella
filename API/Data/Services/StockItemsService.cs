using API.Data.DTOs;
using API.Data.Repositories;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using API.Utilities.Params;
using AutoMapper;

namespace API.Data.Services;

public class StockItemsService : IStockItemsService
{
    private readonly IMapper _mapper;
    private readonly IStockItemsRepository _stockItemsRepository;
    private readonly IStockMetadataRepository _metadataRepository;

    public StockItemsService(DataContext context, string partitionKey, IMapper mapper)
    {
        _mapper = mapper;
        _stockItemsRepository = new StockItemsRepository(context, partitionKey);
        _metadataRepository = new StockMetadataRepository(context, partitionKey);
    }

    public async Task Add(StockItem stockItem)
    {
        _stockItemsRepository.Add(stockItem);
        if (stockItem.Category is null) return;
        
        var metaData = await _metadataRepository.GetAsync();
        metaData.UniqueCategories.AddWithQuantity(stockItem.Category);
    }

    public async Task Update(StockItem stockItem, UpdateStockItemDto updates)
    {
        if (stockItem.Category == updates.Category)
        {
            _mapper.Map(updates, stockItem);
            return;
        }
        var metaData = await _metadataRepository.GetAsync();

        if (stockItem.Category is not null)
        {
            metaData.UniqueCategories.RemoveWithQuantity(stockItem.Category);
        }

        if (updates.Category is not null)
        {
            metaData.UniqueCategories.AddWithQuantity(updates.Category);
        }
        
        _mapper.Map(updates, stockItem);
    }

    public async Task Remove(StockItem stockItem)
    {
        _stockItemsRepository.Remove(stockItem);
        if (stockItem.Category is null) return;

        var metaData = await _metadataRepository.GetAsync();
        metaData.UniqueCategories.RemoveWithQuantity(stockItem.Category);
    }

    public Task<int> Count() => _stockItemsRepository.Count();

    public Task<StockItem?> GetByPartCode(string partCode) => _stockItemsRepository.GetByPartCode(partCode);

    public Task<StockItem?> GetById(string Id) => _stockItemsRepository.GetById(Id);

    public Task<PagedList<StockItem>> GetPagedList(StockItemParams stockParams) =>
        _stockItemsRepository.GetPagedList(stockParams);
    
    public Task<Dictionary<string, int>> GetCategories() => _stockItemsRepository.GetCategories();
}