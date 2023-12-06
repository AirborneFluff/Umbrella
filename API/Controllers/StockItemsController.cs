using API.ActionFilters;
using API.ActionResults;
using API.Data.DTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;

namespace API.Controllers;

public sealed partial class StockItemsController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public StockItemsController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<ActionResult> AddStockItem(StockItemDto item)
    {
        var newStockItem = _mapper.Map<StockItem>(item);
        _unitOfWork.StockItems.Add(newStockItem);

        var saveResult = await _unitOfWork.SaveChangesAsync();
        if (saveResult.Success) return Ok(newStockItem);

        if (saveResult.Exception is null) return BadRequest(saveResult.FailureMessage);
        return new CosmosExceptionResult((CosmosException)saveResult.Exception);
    }
    
    [ServiceFilter(typeof(ValidateStockItemExists))]
    [HttpGet("{partCode}")]
    public async Task<ActionResult> GetStockItem(string partCode)
    {
        var stockItem = HttpContext.GetStockItem();
        return Ok(stockItem);
    }
    
    [ServiceFilter(typeof(ValidateStockItemExists))]
    [HttpPut("{partCode}")]
    public async Task<ActionResult> UpdateStockItem(UpdateStockItemDto item, string partCode)
    {
        var stockItem = HttpContext.GetStockItem();
        
        _mapper.Map(item, stockItem);

        var saveResult = await _unitOfWork.SaveChangesAsync();
        if (saveResult.Success) return Ok(stockItem);

        if (saveResult.Exception is null) return BadRequest(saveResult.FailureMessage);
        return new CosmosExceptionResult((CosmosException)saveResult.Exception);
    }
    
    [ServiceFilter(typeof(ValidateStockItemExists))]
    [HttpDelete("{partCode}")]
    public async Task<ActionResult> RemoveStockItem(string partCode)
    {
        var stockItem = HttpContext.GetStockItem();
        _unitOfWork.StockItems.Remove(stockItem);

        var saveResult = await _unitOfWork.SaveChangesAsync();
        if (saveResult.Success) return Ok();

        if (saveResult.Exception is null) return BadRequest(saveResult.FailureMessage);
        return new CosmosExceptionResult((CosmosException)saveResult.Exception);
    }
}