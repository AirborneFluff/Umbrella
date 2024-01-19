using API.Data;
using API.Entities;
using AutoMapper;
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
            var roleManager = service.GetRequiredService<RoleManager<AppRole>>();
            await context.Database.MigrateAsync();
            await UserSeed.SeedRolesAndOwner(userManager, roleManager, app.Configuration);
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
            
            var userManager = service.GetRequiredService<UserManager<AppUser>>();
            var mapper = service.GetRequiredService<IMapper>();
            var user = await userManager.Users.FirstOrDefaultAsync();
            if (user is null) return;

            var unitOfWork = new UnitOfWork(context, user.OrganisationId, mapper);
            
            await DataSeed.SeedStockItems(unitOfWork, user.OrganisationId);
        }
        catch (Exception ex)
        {
            var logger = service.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred during data seeding");
        }
    }
}