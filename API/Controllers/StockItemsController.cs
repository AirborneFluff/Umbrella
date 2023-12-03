using API.Data;
using API.Data.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public sealed class StockItemsController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public StockItemsController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<ActionResult> AddStockItem(StockItemDto item)
    {
        var newStockItem = _mapper.Map<StockItem>(item);
        _unitOfWork.StockItems.Add(newStockItem);

        if (await _unitOfWork.SaveChangesAsync()) return Ok(newStockItem);
        return BadRequest("Issue adding stock item");
    }
}