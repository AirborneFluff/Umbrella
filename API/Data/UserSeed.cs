using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public sealed class UserSeed
{
    public static async Task SeedRolesAndOwner(
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IConfiguration config)
    {
        if (await userManager.Users.AnyAsync()) return;
        
        var ownerUserName = config["OwnerCredentials:UserName"];
        var ownerPassword = config["OwnerCredentials:Password"];

        if (ownerUserName is null || ownerPassword is null)
            throw new Exception("Owner UserName and Password not configured");

        var roles = new List<IdentityRole>
        {
            new IdentityRole { Name = "Member" },
            new IdentityRole { Name = "Moderator" },
            new IdentityRole { Name = "Admin" },
            new IdentityRole { Name = "Owner" },
        };

        foreach (var role in roles)
            await roleManager.CreateAsync(role);

        var user = new IdentityUser()
        {
            UserName = ownerUserName,
            Email = ownerUserName
        };
        var result = await userManager.CreateAsync(user, ownerPassword);
        if (!result.Succeeded) throw new Exception("Issue creating owner account");
        
        await userManager.AddToRoleAsync(user, "Owner");
    }
}