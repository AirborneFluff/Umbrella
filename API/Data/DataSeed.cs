using System.Text.Json;
using API.Data.Seeds.Models;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public sealed class DataSeed
{
    public static async Task EnsureCreatedAsync(DataContext context)
    {
        await context.Database.EnsureCreatedAsync();
    }

    public static async Task SeedStockItems(DataContext context, string organisationId)
    {
        if (await context.StockItems.CountAsync() != 0) return;
        
        var data = await File.ReadAllTextAsync("Data/Seeds/StockItems/StockItemSeed_v2.json");
        var stockItemDTOs = JsonSerializer.Deserialize<List<StockItemSeedDTO>>(data);
        if (stockItemDTOs == null) return;
        
        var stockItems = stockItemDTOs.Select(item => new StockItem
        {
            PartitionKey = organisationId,
            PartCode = item.PartCode,
            Description = item.Description,
            Category = item.Category,
            Location = item.Location,
            SupplySources = item.SupplySources
        });
        
        context.StockItems.AddRange(stockItems);
        await context.SaveChangesAsync();
    }
}