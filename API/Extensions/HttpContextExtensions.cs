using API.Entities;

namespace API.Extensions;

public static class HttpContextExtensions
{
    public static StockItem GetStockItem(this HttpContext context)
    {
        if (context.Items["stockItem"] is not StockItem stockItem)
            throw new Exception("Stock Item does not exist in this context");

        return stockItem;
    }
}