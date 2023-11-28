using API.Data;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace API.Controllers;

public sealed class StockItemsController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly DataContext _context;

    public StockItemsController(IUnitOfWork unitOfWork, DataContext context)
    {
        _unitOfWork = unitOfWork;
        _context = context;
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
        
        item.SupplySources.Add(new StockSupplySource()
        {
            SupplierId = supplier.Id,
            Supplier = supplier,
            Sku = "11-72661"
        });

        await _unitOfWork.SaveChangesAsync();

        var result = await _unitOfWork.StockItems.GetById(item.Id);
        if (result is null) return NotFound();

        return Ok(result);
    }
    

    [HttpGet("{id}")]
    public async Task<ActionResult> TestEndpointOne(string id)
    {
        var result = await _context.StockItems.FindAsync(ObjectId.Parse(id));
        if (result is null) return NotFound();

        return Ok(result);
    }
}