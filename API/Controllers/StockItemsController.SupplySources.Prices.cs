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
    [HttpPost("{partCode}/supplySources/{sourceIndex:int}/prices")]
    public async Task<ActionResult> AddPriceBreak(NewPriceBreakDto price, string partCode, int sourceIndex)
    {
        var stockItem = HttpContext.GetStockItem();
        var supplySource = stockItem.SupplySources.ElementAtOrDefault(sourceIndex);
        if (supplySource is null) return BadRequest("Index out of range");

        var newPriceBreak = _mapper.Map<PriceBreak>(price);
        supplySource.Prices.Add(newPriceBreak);

        var saveResult = await _unitOfWork.SaveChangesAsync();
        if (saveResult.Success) return Ok(newPriceBreak);

        if (saveResult.Exception is null) return BadRequest(saveResult.FailureMessage);
        return new CosmosExceptionResult((CosmosException)saveResult.Exception);
    }
}