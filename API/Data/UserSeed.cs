﻿using API.Authentication;
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
        
        var ownerUserName = config["OwnerCredentials:UserName"];
        var ownerPassword = config["OwnerCredentials:Password"];

        if (ownerUserName is null || ownerPassword is null)
            throw new Exception("Owner UserName and Password not configured");

        var user = new AppUser()
        {
            UserName = ownerUserName,
            Email = ownerUserName,
            Permissions = PermissionGroups.PowerUser
        };
        var result = await userManager.CreateAsync(user, ownerPassword);
        if (!result.Succeeded) throw new Exception("Issue creating owner account");
    }
}