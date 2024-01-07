using API.Entities;
using API.Interfaces;
using API.Utilities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
public sealed class FiltersController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;

    public FiltersController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet(nameof(StockItem))]
    public async Task<ActionResult> GetStockItemFilter()
    {
        var categories = await _unitOfWork.StockItems.GetCategories();
        var config = QueryFilterConfigBuilder.FromList(categories);
        return Ok(config);
    }
    
}