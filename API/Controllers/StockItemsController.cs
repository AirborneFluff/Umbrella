using API.ActionFilters;
using API.ActionResults;
using API.Authentication;
using API.Data.DTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using API.Utilities.Params;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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

    [Authorize(Policy = nameof(UserPermissions.CreateStockItems))]
    [HttpPost]
    public async Task<ActionResult> AddStockItem(NewStockItemDto item)
    {
        var newStockItem = _mapper.Map<StockItem>(item);
        await _unitOfWork.StockItems.Add(newStockItem);

        var saveResult = await _unitOfWork.SaveChangesAsync();
        if (saveResult.Success) return Ok(newStockItem);

        if (saveResult.Exception is null) return BadRequest(saveResult.FailureMessage);
        return new CosmosExceptionResult((CosmosException)saveResult.Exception);
    }
    
    [Authorize(Policy = nameof(UserPermissions.ReadStockItems))]
    [ServiceFilter(typeof(ValidateStockItemExists))]
    [HttpGet("{id}")]
    public async Task<ActionResult> GetStockItem(string id)
    {
        var stockItem = HttpContext.GetStockItem();
        return Ok(stockItem);
    }
    
    [Authorize(Policy = nameof(UserPermissions.ReadStockItems))]
    [HttpGet]
    public async Task<ActionResult> GetStockItems([FromQuery] StockItemParams stockParams)
    {
        var result = await _unitOfWork.StockItems.GetPagedList(stockParams);
        
        Response.AddPaginationHeader(result);
        return Ok(result);
    }
    
    [Authorize(Policy = nameof(UserPermissions.EditStockItems))]
    [ServiceFilter(typeof(ValidateStockItemExists))]
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateStockItem(UpdateStockItemDto item, string id)
    {
        var stockItem = HttpContext.GetStockItem();
        await _unitOfWork.StockItems.Update(stockItem, item);

        var saveResult = await _unitOfWork.SaveChangesAsync();
        if (saveResult.Success) return Ok(stockItem);

        if (saveResult.Exception is null) return BadRequest(saveResult.FailureMessage);
        return new CosmosExceptionResult((CosmosException)saveResult.Exception);
    }

    [Authorize(Policy = nameof(UserPermissions.DeleteStockItems))]
    [ServiceFilter(typeof(ValidateStockItemExists))]
    [HttpDelete("{id}")]
    public async Task<ActionResult> RemoveStockItem(string id)
    {
        var stockItem = HttpContext.GetStockItem();
        await _unitOfWork.StockItems.Remove(stockItem);

        var saveResult = await _unitOfWork.SaveChangesAsync();
        if (saveResult.Success) return Ok();

        if (saveResult.Exception is null) return BadRequest(saveResult.FailureMessage);
        return new CosmosExceptionResult((CosmosException)saveResult.Exception);
    }
}