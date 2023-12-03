using API.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.ActionFilters;

public sealed class ValidateStockItemExists : IAsyncActionFilter
{
    private readonly IUnitOfWork _unitOfWork;

    public ValidateStockItemExists(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var partCode = (string?) context.ActionArguments["partCode"];
        if (partCode is null) throw new Exception("PartCode not provided for validation");
        
        var stockItem = await _unitOfWork.StockItems.GetByPartCode(partCode);
        
        if (stockItem is null)
        {
            context.Result = new NotFoundObjectResult("No Stock Item found by the Part Code: " + partCode);
            return;
        }
        
        context.HttpContext.Items.Add("stockItem", stockItem);
        await next.Invoke();
    }
}