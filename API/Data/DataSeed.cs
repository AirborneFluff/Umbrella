using System.Text.Json;
using API.Data.Seeds.Models;
using API.Entities;
using API.Interfaces;

namespace API.Data;

public sealed class DataSeed
{
    public static async Task EnsureCreatedAsync(DataContext context)
    {
        await context.Database.EnsureCreatedAsync();
    }

    public static async Task SeedStockItems(IUnitOfWork unitOfWork, string organisationId)
    {
        if (await unitOfWork.StockItems.Count() > 0) return;
        
        var data = await File.ReadAllTextAsync("Data/Seeds/StockItems/StockItemSeed_v2.json");
        var stockItemDTOs = JsonSerializer.Deserialize<List<StockItemSeedDTO>>(data);
        if (stockItemDTOs == null) return;

        var addTasks = stockItemDTOs.Select(item => new StockItem
            {
                OrganisationId = organisationId,
                PartCode = item.PartCode,
                Description = item.Description,
                Category = item.Category,
                Location = item.Location,
                SupplySources = item.SupplySources
            })
            .Select(item => unitOfWork.StockItems.Add(item));

        foreach (var task in addTasks)
        {
            await task;
            await unitOfWork.SaveChangesAsync();
        }
    }
}