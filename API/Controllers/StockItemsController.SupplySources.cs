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
        newSupplySource.Supplier = supplier;

        stockItem.SupplySources.Add(newSupplySource);

        var saveResult = await _unitOfWork.SaveChangesAsync();
        if (saveResult.Success) return Ok(newSupplySource);

        if (saveResult.Exception is null) return BadRequest(saveResult.FailureMessage);
        return new CosmosExceptionResult((CosmosException)saveResult.Exception);
    }
}