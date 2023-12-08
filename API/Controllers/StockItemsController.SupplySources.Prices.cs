﻿using API.ActionFilters;
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
    [HttpPost("{partCode}/supplySources/{sourceIndex:int}/prices")]
    public async Task<ActionResult> AddPriceBreak(NewPriceBreakDto price, string partCode, int sourceIndex)
    {
        var stockItem = HttpContext.GetStockItem();
        
        var supplySource = stockItem.SupplySources.ElementAtOrDefault(sourceIndex);
        if (supplySource is null) return BadRequest("Supply Source index out of range");

        var newPriceBreak = _mapper.Map<PriceBreak>(price);
        supplySource.Prices.Add(newPriceBreak);

        var saveResult = await _unitOfWork.SaveChangesAsync();
        if (saveResult.Success) return Ok(newPriceBreak);

        if (saveResult.Exception is null) return BadRequest(saveResult.FailureMessage);
        return new CosmosExceptionResult((CosmosException)saveResult.Exception);
    }
    
    [ServiceFilter(typeof(ValidateStockItemExists))]
    [HttpPut("{partCode}/supplySources/{sourceIndex:int}/prices/{priceIndex:int}")]
    public async Task<ActionResult> UpdatePriceBreak(NewPriceBreakDto price, string partCode, int sourceIndex, int priceIndex)
    {
        var stockItem = HttpContext.GetStockItem();
        
        var supplySource = stockItem.SupplySources.ElementAtOrDefault(sourceIndex);
        if (supplySource is null) return BadRequest("Supply Source index out of range");

        var priceBreak = supplySource.Prices.ElementAtOrDefault(priceIndex);
        if (priceBreak is null) return BadRequest("Price index out of range");

        _mapper.Map(price, priceBreak);

        var saveResult = await _unitOfWork.SaveChangesAsync();
        if (saveResult.Success) return Ok(priceBreak);

        if (saveResult.Exception is null) return BadRequest(saveResult.FailureMessage);
        return new CosmosExceptionResult((CosmosException)saveResult.Exception);
    }
    
    [ServiceFilter(typeof(ValidateStockItemExists))]
    [HttpDelete("{partCode}/supplySources/{sourceIndex:int}/prices/{priceIndex:int}")]
    public async Task<ActionResult> RemovePriceBreak(string partCode, int sourceIndex, int priceIndex)
    {
        var stockItem = HttpContext.GetStockItem();
        
        var supplySource = stockItem.SupplySources.ElementAtOrDefault(sourceIndex);
        if (supplySource is null) return BadRequest("Supply Source index out of range");

        var priceBreak = supplySource.Prices.ElementAtOrDefault(priceIndex);
        if (priceBreak is null) return BadRequest("Price index out of range");

        supplySource.Prices.Remove(priceBreak);

        var saveResult = await _unitOfWork.SaveChangesAsync();
        if (saveResult.Success) return Ok();

        if (saveResult.Exception is null) return BadRequest(saveResult.FailureMessage);
        return new CosmosExceptionResult((CosmosException)saveResult.Exception);
    }
}