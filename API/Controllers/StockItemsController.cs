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
        var item = new StockItem()
        {
            Description = "Test"
        };
        
        _unitOfWork.StockItems.Add(item);

        var supplier = new StockSupplier()
        {
            Name = "Rapid"
        };
        
        _unitOfWork.StockSuppliers.Add(supplier);
        
        item.SupplySources.Add(new StockSupplySource()
        {
            SupplierId = supplier.Id,
            Supplier = supplier,
            Sku = "11-72661"
        });

        await _unitOfWork.SaveChangesAsync();

        return Ok();
    }
}