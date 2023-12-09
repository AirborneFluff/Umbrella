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
    [HttpPost("{partCode}/supplySources/{sourceIndex:int}/prices")]
    public async Task<ActionResult> AddPriceBreak(NewPriceBreakDto price, string partCode, int sourceIndex)
    {
        var stockItem = HttpContext.GetStockItem();
        
        var supplySource = stockItem.SupplySources.ElementAtOrDefault(sourceIndex);
        if (supplySource is null) return NotFound("Supply Source index out of range");

        var newPriceBreak = _mapper.Map<PriceBreak>(price);
        supplySource.Prices.Add(newPriceBreak);

        var saveResult = await _unitOfWork.SaveChangesAsync();
        if (saveResult.Success) return Ok(newPriceBreak);

        if (saveResult.Exception is null) return BadRequest(saveResult.FailureMessage);
        return new CosmosExceptionResult((CosmosException)saveResult.Exception);
    }
    
    [ServiceFilter(typeof(ValidateStockItemExists))]
    [HttpGet("{partCode}/supplySources/{sourceIndex:int}/prices/{priceIndex:int}")]
    public async Task<ActionResult> GetPriceBreak(string partCode, int sourceIndex, int priceIndex)
    {
        var stockItem = HttpContext.GetStockItem();
        var supplySource = stockItem.SupplySources.ElementAtOrDefault(sourceIndex);
        if (supplySource is null) return NotFound("Supply Source index out of range");

        var priceBreak = supplySource.Prices.ElementAtOrDefault(priceIndex);
        if (priceBreak is null) return NotFound("Price index out of range");
        
        return Ok(priceBreak);
    }
    
    [ServiceFilter(typeof(ValidateStockItemExists))]
    [HttpGet("{partCode}/supplySources/{sourceIndex:int}/prices")]
    public async Task<ActionResult> GetAllPriceBreaks(string partCode, int sourceIndex)
    {
        var stockItem = HttpContext.GetStockItem();
        var supplySource = stockItem.SupplySources.ElementAtOrDefault(sourceIndex);
        if (supplySource is null) return NotFound("Supply Source index out of range");
        
        return Ok(supplySource.Prices);
    }
    
    [ServiceFilter(typeof(ValidateStockItemExists))]
    [Authorize(Policy = nameof(UserPermissions.ManageStockItems))]
    [HttpPut("{partCode}/supplySources/{sourceIndex:int}/prices/{priceIndex:int}")]
    public async Task<ActionResult> UpdatePriceBreak(NewPriceBreakDto price, string partCode, int sourceIndex, int priceIndex)
    {
        var stockItem = HttpContext.GetStockItem();
        
        var supplySource = stockItem.SupplySources.ElementAtOrDefault(sourceIndex);
        if (supplySource is null) return NotFound("Supply Source index out of range");

        var priceBreak = supplySource.Prices.ElementAtOrDefault(priceIndex);
        if (priceBreak is null) return NotFound("Price index out of range");

        _mapper.Map(price, priceBreak);

        var saveResult = await _unitOfWork.SaveChangesAsync();
        if (saveResult.Success) return Ok(priceBreak);

        if (saveResult.Exception is null) return BadRequest(saveResult.FailureMessage);
        return new CosmosExceptionResult((CosmosException)saveResult.Exception);
    }
    
    [ServiceFilter(typeof(ValidateStockItemExists))]
    [Authorize(Policy = nameof(UserPermissions.ManageStockItems))]
    [HttpDelete("{partCode}/supplySources/{sourceIndex:int}/prices/{priceIndex:int}")]
    public async Task<ActionResult> RemovePriceBreak(string partCode, int sourceIndex, int priceIndex)
    {
        var stockItem = HttpContext.GetStockItem();
        
        var supplySource = stockItem.SupplySources.ElementAtOrDefault(sourceIndex);
        if (supplySource is null) return NotFound("Supply Source index out of range");

        var priceBreak = supplySource.Prices.ElementAtOrDefault(priceIndex);
        if (priceBreak is null) return NotFound("Price index out of range");

        supplySource.Prices.Remove(priceBreak);

        var saveResult = await _unitOfWork.SaveChangesAsync();
        if (saveResult.Success) return Ok();

        if (saveResult.Exception is null) return BadRequest(saveResult.FailureMessage);
        return new CosmosExceptionResult((CosmosException)saveResult.Exception);
    }
}