using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

public static class WebApplicationExtensions
{
    public static async void SeedUsersDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var service = scope.ServiceProvider;
        try
        {
            var context = service.GetRequiredService<AuthenticationContext>();
            var userManager = service.GetRequiredService<UserManager<AppUser>>();
            await context.Database.MigrateAsync();
            await UserSeed.SeedRolesAndOwner(userManager, app.Configuration);
        }
        catch (Exception ex)
        {
            var logger = service.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred during data seeding");
        }
    }
    
    public static async void SeedDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var service = scope.ServiceProvider;
        try
        {
            var context = service.GetRequiredService<DataContext>();
            await DataSeed.EnsureCreatedAsync(context);
            await DataSeed.SeedStockItems(context);
        }
        catch (Exception ex)
        {
            var logger = service.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred during data seeding");
        }
    }
}