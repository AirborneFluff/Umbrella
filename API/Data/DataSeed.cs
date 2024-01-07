using System.Text.Json;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public sealed class DataSeed
{
    public static async Task EnsureCreatedAsync(DataContext context)
    {
        await context.Database.EnsureCreatedAsync();
    }

    public static async Task SeedStockItems(DataContext context)
    {
        if (await context.StockItems.CountAsync() != 0) return;
        
        var data = await File.ReadAllTextAsync("Data/Seeds/StockItems/StockItemSeed.json");
        var stockItems = JsonSerializer.Deserialize<List<StockItem>>(data);
        if (stockItems == null) return;
        
        context.StockItems.AddRange(stockItems);
        await context.SaveChangesAsync();
    }
}