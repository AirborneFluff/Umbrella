using API.ActionFilters;
using API.ActionResults;
using API.Data.DTOs;
using API.Entities;
using API.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;

namespace API.Controllers;

public sealed partial class StockItemsController
{
    [ServiceFilter(typeof(ValidateStockItemExists))]
    [HttpPost("{partCode}/supplySources")]
    public async Task<ActionResult> AddSupplySource(StockSupplySourceDto supplySource, string partCode)
    {
        var stockItem = HttpContext.GetStockItem();
        
        var supplier = await _unitOfWork.StockSuppliers.GetById(supplySource.SupplierId);
        if (supplier is null) return BadRequest($"No Stock Supplier found by the Id: {supplySource.SupplierId}");

        var newSupplySource = _mapper.Map<StockSupplySource>(supplySource);
        newSupplySource.Supplier = _mapper.Map<OwnedStockSupplier>(supplier);

        stockItem.SupplySources.Add(newSupplySource);

        var saveResult = await _unitOfWork.SaveChangesAsync();
        if (saveResult.Success) return Ok(newSupplySource);

        if (saveResult.Exception is null) return BadRequest(saveResult.FailureMessage);
        return new CosmosExceptionResult((CosmosException)saveResult.Exception);
    }
    
    [ServiceFilter(typeof(ValidateStockItemExists))]
    [HttpPut("{partCode}/supplySources/{sourceIndex:int}")]
    public async Task<ActionResult> UpdateSupplySource(StockSupplySourceDto sourceUpdates, string partCode, int sourceIndex)
    {
        var stockItem = HttpContext.GetStockItem();
        
        sourceIndex -= 1;
        if (!stockItem.SupplySources.IsValidIndex(sourceIndex)) return BadRequest("Index out of range");

        var supplySource = stockItem.SupplySources.ElementAt(sourceIndex);
        _mapper.Map(sourceUpdates, supplySource);

        if (supplySource.Supplier.Id != sourceUpdates.SupplierId)
        {
            var supplier = await _unitOfWork.StockSuppliers.GetById(sourceUpdates.SupplierId);
            if (supplier is null) return BadRequest($"No Stock Supplier found by the Id: {sourceUpdates.SupplierId}");
            
            supplySource.Supplier = _mapper.Map<OwnedStockSupplier>(supplier);;
        }

        var saveResult = await _unitOfWork.SaveChangesAsync();
        if (saveResult.Success) return Ok(supplySource);

        if (saveResult.Exception is null) return BadRequest(saveResult.FailureMessage);
        return new CosmosExceptionResult((CosmosException)saveResult.Exception);
    }
    
    [ServiceFilter(typeof(ValidateStockItemExists))]
    [HttpDelete("{partCode}/supplySources/{sourceIndex:int}")]
    public async Task<ActionResult> RemoveSupplySource(string partCode, int sourceIndex)
    {
        var stockItem = HttpContext.GetStockItem();
        
        sourceIndex -= 1;
        if (!stockItem.SupplySources.IsValidIndex(sourceIndex)) return BadRequest("Index out of range");

        var supplySource = stockItem.SupplySources.ElementAt(sourceIndex);
        stockItem.SupplySources.Remove(supplySource);

        var saveResult = await _unitOfWork.SaveChangesAsync();
        if (saveResult.Success) return Ok();

        if (saveResult.Exception is null) return BadRequest(saveResult.FailureMessage);
        return new CosmosExceptionResult((CosmosException)saveResult.Exception);
    }
}