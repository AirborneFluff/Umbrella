using API.Authentication;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public sealed class UserSeed
{
    public static async Task SeedRolesAndOwner(
        UserManager<AppUser> userManager,
        IConfiguration config)
    {
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
            Permissions = PermissionGroups.PowerUser,
            Organisation = organisation,
            OrganisationId = organisation.Id
        };
        
        organisation.Members.Add(owner);
        
        var result = await userManager.CreateAsync(owner, ownerPassword);
        if (!result.Succeeded) throw new Exception("Error adding seed account");
    }
    
}