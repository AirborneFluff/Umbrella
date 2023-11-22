using API.Data;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public sealed class StockItemsController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;

    public StockItemsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<ActionResult> TestEndpoint()
    {
        var newStockItem = new StockItem()
        {
            ChargedUnitCost = (decimal)0.5,
            Description = "My Test Item",
            Location = "AAB-001"
        };
        
        await _unitOfWork.StockItems.Insert(newStockItem);

        return Ok(newStockItem);
    }
}