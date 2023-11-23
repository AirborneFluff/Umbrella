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
        _unitOfWork.StockItems.Add(new StockItem()
        {
            Description = "Test"
        });

        await _unitOfWork.SaveChangesAsync();

        return Ok();
    }
}