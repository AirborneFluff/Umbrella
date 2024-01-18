using System.Collections.ObjectModel;
using API.Authentication;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public sealed class UserSeed
{
    public static async Task SeedRolesAndOwner(
        UserManager<AppUser> userManager,
        RoleManager<AppRole> roleManager,
        IConfiguration config)
    {
        await CreateRoles(roleManager);
        if (await userManager.Users.AnyAsync()) return;
        
        var organisationName = config["OwnerCredentials:OrganisationName"];
        var ownerUserName = config["OwnerCredentials:UserName"];
        var ownerPassword = config["OwnerCredentials:Password"];

        if (ownerUserName is null || ownerPassword is null|| organisationName is null)
            throw new Exception("Owner UserName and Password not configured");
        
        var ownerId = Guid.NewGuid().ToString();
        
        var organisation = new Organisation()
        {
            Name = organisationName,
            OwnerId = ownerId
        };
        
        var owner = new AppUser
        {
            Id = ownerId,
            UserName = ownerUserName,
            Email = ownerUserName,
            Organisation = organisation,
            OrganisationId = organisation.Id
        };

        var demoUser = new AppUser()
        {
            UserName = "demo",
            Email = "demo",
            OrganisationId = organisation.Id
        };
        
        organisation.Members.Add(owner);
        organisation.Members.Add(demoUser);
        
        await userManager.CreateAsync(demoUser, "Demologin@1");
        await userManager.AddToRoleAsync(demoUser, nameof(UserPermissions.ReadStockItems));
        
        await userManager.CreateAsync(owner, ownerPassword);
        await userManager.AddToRolesAsync(owner, Enum.GetValues<UserPermissions>().Select(role => role.ToString()));
    }

    private static async Task CreateRoles(RoleManager<AppRole> roleManager)
    {
        if (roleManager.Roles.Any()) return;
        
        var tasks = Enum.GetValues<UserPermissions>()
            .Select(role => roleManager.CreateAsync(new AppRole(role.ToString(), (ulong)role)))
            .ToArray();
        await Task.WhenAll(tasks);
    }
}