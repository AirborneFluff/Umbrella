using API.ActionFilters;
using API.ActionResults;
using API.Authentication;
using API.Data.DTOs;
using API.Entities;
using API.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;

namespace API.Controllers;

public sealed partial class StockItemsController
{
    [ServiceFilter(typeof(ValidateStockItemExists))]
    [Authorize(Policy = nameof(UserPermissions.ManageStockItems))]
    [HttpPost("{id}/supplySources")]
    public async Task<ActionResult> AddSupplySource(StockSupplySourceDto supplySource, string id)
    {
        var stockItem = HttpContext.GetStockItem();
        
        var supplier = await _unitOfWork.StockSuppliers.GetById(supplySource.SupplierId);
        if (supplier is null) return NotFound($"No Stock Supplier found by the Id: {supplySource.SupplierId}");

        var newSupplySource = _mapper.Map<StockSupplySource>(supplySource);
        newSupplySource.Supplier = _mapper.Map<OwnedStockSupplier>(supplier);

        stockItem.SupplySources.Add(newSupplySource);

        var saveResult = await _unitOfWork.SaveChangesAsync();
        if (saveResult.Success) return Ok(newSupplySource);

        if (saveResult.Exception is null) return BadRequest(saveResult.FailureMessage);
        return new CosmosExceptionResult((CosmosException)saveResult.Exception);
    }
    
    [ServiceFilter(typeof(ValidateStockItemExists))]
    [HttpGet("{id}/supplySources")]
    public async Task<ActionResult> GetAllSupplySources(string id)
    {
        var stockItem = HttpContext.GetStockItem();
        return Ok(stockItem.SupplySources);
    }
    
    [ServiceFilter(typeof(ValidateStockItemExists))]
    [HttpGet("{id}/supplySources/{sourceIndex:int}")]
    public async Task<ActionResult> GetSupplySourceByIndex(string id, int sourceIndex)
    {
        var stockItem = HttpContext.GetStockItem();
        var supplySource = stockItem.SupplySources.ElementAtOrDefault(sourceIndex);
        if (supplySource is null) return NotFound("Supply Source index out of range");
        
        return Ok(supplySource);
    }
    
    [ServiceFilter(typeof(ValidateStockItemExists))]
    [Authorize(Policy = nameof(UserPermissions.ManageStockItems))]
    [HttpPut("{id}/supplySources/{sourceIndex:int}")]
    public async Task<ActionResult> UpdateSupplySource(StockSupplySourceDto sourceUpdates, string id, int sourceIndex)
    {
        var stockItem = HttpContext.GetStockItem();
        
        var supplySource = stockItem.SupplySources.ElementAtOrDefault(sourceIndex);
        if (supplySource is null) return NotFound("Supply Source index out of range");
        
        _mapper.Map(sourceUpdates, supplySource);

        if (supplySource.Supplier.Id != sourceUpdates.SupplierId)
        {
            var supplier = await _unitOfWork.StockSuppliers.GetById(sourceUpdates.SupplierId);
            if (supplier is null) return NotFound($"No Stock Supplier found by the Id: {sourceUpdates.SupplierId}");
            
            supplySource.Supplier = _mapper.Map<OwnedStockSupplier>(supplier);;
        }

        var saveResult = await _unitOfWork.SaveChangesAsync();
        if (saveResult.Success) return Ok(supplySource);

        if (saveResult.Exception is null) return BadRequest(saveResult.FailureMessage);
        return new CosmosExceptionResult((CosmosException)saveResult.Exception);
    }
    
    [ServiceFilter(typeof(ValidateStockItemExists))]
    [Authorize(Policy = nameof(UserPermissions.ManageStockItems))]
    [HttpDelete("{id}/supplySources/{sourceIndex:int}")]
    public async Task<ActionResult> RemoveSupplySource(string id, int sourceIndex)
    {
        var stockItem = HttpContext.GetStockItem();
        
        var supplySource = stockItem.SupplySources.ElementAtOrDefault(sourceIndex);
        if (supplySource is null) return NotFound("Supply Source index out of range");
        
        stockItem.SupplySources.Remove(supplySource);

        var saveResult = await _unitOfWork.SaveChangesAsync();
        if (saveResult.Success) return Ok();

        if (saveResult.Exception is null) return BadRequest(saveResult.FailureMessage);
        return new CosmosExceptionResult((CosmosException)saveResult.Exception);
    }
}